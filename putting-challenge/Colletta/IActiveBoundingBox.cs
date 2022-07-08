using putting_challenge.Fantilli;

namespace OOP21-putting-challenge-csharp.Colletta
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
    /// <returns><see langword="true"/> if a collision is occurred, <see langword="false"/> otherwise.</returns>
    bool IsColliding(IPassiveCircleBoundingBox circle);

    bool BounceAlongTangent();

    Vector2D GetNormal(Point2D pointOnActiveBoundingBox);

    Point2D ClosestPointOnBBToPoint(Point2D point);

    Point2D intersectionToSegment(Point2D pointA, Point2D pointB);

}
}
