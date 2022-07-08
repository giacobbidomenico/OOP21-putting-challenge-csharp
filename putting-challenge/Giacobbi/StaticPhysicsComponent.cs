using Fantilli;
using System;

namespace puttingchallenge_Giacobbi
{

    /// <summary>
    /// Describes the physical behavior of a fixed <see cref="IGameObject"/>.
    /// </summary>
    public class StaticPhysicsComponent : IPhysicsComponent
    {

        /// <inheritdoc/>
        public Vector2D Velocity { get => new Vector2D(0, 0); }

        /// <inheritdoc/>
        public void Update(long dt, IGameObject obj, IEnvironment env)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public override int GetHashCode() => HashCode.Combine(new Vector2D(0,0));

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            if (this == obj)
            {
                return true;
            }
            if (obj == null)
            {
                return false;
            }
            if (obj is IPhysicsComponent phys)
            {
                return phys.Velocity.Equals(new Vector2D(0, 0));
            }
            return false;
        }
    }
}
