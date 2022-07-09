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
using Optional;

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
                BuilderEnvironment builder = new BuilderEnvironment();
                builder.AddBall(new Point2D(0, 0), 1);
                builder.AddContainer(new System.Drawing.Rectangle());
                builder.AddHole(new Point2D(100, 100), 10, 10);
                builder.AddPlayer(new Point2D(0, 0), 10, 10, false);
                Environment = builder.Build().Some();
                InitModelComunication();
                Mediator.NotifyColleagues(new GameEventWithDetailsImpl<Tuple<IEnumerable<SceneType>, IList<IGameObject>>>(GameEventType.SET_SCENE, new Tuple<IEnumerable<SceneType>, IList<IGameObject>>(_currentScene, Environment.ValueOrFailure().GetObjects())), this);
            }
            else
            {
                LeavingState(GameStatus.GameWin);
            }
        }

        private void WriteStats()
        {
            
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
            IList<ModelEventType> events = new List<ModelEventType>();
            events.Add(eventType);
            _observer.SendModelEvents(events);
        }

        public override void ReceiveEvents()
        {
            IList<ModelEventType> eventsReceived = _observable.EventsReceived();
            if(eventsReceived.Count > 0)
            {
                foreach (var item in eventsReceived)
                {
                    switch (item)
                    {
                        case ModelEventType.BALL_IN_HOLE:
                            HandleWin();
                            break;
                        case ModelEventType.BALL_OUT_OF_BOUND:
                        case ModelEventType.BALL_STOPPED:
                            HandleMiss();
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        public override void NotifyEvent(IGameEvent gameEvent)
        {
            switch (gameEvent.EventType)
            {
                case GameEventType.START:
                    break;
                case GameEventType.GAMEOVER:
                    break;
                case GameEventType.SHOOT:
                    if(Status == GameStatus.Play)
                    {
                        GameEventWithDetailsImpl<Tuple<Point2D, Point2D>> eventWithDetails = (GameEventWithDetailsImpl<Tuple<Point2D, Point2D>>)gameEvent;    
                        Tuple<Point2D, Point2D> points = eventWithDetails.GetDetails<Tuple<Point2D, Point2D>>().ValueOrFailure<Tuple<Point2D, Point2D>>();
                        Shoot(points);
                    }
                    break;
                case GameEventType.WIN:
                    break;
                case GameEventType.SHOW_LEADERBOARD:
                    break;
                case GameEventType.SHOW_MAIN_MENU:
                    break;
                case GameEventType.SET_SCENE:
                    break;
                case GameEventType.UPDATE_STATS:
                    break;
                case GameEventType.QUIT:
                    break;
                default:
                    break;
            }
        }
    }
}