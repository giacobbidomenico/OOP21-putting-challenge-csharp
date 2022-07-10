namespace PuttingChallenge.Fantilli.Physics
{
    using PuttingChallenge.Giacobbi.Environment;
    using PuttingChallenge.Fantilli.Common;
    using PuttingChallenge.Fantilli.GameObjects;
    using System;

    /// <summary>
    /// Abstract class that describes the physics of moving <see cref="IGameObject"/>.
    /// </summary>
    public abstract class AbstractPhysicsComponent : IPhysicsComponent
    {
        /// <inheritdoc cref="IPhysicsComponent.Velocity"/>
        public virtual Vector2D Velocity { get; set; }

        /// <inheritdoc cref="IPhysicsComponent.Update(long, IGameObject, IEnvironment)"/>
        public abstract void Update(long dt, IGameObject obj, IEnvironment env);

        /// <inheritdoc cref="object.Equals(object?)"/>
        public override bool Equals(object? obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (this == obj)
            {
                return true;
            }
            if (obj is IPhysicsComponent physics)
            {
                return this.Velocity.Equals(physics.Velocity);
            }
            return false;
        }

        /// <inheritdoc/>
        public override int GetHashCode() => HashCode.Combine(Velocity);
    }
}
