using System;
using System.Collections.Generic;
using Optional;
using PuttingChallenge.Colletta.Mediator;
using PuttingChallenge.Fantilli.Events;
using PuttingChallenge.Fantilli.GameObjects;
using PuttingChallenge.Giacobbi.Environment;
using PuttingChallenge.Giacobbi.Events;

namespace PuttingChallenge.Lucioli
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
        public IMediator Mediator { set => throw new NotImplementedException(); }

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
        public abstract void ReceiveEvents();

        public void NotifyEvent(IGameEvent eventOccurred)
        {
            throw new NotImplementedException();
        }

        public abstract void NotifyEvents(ModelEventType eventType);

    }
}