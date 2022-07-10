namespace PuttingChallenge.Fantilli.GameObjects
{
    using System;
    using PuttingChallenge.Fantilli.Common;
    using PuttingChallenge.Fantilli.Physics;
    using PuttingChallenge.Giacobbi.Environment;

    /// <summary>
    /// Abstract class that define a generic <see cref="IGameObject"/>.
    /// </summary>
    public abstract class AbstractGameObject : IGameObject
    {
        private readonly IGameObject.GameObjectType _type;
        private readonly IPhysicsComponent _phys;
        private Point2D _position;

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
            _position = position;
            _phys = phys;
        }

        /// <inheritdoc cref="IGameObject.Position"/>
        public Point2D Position { get => _position; set => _position = value; }

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
        public override bool Equals(object? obj)
        {
            if(obj == null)
            {
                return false;
            }

            if (this == obj)
            {
                return true;
            }

            if(obj is IGameObject gameObject)
            {
                return Position.Equals(gameObject.Position)
                       && _phys.Equals(gameObject.PhysicsComponent)
                       && _type.Equals(gameObject.Type);
            }
            
            return false;
        }
    }
}
