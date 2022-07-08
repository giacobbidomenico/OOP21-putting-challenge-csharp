namespace putting_challenge.Lucioli
{
    public interface IGameStateManager : Colleague
    {
        /// <summary>
        /// Sets the initial state of the game.
        /// </summary>
        public void InitState();

        /// <summary>
        /// Sets a new current <see cref="GameState"/> and send the <see cref="GameEvent"/>  to set the appropriate scene.
        /// </summary>
        /// <param name="status">is the status to switch to.</param>
        public void SwitchState(GameStatus status);

        /// <summary>
        /// This method updates the physics state of the <see cref="GameObject"/>s.
        /// </summary>
        /// <param name="dt"></param>
        public void Update(long dt);

    }
}