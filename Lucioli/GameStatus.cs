namespace OOP21-putting-challenge-csharp.Lucioli
{
    public enum GameStatus
    {
        /// <summary>
        /// The player started the game.
        /// </summary>
        Play,
        /// <summary>
        /// The player runs out of lives.
        /// </summary>
        GameOver,
        /// <summary>
        /// The player won every single map in the game.
        /// </summary>
        GameWin,
        /// <summary>
        /// The user consults the leaderboard.
        /// </summary>
        Leaderboard,
        /// <summary>
        /// The player quits the game or has just started the application.
        /// </summary>
        MainMenu
    }
}