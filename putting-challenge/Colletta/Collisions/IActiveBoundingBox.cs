using putting_challenge.Fantilli;

namespace PuttingChallenge.Colletta.Collisions
{
    /// <summary>
    /// Defines a bounding box capable of performing intersection tests, hence Active.
    /// </summary>
    public interface IActiveBoundingBox
    {

        /// <summary>
        /// Perform an intersection test with a <see cref="IPassiveCircleBoundingBox"/>.
        /// </summary>
        /// <param name="circle">The object to test with.</param>
        /// <returns>True if a collision is occurred, false otherwise.</returns>
        bool IsColliding(IPassiveCircleBoundingBox circle);

        /// <summary>
        /// </summary>
        /// <returns>True if an object colliding would bounce along the tangent, false otherwise</returns>
        bool BounceAlongTangent();

        /// <summary>
        /// </summary>
        /// <param name="pointOnActiveBoundingBox">The point on the bounding box.</param>
        /// <returns>The normal vector to the point passed</returns>
        Vector2D GetNormal(Point2D pointOnActiveBoundingBox);

        /// <summary>
        /// </summary>
        /// <param name="point">The point to be projected to the bounding box.</param>
        /// <returns>The closest point on the bounding box to point.</returns>
        Point2D ClosestPointOnBBToPoint(Point2D point);

        /// <summary>
        /// Performs an intersection test with a segment.
        /// </summary>
        /// <param name="pointA">Endpoint of the segment outside the bounding box.</param>
        /// <param name="pointB">Endpoint of the segment inside the bounding box.</param>
        /// <returns>The point of intersection with the segment.</returns>
        Point2D IntersectionToSegment(Point2D pointA, Point2D pointB);

    }
}
