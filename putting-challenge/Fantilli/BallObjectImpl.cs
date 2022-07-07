namespace putting_challenge.Fantilli
{
    /// <summary>
    /// Class that represent the ball game object.
    /// </summary>
    public class BallObjectImpl : AbstractGameObject
    {
        private readonly PassiveCircleBoundingBox _hitBox;

        /// <summary>
        /// Build a new <see cref="BallObjectImpl"/>.
        /// </summary>
        /// <param name="type">type of the game object</param>
        /// <param name="position">position of the game object</param>
        /// <param name="phys">physical component of the game object</param>
        /// <param name="hitBox">the hit-box of the object</param>
        public GameObjectImpl(IGameObject.GameObjectType type,
                              Point2D position,
                              IPhysicsComponent phys,
                              PassiveCircleBoundingBox hitBox) : base(type, position, phys)
        {
            this._hitBox = hitBox;
        }

        /// <inheritdoc cref="IGameObject.Position"/>
        public override Point2D Position 
        {
            get => base.Position;
            set
            {
                base.Position = value;
                this._hitBox.Position = value;
            }
        }

        /// <summary>
        /// Gets the hit-box of the ball.
        /// </summary>
        public PassiveCircleBoundingBox HitBox { get => this._hitBox; }
    }
}
