using putting_challenge.Fantilli;
using System;
using System.Collections.Generic;
using System.Text;

namespace putting_challenge.Lucioli
{
    public class ScreenGameState : GameState
    {
        public ScreenGameState(GameStateManager manager, GameStatus status) : base(manager, status)
        {
        }

        public override Tuple<SceneType, List<IGameObject>> InitState()
        {
            throw new NotImplementedException();
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
