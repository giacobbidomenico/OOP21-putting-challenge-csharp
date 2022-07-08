using puttingchallenge.Giacobbi;
using System.Collections.Generic;
using System.Linq;

namespace puttingchallenge.Giacobbi
{
    /// <summary>
    /// Class that implements an event observer.
    /// </summary>
    /// <typeparam name="A"></typeparam>
    public class ObserverEvents<A> : IObserverEvents<A>
    {
        private readonly IList<A> events = new List<A>();

        /// <inheritdoc/>
        public void SendModelEvents(IList<A> sendEvents) => events.ToList().AddRange(sendEvents);

        /// <inheritdoc/> 
        IList<A> IObserverEvents<A>.GetEvents()
        {
            IList<A> copy = new List<A>(events);
            events.Clear();
            return copy;
        }
    }
}
