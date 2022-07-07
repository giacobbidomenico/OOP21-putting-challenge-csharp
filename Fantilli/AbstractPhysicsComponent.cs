namespace Fantilli
{
    public abstract class AbstractPhysicsComponent : IPhysicsComponent
    {
        /// <inheritdoc cref="IPhysicsComponent.Velocity"/>
        public virtual Vector2D Velocity { get; set; }

        /// <inheritdoc cref="IPhysicsComponent.Update(long, IGameObject, Environment)"/>
        public abstract void Update(long dt, IGameObject obj, Environment env);
        
    }
}
