using PuttingChallenge.Fantilli;
using PuttingChallenge.Fantilli.Common;
using PuttingChallenge.Fantilli.GameObjects;
using PuttingChallenge.Fantilli.Physics;
using PuttingChallenge.Lucioli;
using PuttingChallenge.Giacobbi.Events;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Optional;
using Optional.Unsafe;
using PuttingChallenge.Colletta.Collisions;

namespace PuttingChallenge.Giacobbi.Environment
{
    public class Environment : IEnvironment
    {
        private Option<IObservableEvents<ModelEventType>> _observableGameState;
        private readonly IObservableEvents<ModelEventType> _observable;
        private readonly IObserverEvents<ModelEventType> _observer;
        private bool _collisionWithHole;
        private Point2D _precPosBall;
        private Point2D _precPosPlayer;
        private bool _notifiable;
        
        /// <inheritdoc/>
        public IGameObject Ball { get; private set; }
        
        /// <inheritdoc/>
        public PlayerObject Player { get; private set; }
        
        /// <inheritdoc/>
        public Rectangle Container { get; private set; }
        
        /// <inheritdoc/>
        public IList<IGameObject> StaticObstacle { get; private set; }
        
        /// <inheritdoc/>
        public IGameObject Hole { get; private set; }

        /// <inheritdoc/>
        public IObservableEvents<ModelEventType> Observable { get => _observable; }

        /// <summary>
        /// Build a new <see cref="Environment"/>.
        /// </summary>
        /// <param name="container">
        /// the <see cref="Rectangle"/> inside which there is the game <see cref="IEnvironment"/>
        /// </param>
        /// <param name="ball">
        /// the <see cref="IGameObject"/> corresponding to the ball in the game <see cref="IEnvironment"/>
        /// </param>
        /// <param name="player">
        /// the <see cref="IGameObject"/> corresponding to the player in the game <see cref="IEnvironment"/>
        /// </param>
        /// <param name="staticObstacles">
        /// the <see cref="IList{IGameObject}"/>, corresponding to the static obstacles 
        /// in the game <see cref="IEnvironment"/>
        /// </param>
        /// <param name="hole">
        /// the <see cref="IGameObject"/> corresponding to the hole in the game <see cref="IEnvironment"/>  
        /// </param>
        public Environment(Rectangle container,
                           IGameObject ball,
                           PlayerObject player,
                           IList<IGameObject> staticObstacles,
                           IGameObject hole)
        {
            _observableGameState = Option.None<IObservableEvents<ModelEventType>>();
            _observable = new ObservableEvents<ModelEventType>();
            _observer = new ObserverEvents<ModelEventType>();
            _precPosBall = ball.Position;
            _precPosPlayer = player.Position;

            Container = container;
            Ball = ball;
            Player = player;
            StaticObstacle = new List<IGameObject>(staticObstacles);
            Hole = hole;
        }

        /// <inheritdoc/>
        public void Update(long dt)
        {
            BallPhysicsComponent bf = (BallPhysicsComponent) Ball.PhysicsComponent;
            bf.Update(dt, Ball, this);
            this.ReceiveEvents();
            this.NotifyEvents();
        }


        private void MovePlayer()
        {
            BallPhysicsComponent bf = (BallPhysicsComponent) Ball.PhysicsComponent;

            if (this.IsBallOutOfBounds())
            {
                bf.Velocity = new Vector2D(0, 0);
                Ball.Position = _precPosBall;
                Player.Position = _precPosPlayer;
                return;
            }

            var posBall = Ball.Position;
            var newPos = this.LeftBallPos();
            if (!Player.Flip)
            {
                if (posBall.X >= 0
                        && posBall.X <= Hole.Position.X)
                {
                    Player.Flip = false;
                    Player.Position = newPos;
                }

                if (posBall.X > Hole.Position.X
                        && posBall.X < Container.Width)
                {
                    newPos = this.RightBallPos();
                    Player.Flip = true;
                    Player.Position = newPos;
                }
            }
            else
            {
                if (posBall.X > Hole.Position.X)
                {
                    newPos = this.RightBallPos();
                    Player.Flip = true;
                    Player.Position = newPos;
                }

                if (posBall.X >= 0
                        && posBall.X <= Hole.Position.X)
                {
                    Player.Flip = false;
                    Player.Position = newPos;
                }
            }
            _precPosBall = Ball.Position;
            _precPosPlayer = ((IGameObject) Player).Position;
        }

        /// <returns>
        /// return the player's position calculated to the left of the ball
        /// </returns>
        private Point2D LeftBallPos()
        {
            var bf = (BallPhysicsComponent) Ball.PhysicsComponent;
            return new Point2D(Ball.Position.X - Player.Width,
                               Ball.Position.Y - Player.Height + (bf.Radius * 2));
        }

        /// <returns>
        /// return the player's position calculated to the right of the ball
        /// </returns>
        private Point2D RightBallPos()
        {
            var bf = (BallPhysicsComponent) Ball.PhysicsComponent;
            return new Point2D(Ball.Position.X + (bf.Radius * 2),
                               Ball.Position.Y - Player.Height + (bf.Radius * 2));
        }

        ///<returns>returns true if the ball is stationary, false otherwise</returns>
        private bool IsBallStationary()
        {
            BallPhysicsComponent bf = (BallPhysicsComponent) Ball.PhysicsComponent;
            return !bf.IsMoving;
        }

        ///<returns>returns true if the ball is out of bounds, false otherwise</returns>
        private bool IsBallOutOfBounds()
        {
            var posBall = Ball.Position;
            BallPhysicsComponent bf = (BallPhysicsComponent) Ball.PhysicsComponent;
            var rectBall = new Rectangle(Convert.ToInt32(posBall.X),
                                         Convert.ToInt32(posBall.Y),
                                         Convert.ToInt32(bf.Radius * 2),
                                         Convert.ToInt32(bf.Radius * 2));
            return !Container.Contains(rectBall);
        }

        ///<returns>returns true if the ball is in the hole, false otherwise</returns>
        private bool IsBallInTheHole()
        {
            return _collisionWithHole;
        }

        /// <inheritdoc/>
        public Option<IDynamicBoundingBox.ICollisionTest> CheckCollisions(IPassiveCircleBoundingBox ballHitbox, 
                                                                          BallPhysicsComponent ballPhysics, 
                                                                          Point2D ballPosition, 
                                                                          long dt)
        {
            PassiveCircleBBTrajectoryBuilder builder = new PassiveCircleBBTrajectoryBuilder();
            IPassiveCircleBoundingBox box = new ConcretePassiveCircleBoundingBox(
                    new Point2D(ballPosition.X + ballHitbox.Radius,ballPosition.Y + ballHitbox.Radius),ballHitbox.Radius);

            builder.HitBox = box;
            builder.Physic = ballPhysics;
            builder.Position = box.Position;

            IDynamicBoundingBox.ICollisionTest result = ((GameObjectImpl)Hole).HitBox.CollidesWith(builder, box, dt);
            if (result.IsColliding())
            {
                _collisionWithHole = true;
            }

            foreach (IGameObject gameObject in StaticObstacle.ToList())
            {
                IDynamicBoundingBox.ICollisionTest currentResult = ((GameObjectImpl)gameObject).HitBox.CollidesWith(builder, box, dt);
                if (currentResult.IsColliding())
                {
                    result = currentResult;
                }
            }

            return Option.None<IDynamicBoundingBox.ICollisionTest>();
        }

        /// <inheritdoc/>
        public void ConfigureObservable(IObservableEvents<ModelEventType> observableGameState)
        {
            _observableGameState = Option.Some<IObservableEvents<ModelEventType>>(observableGameState);
            _observableGameState.ValueOrFailure().AddObserver(_observer);
        }

        /// <inheritdoc/>
        public void NotifyEvents()
        {
            IList<ModelEventType> events = new List<ModelEventType>();
            if (this.IsBallStationary() && _notifiable)
            {
                events.Add(ModelEventType.BALL_STOPPED);
                _notifiable = false;
            }
            if (this.IsBallOutOfBounds())
            {
                events.Add(ModelEventType.BALL_OUT_OF_BOUND);
                _notifiable = false;
            }
            if (this.IsBallInTheHole())
            {
                events.Add(ModelEventType.BALL_IN_HOLE);
            }
            _observer.SendModelEvents(events);
        }

        /// <inheritdoc/>
        public void ReceiveEvents()
        {
            if (!_observableGameState.HasValue)
            {
                throw new InvalidOperationException();
            }
            IEnumerable<ModelEventType> eventsReceived = _observable.EventsReceived();
            eventsReceived.All(e =>
            {
                switch (e)
                {
                    case ModelEventType.SHOOT:
                        _notifiable = true;
                        break;
                    case ModelEventType.MOVE_PLAYER:
                        this.MovePlayer();
                        break;
                }
                return true;
            });
        }

        /// <inheritdoc/>
        public IList<IGameObject> GetObjects()
        {
            IList<IGameObject> allGameObjects = new List<IGameObject>
            {
                Player,
                Ball
            };
            allGameObjects.ToList().AddRange(StaticObstacle);
            allGameObjects.Add(Hole);
            return allGameObjects;
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            if (this == obj)
            {
                return true;
            }
            if (obj == null)
            {
                return false;
            }
            if (obj is IEnvironment env)
            {
                return Ball.Equals(env.Ball)
                        && StaticObstacle.Select(e => StaticObstacle.Contains(e)).Count() != 0
                        && Container.Equals(env.Container)
                        && Hole.Equals(env.Hole);
            }
            return false;
        }

        /// <inheritdoc/>
        public override int GetHashCode() =>
            HashCode.Combine(Ball, Container, Hole, Player, StaticObstacle);

        /// <inheritdoc/>
        public void AddStaticObstacle(IGameObject obstacle) => StaticObstacle.Add(obstacle);

    }
}
