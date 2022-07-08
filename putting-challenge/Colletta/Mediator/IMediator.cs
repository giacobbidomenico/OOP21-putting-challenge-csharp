using putting_challenge.Fantilli;

namespace PuttingChallenge.Colletta.Mediator
{
    /// <summary>
    /// Defines an object (Mediator) that encapsulates the interactions between a set of colleagues.
    /// A colleague delegates its interaction with other colleagues to a Mediator.
    /// </summary>
    public interface IMediator
    {
        /// <summary>
        /// Adds a new Colleague.
        /// </summary>
        /// <param name="newColleague">A new colleague to be added..</param>
        void AddColleague(IColleague newColleague);

        /// <summary>
        /// Remove a Colleague.
        /// </summary>
        /// <param name="toRemove">The colleeague to be removed.</param>
        void RemoveColleague(IColleague toRemove);

        /// <summary>
        /// Notifies the known colleagues an event.
        /// </summary>
        /// <param name="eventOccurred">The event occurred to be notified.</param>
        /// <param name="sender">The Colleague who wants to send an event.</param>
        void NotifyColleagues(IGameEvent eventOccurred, IColleague sender);
    }
}
