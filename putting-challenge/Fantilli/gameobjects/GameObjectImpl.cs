namespace puttingchallenge.Fantilli.gameobjects
{
    using puttingchallenge.Fantilli.common;
    using puttingchallenge.Fantilli.physics;

    /// <summary>
    /// Class that implements an object of the game.
    /// </summary>
    public class GameObjectImpl : AbstractGameObject
    {
        private readonly DynamicBoundingBox _hitBox;

        /// <summary>
        /// Build a new <see cref="GameObjectImpl"/>.
        /// </summary>
        /// <param name="type">type of the game object</param>
        /// <param name="position">position of the game object</param>
        /// <param name="phys">physical component of the game object</param>
        /// <param name="hitBox">the hit-box of the object</param>
        public GameObjectImpl(IGameObject.GameObjectType type,
                              Point2D position,
                              IPhysicsComponent phys,
                              DynamicBoundingBox hitBox) : base(type, position, phys)
        {
            _hitBox = hitBox;
        }

        /// <summary>
        /// Gets the hit-box of the object.
        /// </summary>
        public DynamicBoundingBox HitBox { get => _hitBox; }
    }
}
