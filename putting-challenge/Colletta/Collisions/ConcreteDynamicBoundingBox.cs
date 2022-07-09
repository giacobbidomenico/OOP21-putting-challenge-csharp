using Optional;
using Optional.Unsafe;
using puttingchallenge.Fantilli.common;
using System;
using System.Collections.Generic;
using System.Text;

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
        public IDynamicBoundingBox.ICollisionTest CollidesWith(IPassiveCircleBoundingBox circle, long dt)
        {
            throw new NotImplementedException();
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
            if (leftResult.HasValue)
            {
                return leftResult;
            }
            return TestMovingCircle(circleBuilder, mid, t2);
        }

        /// <summary>
        /// Represents a concrete <see cref="IDynamicBoundingBox.ICollisionTest"/> between a 
        /// <see cref="PassiveCircleBoundingBox"/> and a <see cref="ActiveBoundingBox"/>.
        /// </summary>
        public class ConcreteCollisionTest : IDynamicBoundingBox.ICollisionTest
        {
            private readonly bool _hasCollided;
            private readonly Point2D? _estimatedPointOfImpact;
            private readonly Vector2D? _normal;
            private readonly Point2D? _positionBeforeCollision;

            private ConcreteCollisionTest(bool hasCollided, Point2D estimatedPoint, Vector2D normal, Point2D position)
            {
                _hasCollided = hasCollided;
                _estimatedPointOfImpact = estimatedPoint;
                _normal = normal;
                _positionBeforeCollision = position;
            }
            
            private ConcreteCollisionTest()
            {
                _hasCollided = false;
                _estimatedPointOfImpact = null;
                _normal = null;
                _positionBeforeCollision = null;
            }

            /// <inheritdoc cref="IDynamicBoundingBox.ICollisionTest.GetActiveBBSideNormal"/>
            public Option<Vector2D> GetActiveBBSideNormal()
            {
                throw new NotImplementedException();
            }

            /// <inheritdoc cref="IDynamicBoundingBox.ICollisionTest.GetActiveBBSideTanget"/>
            public Option<Vector2D> GetActiveBBSideTanget()
            {
                throw new NotImplementedException();
            }

            /// <inheritdoc cref="IDynamicBoundingBox.ICollisionTest.GetActiveBoundingBox"/>
            public IActiveBoundingBox GetActiveBoundingBox()
            {
                throw new NotImplementedException();
            }

            /// <inheritdoc cref="IDynamicBoundingBox.ICollisionTest.GetEstimatedPointOfImpact"/>
            public Option<Point2D> GetEstimatedPointOfImpact()
            {
                throw new NotImplementedException();
            }

            /// <inheritdoc cref="IDynamicBoundingBox.ICollisionTest.GetPassiveBoxPositionBeforeCollisions"/>
            public Option<Point2D> GetPassiveBoxPositionBeforeCollisions()
            {
                throw new NotImplementedException();
            }

            /// <inheritdoc cref="IDynamicBoundingBox.ICollisionTest.IsColliding"/>
            public bool IsColliding()
            {
                throw new NotImplementedException();
            }
        }
    }
}
