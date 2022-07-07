using System;

namespace Lucioli
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
            switch (newStatus)
            {
                case GameStatus.Play:
                    CurrentGameState = new GamePlayGameState(this, newStatus);
                    // missing
                    break;
                case GameStatus.GameOver:
                case GameStatus.GameWin:
                case GameStatus.Leaderboard:
                case GameStatus.MainMenu:
                    CurrentGameState = new ScreenGameState(this, newStatus);
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
                CurrentGameState.Environment.Update(dt);
                CurrentGameState.ReceiveEvents();
            }
        }


    }
}
