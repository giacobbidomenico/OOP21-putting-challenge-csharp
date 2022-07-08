using putting_challenge.Fantilli;
using System.Collections.Generic;

namespace PuttingChallenge.Colletta.Mediator
{
    /// <summary>
    /// Implementation of <see cref="IMediator"/>
    /// </summary>
    public class ConcreteMediator : IMediator
    {
        private readonly IList<IColleague> _colleagues;
        
        /// <summary>
        /// Builds a <see cref="ConcreteMediator"/>
        /// </summary>
        public ConcreteMediator()
        {
            List<IColleague> ts = new List<IColleague>();
            _colleagues = ts;
        }

        /// <inheritdoc/>
        public void AddColleague(IColleague newColleague) => _colleagues.Add(newColleague);

        /// <inheritdoc/>
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

        /// <inheritdoc/>
        public void RemoveColleague(IColleague toRemove) => _colleagues.Remove(toRemove);
    }
}
