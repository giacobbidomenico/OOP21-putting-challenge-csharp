namespace PuttingChallenge.Giacobbi.Events
{
    /// <summary>
    /// Enumeration for the different event types notified 
    /// from the GameState to the Environment and vice-versa.
    /// </summary>
    public enum ModelEventType
    {
        /// <summary>
        /// The ball stopped inside the map. 
        /// </summary>
        BALL_STOPPED,

        /// <summary>
        /// The ball stopped in the hole.
        /// </summary>
        BALL_IN_HOLE,

        /// <summary>
        /// The ball is outside the map.
        /// </summary>
        BALL_OUT_OF_BOUND,

        /// <summary>
        /// The player shot the ball. 
        /// </summary>
        SHOOT,

        /// <summary>
        /// The player has to move next to the ball in order to re-try.
        /// </summary>
        MOVE_PLAYER
    }
}
