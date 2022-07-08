using putting_challenge.Fantilli;

namespace PuttingChallenge.Colletta.Mediator
{
    public interface IColleague
    {
        /// <summary>
        /// Sets the mediator to delegate event notification.
        /// </summary>
        IMediator Mediator { set; };

        void NotifyEvent(IGameEvent eventOccurred);

    }
}
