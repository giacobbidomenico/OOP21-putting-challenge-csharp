using NUnit.Framework;
using PuttingChallenge.Colletta.Collisions;
using PuttingChallenge.Fantilli.Common;
using System;

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
            Assert.AreEqual(circle.ClosestPointOnBBToPoint(_pointB), new Point2D(10, 0));
            Assert.AreEqual(circle.ClosestPointOnBBToPoint(_pointA), _pointA);
            Assert.AreEqual(circle.IntersectionToSegment(_pointB, _pointA), new Point2D(10, 0));
            Assert.AreEqual(circle.GetNormal(circle.ClosestPointOnBBToPoint(_pointB)), new Vector2D(+1, 0));
            Assert.AreEqual(circle.IsColliding(_passiveCircle), true);
        }

        [Test]
        public void AABoundingBoxIntersectionsTest()
        {
            double height = 20;
            double width = 10;
            IActiveBoundingBox aABB = new AxisAlignedBoundingBox(_pointA, height, width);
            Assert.AreEqual(aABB.ClosestPointOnBBToPoint(_pointB), new Point2D(10, 0));
            Assert.AreEqual(aABB.IntersectionToSegment(_pointB, _pointA), new Point2D(10, 0));
            Assert.AreEqual(aABB.GetNormal(aABB.ClosestPointOnBBToPoint(_pointB)), new Vector2D(+1, 0));
            Assert.AreEqual(aABB.IsColliding(_passiveCircle), true);
        }

    }
}
