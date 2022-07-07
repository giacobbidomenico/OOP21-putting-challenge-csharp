﻿namespace putting_challenge.Fantilli
{
    using putting_challenge.Giacobbi;

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