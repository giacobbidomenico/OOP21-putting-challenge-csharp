using puttingchallenge.Fantilli.common;
using System;
using System.Collections.Generic;
using System.Text;

namespace PuttingChallenge.Colletta.Collisions
{
    /// <summary>
    /// Represents an active circle bounding box.
    /// </summary>    
    public class CircleBoundingBox : IActiveBoundingBox
    {
        private readonly Point2D _centerPosition;
        private readonly double _radius;

        /// <summary>
        /// Builds a <see cref="CircleBoundingBox"/>.
        /// </summary>
        /// <param name="position">Position of the center of the bounding box,</param>
        /// <param name="radius">Radius of the boudning box</param>
        public CircleBoundingBox(Point2D position, double radius)
        {
            _centerPosition = position;
            _radius = radius;
        }

        /// <inheritdoc cref="IActiveBoundingBox.BounceAlongTangent"/>
        public bool BounceAlongTangent() => true;

        /// <inheritdoc cref="IActiveBoundingBox.ClosestPointOnBBToPoint"/>
        public Point2D ClosestPointOnBBToPoint(Point2D point)
        {
            Point2D diff = new Point2D(
                point.X - _centerPosition.X,
                point.Y - _centerPosition.Y);

            double module = Math.Sqrt(diff.X * diff.X + diff.Y * diff.Y);

            return new Point2D(
                    _centerPosition.X + (diff.X / module * _radius),
                    _centerPosition.Y + (diff.Y / module * _radius));
        }

        /// <inheritdoc cref="IActiveBoundingBox.GetNormal"/>
        public Vector2D GetNormal(Point2D pointOnActiveBoundingBox)
        {
            double x = -_centerPosition.X + pointOnActiveBoundingBox.X;
            double y = -_centerPosition.Y + pointOnActiveBoundingBox.Y;
            return new Vector2D(x / Math.Sqrt(y * y + x * x), y / Math.Sqrt(y * y + x * x));
        }

        /// <inheritdoc cref="IActiveBoundingBox.IntersectionToSegment"/>
        public Point2D IntersectionToSegment(Point2D pointA, Point2D pointB)
        {
            if (ClosestPointOnBBToPoint(pointA) == pointA || ClosestPointOnBBToPoint(pointB) != pointB)
            {
                throw new ArgumentException();
            }
            Vector2D m = new Vector2D(pointA.X - _centerPosition.X,
                    pointA.Y - _centerPosition.Y);

            Vector2D segmentVector = new Vector2D(pointB.X - pointA.X,
                    pointB.Y - pointA.Y);

            Vector2D normalizedSegmentVector = new Vector2D(segmentVector.X / segmentVector.GetModule(),
                    segmentVector.Y / segmentVector.GetModule());

            double b = m.DotProduct(normalizedSegmentVector);
            double c = m.DotProduct(m) - Math.Pow(_radius, 2);
            double t1 = -b - Math.Sqrt(Math.Pow(b, 2) - c);
            double resultX = pointA.X + normalizedSegmentVector.X * t1;
            double resultY = pointA.Y + normalizedSegmentVector.Y * t1;
            return new Point2D(resultX, resultY);
        }

        /// <inheritdoc cref="IActiveBoundingBox.IsColliding"/>
        public bool IsColliding(IPassiveCircleBoundingBox circle)
        {
            return _radius + circle.Radius >= Point2D.GetDistance(_centerPosition, circle.Position);
        }
    }
}
