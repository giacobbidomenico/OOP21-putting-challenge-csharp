using System.Collections.Generic;
using System.Linq;

namespace PuttingChallenge.Giacobbi.Events
{
    /// <summary>
    /// Class that implements an event observer.
    /// </summary>
    /// <typeparam name="A"></typeparam>
    public class ObserverEvents<A> : IObserverEvents<A>
    {
        private readonly IList<A> _events = new List<A>();

        /// <inheritdoc/>
        public void SendModelEvents(IList<A> sendEvents)
        {
            foreach(var element in sendEvents)
            {
                _events.Add(element);
            }
        }

        /// <inheritdoc/> 
        public IList<A> Events 
        { 
            get
            {
                IList<A> copy = new List<A>(_events);
                _events.Clear();
                return copy;
            }
        }
    }
}
