namespace puttingchallenge.Fantilli.common
{
    using System;

    /// <summary>
    /// Class that represent a 2-dimensional point.
    /// </summary>
    public class Point2D
    {
        /// <summary>
        /// Gets the abscissa of the 2D point.
        /// </summary>
        public double X { get; private set; }

        /// <summary>
        /// Gets the ordinate of the 2D point.
        /// </summary>
        public double Y { get; private set; }

        /// <summary>
        /// Build a new <see cref="Point2D"/>.
        /// </summary>
        /// <param name="x">abscissa of the 2D point</param>
        /// <param name="y">ordinate of the 2D point</param>
        public Point2D(double x, double y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// Build a new <see cref="Point2D"/>, copy of the point given as argument.
        /// </summary>
        /// <param name="point">another instance of <see cref="Point2D"/> class</param>
        public Point2D(Point2D point) : this(point.X, point.Y)
        {
        }

        /// <inheritdoc cref="object.ToString()"/>
        public override string ToString() => "Point2D (" + X + ", " + Y + ")";

        /// <summary>
        /// Adds the supplied value to the abscissa of the point.
        /// </summary>
        /// <param name="value">the value to sum</param>
        public void SumX(double value) => X += value;

        /// <summary>
        /// Adds the supplied value to the ordinate of the point.
        /// </summary>
        /// <param name="value">the value to sum</param>
        public void SumY(double value) => Y += value;

        /// <inheritdoc cref="object.GetHashCode()"/>
        public override int GetHashCode() => HashCode.Combine(X, Y);

        /// <inheritdoc cref="object.Equals(object?)"/>
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj.GetType() != GetType()) return false;
            return Equals(obj as Point2D);
        }

        /// <summary>
        /// Compares this instance with given <paramref name="point"/>.
        /// The result is true if and only if the argument is not null and is 
        /// an instance of <see cref="Point2D"/> and contains the same coordinates.
        /// </summary>
        /// <param name="point">the point to compare</param>
        /// <returns>true if the given object is equal to this point, false otherwise</returns>
        public bool Equals(Point2D point)
        {
            if (this == point)
            {
                return true;
            }
            else
            {
                return X.CompareTo(point.X) == 0
                       && Y.CompareTo(point.Y) == 0;
            }
        }

        /// <summary>
        /// Get the distance between two <see cref="Point2D"/>.
        /// </summary>
        /// <param name="pointA">the first point</param>
        /// <param name="pointB">the second point</param>
        /// <returns>the distance between point A and B</returns>
        public static double GetDistance(Point2D pointA, Point2D pointB) =>
            Vector2D.GetVectorFrom(pointA, pointB).GetModule();
    }
}
