using puttingchallenge.Fantilli;
using System;
using System.Collections.Generic;
using System.Text;

namespace puttingchallenge.Lucioli
{
    public class ScreenGameState : GameState
    {
        public ScreenGameState(GameStateManager manager, GameStatus status) : base(manager, status)
        {
        }

        public override Tuple<IEnumerable<SceneType>, IList<IGameObject>> InitState()
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
