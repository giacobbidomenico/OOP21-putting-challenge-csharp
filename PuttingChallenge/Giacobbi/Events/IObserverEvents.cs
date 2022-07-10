using System.Collections.Generic;

namespace PuttingChallenge.Giacobbi.Events
{
    /// <summary>
    /// Interface that defines an event observer.
    /// </summary>
    /// <typeparam name="A"></typeparam>
    public interface IObserverEvents<A>
    {
        /// <summary>
        /// Sends a <see cref="IList{T}"/> of events.
        /// </summary>
        /// <param name="types">the events which must be notified</param>
        void SendModelEvents(IList<A> types);

        /// <returns>
        /// return a <see cref="IList{T}"/> of the events which must be notified.
        /// </returns>
        IList<A> Events { get; }
    }
}
