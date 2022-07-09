using Optional;
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
        private readonly IActiveBoundingBox _box;

        public ConcreteDynamicBoundingBox(IActiveBoundingBox box) => _box = box;

        /// <inheritdoc cref="IDynamicBoundingBox.CollidesWith"/>
        public IDynamicBoundingBox.ICollisionTest CollidesWith(IPassiveCircleBoundingBox circle, long dt)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Represents a concrete <see cref="IDynamicBoundingBox.ICollisionTest"/> between a 
        /// <see cref="PassiveCircleBoundingBox"/> and a <see cref="ActiveBoundingBox"/>.
        /// </summary>
        public class ConcreteCollisionTest : IDynamicBoundingBox.ICollisionTest
        {
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
