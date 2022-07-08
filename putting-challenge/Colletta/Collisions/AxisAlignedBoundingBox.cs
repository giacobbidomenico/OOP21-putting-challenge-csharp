using System;
using System.Collections.Generic;
using System.Text;

namespace PuttingChallenge.Colletta.Collisions
{
    class AxisAlignedBoundingBox : IActiveBoundingBox
    {
        /// <inheritdoc cref="IActiveBoundingBox.BounceAlongTangent"/>
        public bool BounceAlongTangent()
        {
        }

        /// <inheritdoc cref="IActiveBoundingBox.ClosestPointOnBBToPoint(Point2D)"/>
        public Point2D ClosestPointOnBBToPoint(Point2D point)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc cref="IActiveBoundingBox.GetNormal(Point2D)"/>
        public Vector2D GetNormal(Point2D pointOnActiveBoundingBox)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc cref="IActiveBoundingBox.IntersectionToSegment(Point2D, Point2D)"/>
        public Point2D IntersectionToSegment(Point2D pointA, Point2D pointB)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc cref="IActiveBoundingBox.IsColliding(IPassiveCircleBoundingBox)"/>
        public bool IsColliding(IPassiveCircleBoundingBox circle)
        {
            throw new NotImplementedException();
        }
    }
}
