using puttingchallenge.Fantilli;
using System;
using System.Collections.Generic;
using Giacobbi;
using Optional;

namespace puttingchallenge.Lucioli
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

        public Option<IEnvironment> Environment 
        { 
            get;
            set;
        }

        public GameState(GameStateManager manager, GameStatus status)
        {
            StateManager = manager;
            Status = status;
            Environment = Option.None<IEnvironment>();
        }

        /// <inheritdoc/>
        public virtual void LeavingState(GameStatus nextStatus)
        {
            StateManager.SwitchState(nextStatus);
        }

        /// <inheritdoc/>
        public abstract Tuple<IEnumerable<SceneType>, IList<IGameObject>> InitState();

        /// <inheritdoc/>
        public abstract void NotifyEvents(ModelEventType eventType);
        
        /// <inheritdoc/>
        public abstract void ReceiveEvents();
    }
}