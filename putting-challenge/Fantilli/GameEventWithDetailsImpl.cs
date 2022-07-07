namespace putting_challenge.Fantilli
{
    using Optional;
    using System;

    /// <summary>
    /// Implementation of <see cref="IGameEventWithDetails{B}"/> interface.
    /// </summary>
    /// <typeparam name="B">the type of the object containing details about the event</typeparam>
    public class GameEventWithDetailsImpl<B> : IGameEventWithDetails<B>
    {
        private readonly B _details;

        /// <summary>
        /// Build a new game event with details.
        /// </summary>
        /// <param name="eventType">the type of the event</param>
        /// <param name="details">the details of the event</param>
        public GameEventWithDetailsImpl(GameEventType eventType, B details)
        {
            this.EventType = eventType;
            this._details = details;
        }

        /// <inheritdoc cref="IGameEvent.EventType"/>
        public GameEventType EventType { get; private set; }

        /// <inheritdoc cref="IGameEventWithDetails{B}.GetDetails{T}"/>
        public Option<T> GetDetails<T>()
        {
            try
            {
                return Option.Some<T>((T)Convert.ChangeType(this._details, typeof(T)));
            }
            catch (Exception)
            {
                return Option.None<T>();
            }
        }
    }
}
