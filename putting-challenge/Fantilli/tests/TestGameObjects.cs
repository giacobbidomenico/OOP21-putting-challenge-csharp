namespace puttingchallenge.Fantilli.tests
{
    using NUnit.Framework;
    using puttingchallenge.Fantilli.gameobjects;
    using puttingchallenge.Fantilli.common;
    using puttingchallenge.Giacobbi;

    [TestFixture]
    public class TestGameObjects
    {
        private const double X = 20.0;
        private const double Y = 30.0;

        private GameObjectImpl _gameObject;
        private IGameObject.GameObjectType _type;
        private Point2D _pos;
        private StaticPhysicsComponent _phys;
        private DynamicBoundingBox _hitBox;

        [SetUp]
        public void SetUp()
        {
            this._type = IGameObject.GameObjectType.WALL;
            this._pos = new Point2D(X, Y);
            this._phys = new StaticPhysicsComponent();
            this._hitbox = new DynamicBoundingBox;
            this._gameObject = new GameObjectImpl(this._type,
                                                  this._pos,
                                                  this._phys,
                                                  this._hitBox);
        }

        [Test]
        public void PositionTest()
        {
            Assert.AreEqual(this._gameObject.Position, this._pos);
            Point2D newPos = new Point2D(X + 30, Y);
            this._gameObject.Position = newPos;
            Assert.AreEqual(this._gameObject.Position, newPos);
            this._gameObject.UpdatePhysics(1000, null);
            Assert.AreEqual(this._gameObject.Position, newPos);
        }

        [Test]
        public void VelocityTest()
        {
            Assert.AreEqual(this._gameObject.Velocity, new Vector2D(0, 0));
            Vector2D newVel = new Vector2D(X, Y);
            this._gameObject.Velocity = newVel;
            Assert.AreEqual(this._gameObject.Position, new Vector2D(0, 0));
        }

        [Test]
        public void ComponentTest()
        {
            Assert.AreEqual(this._gameObject.Type, this._type);
            Assert.AreEqual(this._gameObject.PhysicsComponent, this._phys);
            Assert.AreEqual(this._gameObject.HitBox, this._hitBox);
        }
    }
}
