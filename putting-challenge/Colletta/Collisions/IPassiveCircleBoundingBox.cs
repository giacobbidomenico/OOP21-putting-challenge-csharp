using puttingchallenge.Fantilli.common;

namespace PuttingChallenge.Colletta.Collisions
{
    /// <summary>
    /// Represents a basic circle bounsing box.
    /// </summary>
    public interface IPassiveCircleBoundingBox
    {

        /// <summary>
        /// Gets the radius of the bounding box.
        /// </summary>
        double Radius { get; }

        /// <summary>
        /// Gets or sets the position of the bounding box.
        /// </summary>
        Point2D Position { get; set; }
    }
}
