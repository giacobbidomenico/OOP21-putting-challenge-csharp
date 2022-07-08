using Giacobbi;
using Optional;
using Optional.Unsafe;
using putting_challenge.Fantilli;
using System;
using System.Collections.Generic;

namespace puttingchallenge.Lucioli
{
    public class GameStateManager
    {
        private Mediator _generalMediator;
        private const GameStatus _initialState = GameStatus.MainMenu;
        public GameState CurrentGameState
        {
            get;
            private set;
        }
        ///<inheritdoc/>
        public void InitState()
        {
            CurrentGameState = new ScreenGameState(this, _initialState);
        }
        ///<inheritdoc/>
        public void SwitchState(GameStatus newStatus)
        {
            IGameEvent otherEvent;
            switch (newStatus)
            {
                case GameStatus.Play:
                    CurrentGameState = new GamePlayGameState(this, newStatus);
                    CurrentGameState.Mediator = _generalMediator;
                    _generalMediator.AddColleague(CurrentGameState);
                    Tuple<IEnumerable<SceneType>, IList<IGameObject>> tuple = CurrentGameState.InitState();
                    _generalMediator.NotifyColleagues(new GameEventWithDetailsImpl<Tuple<IEnumerable<SceneType>, IList<IGameObject>>>(GameEventType.SET_SCENE, tuple), this);
                    break;
                case GameStatus.GameOver:
                    otherEvent = new GameEventWithDetailsImpl<Tuple<SceneType, IList<IGameObject>>>(GameEventType.SET_SCENE,
                        new Tuple<SceneType, IList<IGameObject>>(SceneType.GameOver, new List<IGameObject>()));
                    CurrentGameState = new ScreenGameState(this, newStatus);
                    _generalMediator.NotifyColleagues(otherEvent, this);
                    break;
                case GameStatus.GameWin:
                    otherEvent = new GameEventWithDetailsImpl<Tuple<SceneType, IList<IGameObject>>>(GameEventType.SET_SCENE,
                        new Tuple<SceneType, IList<IGameObject>>(SceneType.GameWin, new List<IGameObject>()));
                    CurrentGameState = new ScreenGameState(this, newStatus);
                    _generalMediator.NotifyColleagues(otherEvent, this);
                    break;
                case GameStatus.Leaderboard:
                    otherEvent = new GameEventWithDetailsImpl<Tuple<SceneType, IList<IGameObject>>>(GameEventType.SET_SCENE,
                        new Tuple<SceneType, IList<IGameObject>>(SceneType.Leaderboard, new List<IGameObject>()));
                    CurrentGameState = new ScreenGameState(this, newStatus);
                    _generalMediator.NotifyColleagues(otherEvent, this);
                    break;
                case GameStatus.MainMenu:
                    otherEvent = new GameEventWithDetailsImpl<Tuple<SceneType, IList<IGameObject>>>(GameEventType.SET_SCENE,
                        new Tuple<SceneType, IList<IGameObject>>(SceneType.MainMenu, new List<IGameObject>()));
                    CurrentGameState = new ScreenGameState(this, newStatus);
                    _generalMediator.NotifyColleagues(otherEvent, this);
                    // missing
                    break;
                default:
                    break;
            }
        }
        ///<inheritdoc/>
        public void Update(long dt)
        {
            if(CurrentGameState.Environment != null)
            {
                CurrentGameState.Environment.ValueOrFailure<IEnvironment>().Update(dt);
                CurrentGameState.ReceiveEvents();
            }
        }


    }
}
