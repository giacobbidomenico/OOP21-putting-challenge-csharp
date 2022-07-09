using PuttingChallenge.Fantilli.Common;
using PuttingChallenge.Fantilli.GameObjects;
using PuttingChallenge.Fantilli.Physics;
using PuttingChallenge.Colletta.Collisions;
using static PuttingChallenge.Fantilli.GameObjects.IGameObject;

namespace PuttingChallenge.Lucioli
{
    public class PlayerObject : AbstractGameObject
    {
        private ConcreteDynamicBoundingBox _hitBox;
        public bool Flip
        {
            get;
            set;
        }
        public Bat BatInUse
        {
            get;
            set;
        }
        public double Width
        {
            get;
            private set;
        }
        public double Height
        {
            get;
            private set;
        }

        public void InvertFlip()
        {
            Flip = !Flip;
        }

        public PlayerObject(GameObjectType objectType,
            Point2D position,
            IPhysicsComponent phys,
            ConcreteDynamicBoundingBox concreteDynamicBoundingBox,
            double width, 
            double heigth) : base(objectType, position, phys)
        {
            BatInUse = new Bat(BatType.Hybrid);
            _hitBox = concreteDynamicBoundingBox;
            Width = width;
            Flip = false;
            Height = heigth;
        }

    }
}
