using System;

namespace OOP21-putting-challenge-csharp.Lucioli
{
    abstract class GameState
    {
        public Tuple Status { get; private set; }

        public IGameStateManager StateManager { get; private set; }

        public IEnvironment Environment { get; set; }

        public override Tuple<SceneType, List<IGameObject>> InitState()
        {
        }

        public GameState(IGameStateManager manager, GameStatus status)
        {
            StateManager = manager;
            Status = status;
            Environment = null;
        }

        public override void LeavingState(GameStatus nextStatus)
        {
            StateManager.SwitchState(nextStatus);
        }
        
    }
}