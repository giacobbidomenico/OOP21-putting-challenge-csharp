using putting_challenge.Fantilli;
using System;
using System.Collections.Generic;
using System.Text;

namespace PuttingChallenge.Colletta.Mediator
{
    public class ConcreteMediator : IMediator
    {
        private readonly IList<IColleague> _colleagues;
        
        public ConcreteMediator()
        {
            List<IColleague> ts = new List<IColleague>();
            _colleagues = ts;
        }

        public void AddColleague(IColleague newColleague) => _colleagues.Add(newColleague);

        public void NotifyColleagues(IGameEvent eventOccurred, IColleague sender)
        {
            foreach (IColleague colleague in _colleagues)
            {
                if (!colleague.Equals(sender))
                {
                    colleague.NotifyEvent(eventOccurred);
                }
            }
        }

        public void RemoveColleague(IColleague toRemove) => _colleagues.Remove(toRemove);
    }
}
