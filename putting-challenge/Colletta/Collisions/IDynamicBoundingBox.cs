using System;
using System.Collections.Generic;
using System.Text;
using Optional;
using Optional.Unsafe;
using puttingchallenge.Fantilli.common;

namespace PuttingChallenge.Colletta.Collisions
{
    /// <summary>
    /// Represents a wrapprer for an <see cref="IActiveBoundingBox"/>
    /// </summary>
    public interface IDynamicBoundingBox
    {
        /// <summary>
        /// Perform a dynamic intersection test between the wrapped object 
        /// and a <see cref="IPassiveCircleBoundingBox"/>
        /// </summary>
        /// <param name="circle">Bounding box to perform the test with.</param>
        /// <param name="dt">Time from last frame.</param>
        /// <returns>A CollisionTest with the details about the test.</returns>
        ICollisionTest CollidesWith(IPassiveCircleBoundingBox circle, long dt);

        public interface ICollisionTest
        {
            Boolean IsColliding();

            IActiveBoundingBox GetActiveBoundingBox();

            Option<Point2D> GetEstimatedPointOfImpact();

            Option<Vector2D> GetActiveBBSideNormal();

            Option<Vector2D> GetActiveBBSideTanget();
            
            Option<Point2D> GetPassiveBoxPositionBeforeCollisions();
        }
    }
}
