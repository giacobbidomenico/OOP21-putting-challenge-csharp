namespace PuttingChallenge.Fantilli.GameObjects
{
    using PuttingChallenge.Fantilli.Common;
    using PuttingChallenge.Fantilli.Physics;
    using PuttingChallenge.Colletta.Collisions;

    /// <summary>
    /// Class that represent the ball game object.
    /// </summary>
    public class BallObjectImpl : AbstractGameObject
    {
        private readonly IPassiveCircleBoundingBox _hitBox;

        /// <summary>
        /// Build a new <see cref="BallObjectImpl"/>.
        /// </summary>
        /// <param name="type">type of the game object</param>
        /// <param name="position">position of the game object</param>
        /// <param name="phys">physical component of the game object</param>
        /// <param name="hitBox">the hit-box of the object</param>
        public BallObjectImpl(IGameObject.GameObjectType type,
                              Point2D position,
                              IPhysicsComponent phys,
                              IPassiveCircleBoundingBox hitBox) : base(type, position, phys)
        {
            _hitBox = hitBox;
        }

        /// <inheritdoc cref="IGameObject.Position"/>
        public Point2D Position
        {
            get => base.Position;
            set
            {
                base.Position = value;
                _hitBox.Position = value;
            }
        }

        /// <summary>
        /// Gets the hit-box of the ball.
        /// </summary>
        public IPassiveCircleBoundingBox HitBox { get => _hitBox; }
    }
}
