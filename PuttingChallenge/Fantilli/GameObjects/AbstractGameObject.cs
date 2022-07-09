namespace PuttingChallenge.Fantilli.GameObjects
{
    using System;
    using PuttingChallenge.Fantilli.Common;
    using PuttingChallenge.Fantilli.Physics;
    using PuttingChallenge.Giacobbi;

    /// <summary>
    /// Abstract class that define a generic <see cref="IGameObject"/>.
    /// </summary>
    public abstract class AbstractGameObject : IGameObject
    {
        private readonly IGameObject.GameObjectType _type;
        private readonly IPhysicsComponent _phys;

        /// <summary>
        /// Set up a new <see cref="AbstractGameObject"/>.
        /// </summary>
        /// <param name="type">type of the game object</param>
        /// <param name="position">position of the game object</param>
        /// <param name="phys">physical component of the game object</param>
        public AbstractGameObject(IGameObject.GameObjectType type,
                                  Point2D position,
                                  IPhysicsComponent phys)
        {
            _type = type;
            Position = position;
            _phys = phys;
        }

        /// <inheritdoc cref="IGameObject.Position"/>
        public virtual Point2D Position { get; set; }

        /// <inheritdoc cref="IGameObject.Velocity"/>
        public Vector2D Velocity
        {
            get => _phys.Velocity;
            set => _phys.Velocity = value;
        }

        /// <inheritdoc cref="IGameObject.Type"/>
        public IGameObject.GameObjectType Type { get => _type; }

        /// <inheritdoc cref="IGameObject.PhysicsComponent"/>
        public IPhysicsComponent PhysicsComponent { get => _phys; }

        /// <inheritdoc cref="IGameObject.UpdatePhysics(long, IEnvironment)"/>
        public void UpdatePhysics(long dt, IEnvironment env) =>
            _phys.Update(dt, this, env);

        /// <inheritdoc cref="IGameObject.UpdatePhysics(long, Environment)"/>
        public override int GetHashCode() => HashCode.Combine(_type, Position, Velocity);

        /// <inheritdoc cref="object.Equals(object?)"/>
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj.GetType() != GetType()) return false;
            return Equals(obj as AbstractGameObject);
        }

        /// <summary>
        /// Compares this instance with given <paramref name="obj"/>.
        /// The result is true if and only if the argument is not null and is 
        /// an instance of <see cref="AbstractGameObject"/> and contains the same components.
        /// </summary>
        /// <param name="obj">the game object to compare</param>
        /// <returns>true if the given object is equal to this, false otherwise</returns>
        public bool Equals(AbstractGameObject obj)
        {
            if (this == obj)
            {
                return true;
            }
            else
            {
                return Position.Equals(obj.Position)
                       && _phys.Equals(obj.PhysicsComponent)
                       && _type.Equals(obj.Type);
            }
        }
    }
}
