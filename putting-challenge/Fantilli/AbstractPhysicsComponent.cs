namespace putting_challenge.Fantilli
{
    using putting_challenge.Giacobbi;

    public abstract class AbstractPhysicsComponent : IPhysicsComponent
    {
        /// <inheritdoc cref="IPhysicsComponent.Velocity"/>
        public virtual Vector2D Velocity { get; set; }

        /// <inheritdoc cref="IPhysicsComponent.Update(long, IGameObject, IEnvironment)"/>
        public abstract void Update(long dt, IGameObject obj, IEnvironment env);
        
    }
}
