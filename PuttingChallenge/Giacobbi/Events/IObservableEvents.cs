using System;
using System.Collections.Generic;

namespace PuttingChallenge.Giacobbi.Events
{
    /// <summary>
    /// Interface that defines an event observable of type <A>.
    /// </summary>
    /// <typeparam name="A"></typeparam>
    public interface IObservableEvents<A>
    {
        /// <summary>
        /// Add an <see cref="IObserverEvents{A}"/>.
        /// </summary>
        /// <param name="observer">
        /// the <see cref="IObserver{A}"/> from which events are received
        /// </param>
        void AddObserver(IObserverEvents<A> observer);

        /// <summary>
        /// Remove an <see cref="IObserverEvents{A}"/>.
        /// </summary>
        /// <param name="observer">
        /// the <see cref="IObserverEvents{A}"/>> from which events are received
        /// </param>
        void RemoveObserver(IObserverEvents<A> observer);

        /// <returns>
        /// return a <see cref="IList{A}"/> in which its type is present for each 
        /// event received
        /// </returns>
        IList<A> EventsReceived();
    }
}
