using Optional.Unsafe;
using System;
using System.Collections.Generic;
using PuttingChallenge.Fantilli.GameObjects;
using PuttingChallenge.Fantilli.Common;
using PuttingChallenge.Fantilli.Physics;
using PuttingChallenge.Fantilli.Events;
using PuttingChallenge.Colletta.Mediator;
using PuttingChallenge.Giacobbi.Events;
using PuttingChallenge.Giacobbi.Environment;

namespace PuttingChallenge.Lucioli
{
    public class GamePlayGameState : GameState
    {
        private const int None = 0;
        private const int MaxLives = 3;
        private const int MaxStrength = 600;

        private ObservableEvents<ModelEventType> _observable;
        private ObserverEvents<ModelEventType> _observer;
        private IEnumerable<SceneType> _currentScene;


        private static IEnumerable<SceneType> GetNextMap()
        {
            List<SceneType> maps = new List<SceneType>()
                {
                    SceneType.Environment1, SceneType.Environment2, SceneType.Environment3
                };

            foreach (var map in maps)
            {
                yield return map;
            }
        }

        public int Score
        {
            get;
            private set;
        }
        public int Lives
        {
            get;
            private set;
        }
        public IMediator GeneralMediator
        {
            get;
            set;
        }

        public void InitModelComunication()
        {
            IObservableEvents<ModelEventType> environmentObservable;
            environmentObservable = Environment.ValueOrFailure().Observable;
            _observer = new ObserverEvents<ModelEventType>();
            environmentObservable.AddObserver(_observer);
            _observable = new ObservableEvents<ModelEventType>();
            Environment.ValueOrFailure().ConfigureObservable(_observable);
        }

        /// <summary>
        /// Builds a new <see cref="GamePlayGameState"/> object.
        /// </summary>
        /// <param name="manager"> of the state.</param>
        /// <param name="status"> associated with the <see cref="GamePlayGameState"/> state.</param>
        public GamePlayGameState(GameStateManager manager, GameStatus status) : base(manager, status)
        {
        }

        /// <inheritdoc/>
        public override Tuple<IEnumerable<SceneType>, IList<IGameObject>> InitState()
        {
            Lives = MaxLives;
            Score = None;
            LoadNextEnvironment();
            return new Tuple<IEnumerable<SceneType>, IList<IGameObject>>(_currentScene, (IList<IGameObject>)Environment.ValueOrFailure().GetObjects());
        }

        /// <inheritdoc/>
        public override void LeavingState(GameStatus nextStatus)
        {
            WriteStats();
            StateManager.SwitchState(nextStatus);
        }

        private void DecScore() => Score--;
        private void IncScore() => Score++;
        private void DecLives() => Lives--;
        private void IncLives() => Lives++;

        /// <summary>
        /// Method called when the ball enters the hole.
        /// </summary>
        private void HandleWin()
        {
            IncScore();
            LoadNextEnvironment();
        }

        /// <summary>
        /// Method called when the ball stops or is out of bound.
        /// </summary>
        private void HandleMiss()
        {
            DecLives();
            if (Lives == None)
            {
                LeavingState(GameStatus.GameOver);
            }
            else
            {
                // missing
            }
        }

        private void LoadNextEnvironment()
        {
            var nextMap = GetNextMap();
            if (nextMap != null)
            {
                _currentScene = nextMap;
                InitModelComunication();
                GeneralMediator.NotifyColleagues(new GameEventWithDetailsImpl<Tuple<IEnumerable<SceneType>, IList<IGameObject>>>(GameEventType.SET_SCENE, new Tuple<IEnumerable<SceneType>, IList<IGameObject>>(_currentScene, Environment.ValueOrFailure().GetObjects())), this);
            }
            else
            {
                LeavingState(GameStatus.GameWin);
            }
        }

        private void WriteStats()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Sets the velocity of the ball according to the aiming <see cref="Vector2D"/> created from the given <see cref="Point2D"/>.
        /// </summary>
        /// <param name="points"> where the mouse was pressed and released during the aiming phase.</param>
        public void Shoot(Tuple<Point2D, Point2D> points)
        {
            BallPhysicsComponent ballPhysicsComponent = (BallPhysicsComponent)Environment.ValueOrFailure<IEnvironment>().Ball.PhysicsComponent;
            double batStrength = Environment.ValueOrFailure<IEnvironment>().Player.BatInUse.GetBatStrength();
            if (!ballPhysicsComponent.IsMoving)
            {
                Vector2D shootingVector = Vector2D.GetVectorFrom(points.Item1, points.Item2);
                shootingVector.X *= batStrength;
                shootingVector.Y *= batStrength;
                if (shootingVector.GetModule() > MaxStrength)
                {
                    double moduleRate = MaxStrength / shootingVector.GetModule();
                    shootingVector = new Vector2D(moduleRate * shootingVector.X, moduleRate * shootingVector.Y);
                }
                Environment.ValueOrFailure<IEnvironment>().Ball.Velocity = shootingVector;
                NotifyEvents(ModelEventType.SHOOT);
            }
        }
        public override void NotifyEvents(ModelEventType eventType)
        {
            throw new NotImplementedException();
        }

        public override void ReceiveEvents()
        {
            throw new NotImplementedException();
        }
    }
}