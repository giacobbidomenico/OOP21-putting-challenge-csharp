namespace puttingchallenge.Fantilli
{
    using System;
    using puttingchallenge.Giacobbi;

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
            this._type = type;
            this.Position = position;
            this._phys = phys;
        }

        /// <inheritdoc cref="IGameObject.Position"/>
        public virtual Point2D Position { get; set; }

        /// <inheritdoc cref="IGameObject.Velocity"/>
        public Vector2D Velocity 
        { 
            get => this._phys.Velocity; 
            set => this._phys.Velocity = value; 
        }

        /// <inheritdoc cref="IGameObject.Type"/>
        public IGameObject.GameObjectType Type { get => this._type; }

        /// <inheritdoc cref="IGameObject.PhysicsComponent"/>
        public IPhysicsComponent PhysicsComponent { get => this._phys; }

        /// <inheritdoc cref="IGameObject.UpdatePhysics(long, IEnvironment)"/>
        public void UpdatePhysics(long dt, IEnvironment env) =>
            this._phys.Update(dt, this, env);

        /// <inheritdoc cref="IGameObject.UpdatePhysics(long, Environment)"/>
        public override int GetHashCode() => HashCode.Combine(this._type, this.Position, this.Velocity);

        /// <inheritdoc cref="object.Equals(object?)"/>
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj.GetType() != this.GetType()) return false;
            return this.Equals(obj as AbstractGameObject);
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
                return this.Position.Equals(obj.Position)
                       && this._phys.Equals(obj.PhysicsComponent)
                       && this._type.Equals(obj.Type);
            }
        }
    }
}
