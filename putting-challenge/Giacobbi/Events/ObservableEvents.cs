using System.Collections.Generic;
using System.Linq;

namespace puttingchallenge.Giacobbi
{
    /// <summary>
    /// Class that implements an event observable.
    /// </summary>
    /// <typeparam name="A"></typeparam>
    public class ObservableEvents<A> : IObservableEvents<A>
    {
        private readonly IList<IObserverEvents<A>> observers;

        /// <summary>
        /// Build a new <see cref="ObservableEvents{A}"/>.
        /// </summary>
        public ObservableEvents()
        {
            observers = new List<IObserverEvents<A>>();
        }

        /// <inheritdoc/>
        public void AddObserver(IObserverEvents<A> observer)
        {
            this.observers.Add(observer);
        }

        /// <inheritdoc/>
        public void RemoveObserver(IObserverEvents<A> observer)
        {
            this.observers.Remove(observer);
        }

        /// <inheritdoc/>
        public IList<A> EventsReceived()
        {
            return observers.SelectMany(e => e.GetEvents()).ToList();
        }
    }
}
