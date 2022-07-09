using System;
using puttingchallenge.Fantilli.common;
using Optional;
using Optional.Unsafe;

namespace PuttingChallenge.Colletta.Collisions
{
    /// <summary>
    /// Represents a rectangle bounding box whose sides are parallel to the axis of the plane.
    /// </summary>
    public class AxisAlignedBoundingBox : IActiveBoundingBox
    {
        private readonly Point2D _minimumVertex;
        private readonly Point2D _maximumVertex;

        /// <summary>
        /// Builds a <see cref="AxisAlignedBoundingBox"/> given two vertices.
        /// </summary>
        /// <param name="minimum">The down-left vertex of the rectangle.</param>
        /// <param name="maximum">The up-right vertex of the rectangle.</param>
        public AxisAlignedBoundingBox(Point2D minimum, Point2D maximum)
        {
            _minimumVertex = minimum;
            _maximumVertex = maximum;
        }

        /// <summary>
        /// Builds a <see cref="AxisAlignedBoundingBox"/> given the up-left vertex and its dimensions-
        /// </summary>
        /// <param name="upLeft">The up-left vertex.</param>
        /// <param name="height">The height of the rectangole.</param>
        /// <param name="width">The width of the rectangle.</param>
        public AxisAlignedBoundingBox(Point2D upLeft, double height, double width)
        {
            _minimumVertex = new Point2D(upLeft.X, upLeft.Y + height);
            _maximumVertex = new Point2D(upLeft.X + width, upLeft.Y);
        }

        /// <inheritdoc cref="IActiveBoundingBox.BounceAlongTangent"/>
        public bool BounceAlongTangent() => false;

        /// <inheritdoc cref="IActiveBoundingBox.ClosestPointOnBBToPoint"/>
        public Point2D ClosestPointOnBBToPoint(Point2D point)
        {
            Point2D closestPoint = new Point2D(point.X, point.Y);
            if (closestPoint.X < _minimumVertex.X)
            {
                closestPoint = new Point2D(_minimumVertex.X, closestPoint.Y);
            }
            if (closestPoint.X > _maximumVertex.X)
            {
                closestPoint = new Point2D(_maximumVertex.X, closestPoint.Y);
            }
            if (closestPoint.Y > _minimumVertex.Y)
            {
                closestPoint = new Point2D(closestPoint.X, _minimumVertex.Y);
            }
            if (closestPoint.Y < _maximumVertex.Y)
            {
                closestPoint = new Point2D(closestPoint.X, _maximumVertex.Y);
            }
            return closestPoint;
        }

        /// <inheritdoc cref="IActiveBoundingBox.GetNormal"/>
        public Vector2D GetNormal(Point2D pointOnActiveBoundingBox)
        {
            if (pointOnActiveBoundingBox.X == _minimumVertex.X)
            {
                return new Vector2D(-1, 0);
            }
            if (pointOnActiveBoundingBox.Y == _minimumVertex.Y)
            {
                return new Vector2D(0, 1);
            }
            if (pointOnActiveBoundingBox.X == _maximumVertex.X)
            {
                return new Vector2D(1, 0);
            }
            if (pointOnActiveBoundingBox.Y == _maximumVertex.Y)
            {
                return new Vector2D(0, -1);
            }
            throw new ArgumentException();
        }

        /// <inheritdoc cref="IActiveBoundingBox.IntersectionToSegment"/>
        public Point2D IntersectionToSegment(Point2D pointA, Point2D pointB)
        {
            if (ClosestPointOnBBToPoint(pointA) == pointA || ClosestPointOnBBToPoint(pointB) != pointB)
            {
                throw new ArgumentException();
            }
            Point2D closestPoint = ClosestPointOnBBToPoint(pointA);
            if (pointA.X == pointB.X || pointA.Y == pointB.Y)
            {
                return closestPoint;
            }
            Point2D upLeft = new Point2D(_minimumVertex.X, _maximumVertex.Y);
            Point2D bottomRight = new Point2D(_maximumVertex.X, _minimumVertex.Y);

            Option<Point2D> result = LineLineIntersection(pointA, pointB, _maximumVertex, bottomRight);
            if (result.HasValue)
            {
                return result.ValueOrFailure();
            }
            result = LineLineIntersection(pointA, pointB, _minimumVertex, bottomRight);
            if (result.HasValue)
            {
                return result.ValueOrFailure();
            }
            result = LineLineIntersection(pointA, pointB, upLeft, _minimumVertex);
            if (result.HasValue)
            {
                return result.ValueOrFailure();
            }
            result = LineLineIntersection(pointA, pointB, upLeft, _maximumVertex);
            return result.ValueOrFailure();
        }

        /// <inheritdoc cref="IActiveBoundingBox.IsColliding"/>
        public bool IsColliding(IPassiveCircleBoundingBox circle)
        {
            Point2D closestPointOnAABB = ClosestPointOnBBToPoint(circle.Position);
            return circle.Radius >= Point2D.GetDistance(closestPointOnAABB, circle.Position);
        }

        private Option<Point2D> LineLineIntersection(Point2D pointA1,
            Point2D pointA2,
            Point2D pointB1,
            Point2D pointB2)
        {
            Vector2D b = new Vector2D(pointA2.X - pointA1.X, pointA2.Y - pointA1.Y);
            Vector2D d = new Vector2D(pointB2.X - pointB1.X, pointB2.Y - pointB1.Y);
            double cross = b.X * d.Y - b.Y * d.X;

            if (cross == 0)
            {
                return Option.None<Point2D>();
            }
            Vector2D c = new Vector2D(pointB1.X - pointA1.X, pointB1.Y - pointA1.Y);
            double t = (c.X * d.Y - c.Y * d.X) / cross;
            if (t < 0 || t > 1)
            {
                return Option.None<Point2D>();
            }
            double u = (c.X * b.Y - c.Y * b.X) / cross;
            if (u < 0 || u > 1)
            {
                return Option.None<Point2D>();
            }

            return Option.Some(new Point2D(pointA1.X + (t * b.X), pointA1.Y + (t * b.Y)));
        }
    }
}
