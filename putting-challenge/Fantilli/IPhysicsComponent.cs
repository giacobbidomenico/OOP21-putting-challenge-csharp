using putting_challenge.Giacobbi;

namespace Fantilli
{
    public interface IPhysicsComponent
    {
        /// <summary>
        /// Update the physical state of the provided game object.
        /// </summary>
        /// <param name="dt">elapsed time from the previous state</param>
        /// <param name="obj">the instance of <see cref="IGameObject"/> to update</param>
        /// <param name="env">environment of the game</param>
        void Update(long dt, IGameObject obj, IEnvironment env);

        /// <summary>
        /// Gets or sets the velocity of the object.
        /// </summary>
        Vector2D Velocity { get; }
    }
}
