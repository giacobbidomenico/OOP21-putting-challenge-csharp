using NUnit.Framework;
using PuttingChallenge.Colletta.Collisions;
using puttingchallenge.Fantilli.common;

namespace PuttingChallenge.Colletta.Test
{
    [TestFixture]
    class TestCollisions
    {
        private const double Radius = 10.0;
        private Point2D _pointA;
        private Point2D _pointB;
        private IPassiveCircleBoundingBox _passiveCircle;

        [SetUp]
        public void SetUp()
        {
            _pointA = new Point2D(0, 0);
            _pointB = new Point2D(15, 0);
            _passiveCircle = new ConcretePassiveCircleBoundingBox(_pointA, Radius);
        }

        [Test]
        public void CircleBoundingBoxIntersectionsTest()
        {
            IActiveBoundingBox circle = new CircleBoundingBox(_pointA, Radius);
            Assert.Equals(circle.ClosestPointOnBBToPoint(_pointB), new Point2D(10, 0));
            Assert.Equals(circle.IntersectionToSegment(_pointB, _pointA), new Point2D(10, 0));
            Assert.Equals(circle.GetNormal(circle.ClosestPointOnBBToPoint(_pointB)), new Vector2D(+1, 0));
            Assert.Equals(circle.IsColliding(_passiveCircle), true);
        }

        [Test]
        public void AABoundingBoxIntersectionsTest()
        {
            double height = 20;
            double width = 20;
            IActiveBoundingBox aABB = new AxisAlignedBoundingBox(_pointA, height, width);
            Assert.Equals(aABB.ClosestPointOnBBToPoint(_pointB), new Point2D(10, 0));
            Assert.Equals(aABB.IntersectionToSegment(_pointB, _pointA), new Point2D(10, 0));
            Assert.Equals(aABB.GetNormal(aABB.ClosestPointOnBBToPoint(_pointB)), new Vector2D(+1, 0));
            Assert.Equals(aABB.IsColliding(_passiveCircle), true);
        }

    }
}
