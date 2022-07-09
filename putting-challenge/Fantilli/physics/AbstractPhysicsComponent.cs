namespace PuttingChallenge.Fantilli.Physics
{
    using PuttingChallenge.Giacobbi;
    using PuttingChallenge.Fantilli.Common;
    using PuttingChallenge.Fantilli.GameObjects;

    /// <summary>
    /// Abstract class that describes the physics of moving <see cref="IGameObject"/>.
    /// </summary>
    public abstract class AbstractPhysicsComponent : IPhysicsComponent
    {
        /// <inheritdoc cref="IPhysicsComponent.Velocity"/>
        public virtual Vector2D Velocity { get; set; }

        /// <inheritdoc cref="IPhysicsComponent.Update(long, IGameObject, IEnvironment)"/>
        public abstract void Update(long dt, IGameObject obj, IEnvironment env);

    }
}
