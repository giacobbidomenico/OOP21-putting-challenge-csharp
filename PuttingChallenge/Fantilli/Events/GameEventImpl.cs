namespace PuttingChallenge.Fantilli.Events
{
    using Optional;

    /// <summary>
    /// Implementation of <see cref="IGameEvent"/> interface.
    /// </summary>
    public class GameEventImpl : IGameEvent
    {
        /// <summary>
        /// Build a new game event without details.
        /// </summary>
        /// <param name="eventType">the type of the event</param>
        public GameEventImpl(GameEventType eventType)
        {
            EventType = eventType;
        }

        /// <inheritdoc cref="IGameEvent.EventType"/>
        public GameEventType EventType { get; private set; }

        /// <inheritdoc cref="IGameEvent.GetDetails{T}"/>
        public Option<T> GetDetails<T>() => Option.None<T>();
    }
}
