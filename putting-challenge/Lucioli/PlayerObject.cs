namespace Lucioli
{
    public class PlayerObject : GameObject
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
            GraphicComponent graph,
            PhysicsComponent phys,
            ConcreteDynamicBoundingBox concreteDynamicBoundingBox,
            double width, 
            double heigth) : base(objectType, position, graph, phys)
        {
            BatInUse = new Bat(BatType.Hybrid);
            _hitBox = concreteDynamicBoundingBox;
            Width = width;
            Height = heigth;
        }

    }
}
