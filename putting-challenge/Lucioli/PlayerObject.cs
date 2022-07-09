using putting_challenge.Lucioli;
using puttingchallenge.Fantilli.common;
using puttingchallenge.Fantilli.gameobjects;
using puttingchallenge.Fantilli.physics;
using PuttingChallenge.Colletta.Collisions;
using static puttingchallenge.Fantilli.gameobjects.IGameObject;

namespace PuttingChallenge.Lucioli
{
    public class PlayerObject : AbstractGameObject
    {
        private ConcreteDynamicBoundingBox _hitBox;
        public bool Flip
        {
            get => Flip;
            set => Flip = !Flip;
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
            Height = heigth;
        }

    }
}
