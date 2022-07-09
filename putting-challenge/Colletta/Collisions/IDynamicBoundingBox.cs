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
        ICollisionTest CollidesWith(PassiveCircleBBTrajectoryBuilder circleBuilder, IPassiveCircleBoundingBox circle, long dt);

        /// <summary>
        /// Represents a collision test between an active bounding box and a passive bounding box.
        /// </summary>
        public interface ICollisionTest
        {
            /// <summary>
            /// </summary>
            /// <returns>true if the collision has occurred, false otherwise.</returns>
            Boolean IsColliding();

            /// <summary>
            /// </summary>
            /// <returns>The active bounding box related to the test.</returns>
            IActiveBoundingBox GetActiveBoundingBox();

            /// <summary>
            /// </summary>
            /// <returns>The estimated point of impact, empty if the collision has not occurred.</returns>
            Option<Point2D> GetEstimatedPointOfImpact();

            /// <summary>
            /// </summary>
            /// <returns>The normal of the colliding side of the passive bounding box.</returns>
            Option<Vector2D> GetActiveBBSideNormal();

            /// <summary>
            /// </summary>
            /// <returns>The tangent to the point of collision.</returns>
            Option<Vector2D> GetActiveBBSideTanget();
            
            /// <summary>
            /// </summary>
            /// <returns>The estimated position of the passive bounding box at the first time of collision, 
            /// empty if the collision has not occurred.</returns>
            Option<Point2D> GetPassiveBoxPositionBeforeCollisions();
        }
    }
}
