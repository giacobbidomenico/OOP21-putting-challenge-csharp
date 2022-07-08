using Fantilli;
using System;
using System.Collections.Generic;

namespace Lucioli
{
    public abstract class GameState : IGameState
    {
        public GameStatus Status
        { 
            get; 
            private set; 
        }

        public GameStateManager StateManager 
        { 
            get; 
            private set; 
        }

        public IEnvironment Environment 
        { 
            get; 
            set; 
        }

        public GameState(GameStateManager manager, GameStatus status)
        {
            StateManager = manager;
            Status = status;
            Environment = null;
        }

        /// <inheritdoc/>
        public virtual void LeavingState(GameStatus nextStatus)
        {
            StateManager.SwitchState(nextStatus);
        }

        /// <inheritdoc/>
        public abstract Tuple<SceneType, List<IGameObject>> InitState();

        /// <inheritdoc/>
        public abstract void NotifyEvents(ModelEventType eventType);
        
        /// <inheritdoc/>
        public abstract void ReceiveEvents();
    }
}