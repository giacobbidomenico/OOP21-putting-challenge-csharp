namespace PuttingChallenge.Fantilli.Events
{
    using Optional;

    /// <summary>
    /// Interface that represent a game event. Indicates that a colleague-defined event has occurred.
    /// It Also contains some details about the event.
    /// When a colleague wants to interact with other colleagues, it calls the Mediator passing the event occurred.
    /// The Mediator proceeds to notify the other colleagues passing them the event.
    /// </summary>
    /// <typeparam name="B">the type of the object containing details about the event</typeparam>
    public interface IGameEventWithDetails<B> : IGameEvent
    {
        /// <typeparam name="T">the expected type for the detail return</typeparam>
        /// <returns>an optional containing the details about the event, or an empty optional 
        /// if the type<code>T</code> mismatch the type of the detail</returns>
        new Option<T> GetDetails<T>();
    }
}
