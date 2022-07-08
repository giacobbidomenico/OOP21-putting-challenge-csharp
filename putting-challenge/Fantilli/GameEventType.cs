namespace puttingchallenge.Fantilli
{
    /// <summary>
    /// Enumeration that establishes the various types of events occurred in the Application, 
    /// to exchange between Model, Controller and View.
    /// </summary>
    public enum GameEventType
    {
        /// <summary>
        /// Require to the application the start of the game.
        /// </summary>
        START,

        /// <summary>
        /// Notify the application that the game is over.
        /// </summary>
        GAMEOVER,

        /// <summary>
        /// Notify that the player shot the ball.
        /// </summary>
        SHOOT,

        /// <summary>
        /// Notify the application that the game is won.
        /// </summary>
        WIN,

        /// <summary>
        /// Require to the application to show the leader-board.
        /// </summary>
        SHOW_LEADERBOARD,

        /// <summary>
        /// Require to the application to show the main menu.
        /// </summary>
        SHOW_MAIN_MENU,

        /// <summary>
        /// Require to set a specified scene.
        /// </summary>
        SET_SCENE,

        /// <summary>
        /// Require to update the game statistics (lives and score).
        /// </summary>
        UPDATE_STATS,

        /// <summary>
        /// Require to shutdown the entire application.
        /// </summary>
        QUIT
    }
}
