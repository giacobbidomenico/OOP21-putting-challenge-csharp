using puttingchallenge.Fantilli.events;

namespace PuttingChallenge.Colletta.Mediator
{
    /// <summary>
    /// Defines an object that needs to interact with other objects (other colleagues), 
    /// notifying them the occurred <see cref="IGameEvent"/>.
    /// A Colleague delegates its interaction with other Colleagues to a <see cref="IMediator"/>.
    /// </summary>
    public interface IColleague
    {
        /// <summary>
        /// Sets the mediator to delegate event notification.
        /// </summary>
        IMediator Mediator { set; }

        /// <summary>
        /// Notifies the other colleagues an event occurred.
        /// </summary>
        /// <param name="eventOccurred">The occurred event.</param>
        void NotifyEvent(IGameEvent eventOccurred);

    }
}
