using Optional;
using Optional.Unsafe;
using PuttingChallenge.Fantilli.Common;
using PuttingChallenge.Fantilli;

namespace PuttingChallenge.Colletta.Collisions
{
    /// <summary>
    /// It wraps an ActiveBoundingBox so that its collision can be tested to a PassiveCircleBoundingBox dynamically.
    /// </summary>
    public class ConcreteDynamicBoundingBox : IDynamicBoundingBox
    {
        private const long IntervalDelta = 2;
        private readonly IActiveBoundingBox _box;

        public ConcreteDynamicBoundingBox(IActiveBoundingBox box) => _box = box;

        /// <inheritdoc cref="IDynamicBoundingBox.CollidesWith"/>
        public IDynamicBoundingBox.ICollisionTest CollidesWith(
            PassiveCircleBBTrajectoryBuilder circleBuilder, 
            IPassiveCircleBoundingBox circle, 
            long dt)
        {
            Option<long> timeOfCollision = TestMovingCircle(circleBuilder, 0, dt);
            if (!timeOfCollision.HasValue)
            {
                return new ConcreteCollisionTest(_box);
            }
            Point2D lastPosition = circleBuilder.Build(timeOfCollision.ValueOrFailure()).Position;
            Point2D closestPoint = _box.ClosestPointOnBBToPoint(lastPosition);
            if (lastPosition == closestPoint)
            {
                closestPoint = _box.IntersectionToSegment(
                        circleBuilder.Build(0).Position,
                        lastPosition);
            }
            Vector2D normal = _box.GetNormal(closestPoint);
            return new ConcreteCollisionTest(_box, true, closestPoint, normal, lastPosition);
        }

        private Option<long> TestMovingCircle(PassiveCircleBBTrajectoryBuilder circleBuilder, long t1, long t2)
        {
            long mid = (t2 + t1) / 2;
            if (!_box.IsColliding(circleBuilder.Build(mid)))
            {
                return Option.None<long>();
            }

            if (t2 - t1 < IntervalDelta)
            {
                return Option.Some(t1);
            }
            Option<long> leftResult = TestMovingCircle(circleBuilder, t1, mid);
            return leftResult.HasValue ? leftResult : TestMovingCircle(circleBuilder, mid, t2);
        }

        /// <summary>
        /// Represents a concrete <see cref="IDynamicBoundingBox.ICollisionTest"/> between a 
        /// <see cref="PassiveCircleBoundingBox"/> and a <see cref="ActiveBoundingBox"/>.
        /// </summary>
        public class ConcreteCollisionTest : IDynamicBoundingBox.ICollisionTest
        {
            private readonly bool _hasCollided;
            private readonly IActiveBoundingBox _box;
            private readonly Point2D? _estimatedPointOfImpact;
            private readonly Vector2D? _normal;
            private readonly Point2D? _positionBeforeCollision;

            protected internal ConcreteCollisionTest(IActiveBoundingBox box,
                bool hasCollided, 
                Point2D estimatedPoint, 
                Vector2D normal, 
                Point2D position)
            {
                _box = box;
                _hasCollided = hasCollided;
                _estimatedPointOfImpact = estimatedPoint;
                _normal = normal;
                _positionBeforeCollision = position;
            }
            
            protected internal ConcreteCollisionTest(IActiveBoundingBox box)
            {
                _box = box;
                _hasCollided = false;
                _estimatedPointOfImpact = null;
                _normal = null;
                _positionBeforeCollision = null;
            }

            /// <inheritdoc cref="IDynamicBoundingBox.ICollisionTest.GetActiveBBSideNormal"/>
            public Option<Vector2D> GetActiveBBSideNormal()
            {
                return _normal == null ? Option.None<Vector2D>() : Option.Some(_normal);
            }

            /// <inheritdoc cref="IDynamicBoundingBox.ICollisionTest.GetActiveBBSideTanget"/>
            public Option<Vector2D> GetActiveBBSideTanget()
            {
                return _normal == null ? Option.None<Vector2D>() : Option.Some(new Vector2D(-_normal.Y, _normal.X));
            }

            /// <inheritdoc cref="IDynamicBoundingBox.ICollisionTest.GetActiveBoundingBox"/>
            public IActiveBoundingBox GetActiveBoundingBox()
            {
                return _box;
            }

            /// <inheritdoc cref="IDynamicBoundingBox.ICollisionTest.GetEstimatedPointOfImpact"/>
            public Option<Point2D> GetEstimatedPointOfImpact()
            {
                return _estimatedPointOfImpact == null ? Option.None<Point2D>() : Option.Some(_estimatedPointOfImpact);
            }

            /// <inheritdoc cref="IDynamicBoundingBox.ICollisionTest.GetPassiveBoxPositionBeforeCollisions"/>
            public Option<Point2D> GetPassiveBoxPositionBeforeCollisions()
            {
                return _positionBeforeCollision == null ? Option.None<Point2D>() : Option.Some(_positionBeforeCollision);
            }

            /// <inheritdoc cref="IDynamicBoundingBox.ICollisionTest.IsColliding"/>
            public bool IsColliding()
            {
                return _hasCollided;
            }
        }
    }
}
