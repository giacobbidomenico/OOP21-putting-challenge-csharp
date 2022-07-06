using System;

namespace OOP21-putting-challenge-csharp.Lucioli
{
    abstract class GameState : IGameState
    {
        public Tuple Status { get; private set; }

        public IGameStateManager StateManager { get; private set; }

        public IEnvironment Environment { get; set; }

        public GameState(IGameStateManager manager, GameStatus status)
        {
            StateManager = manager;
            Status = status;
            Environment = null;
        }

        /// <inheritdoc/>
        public override Tuple<SceneType, List<IGameObject>> InitState()
        {
        }

        
        /// <inheritdoc/>
        public override void LeavingState(GameStatus nextStatus)
        {
            StateManager.SwitchState(nextStatus);
        }
        
    }
}