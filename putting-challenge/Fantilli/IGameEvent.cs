namespace putting_challenge.Fantilli
{
    using Optional;

    /// <summary>
    /// Interface that represent a game event. Indicates that a colleague-defined event has occurred.
    /// When a colleague wants to interact with other colleagues, it calls the Mediator passing the event occured.
    /// The Mediator proceeds to notify the other colleagues passing them the event.
    /// </summary>
    public interface IGameEvent
    {
        /// <summary>
        /// Gets the type of the event.
        /// </summary>
        GameEventType EventType { get; }

        /// <typeparam name="T">unused</typeparam>
        /// <returns>an empty <see cref="Option"/>.</returns>
        Option<T> GetDetails<T>();
    }
}
