namespace Fantilli
{
    public class Point2D
    {
        public double X { get; private set; }

        public double Y { get; private set; }

        /// <summary>
        /// Build a new <see cref="Point2D"/>.
        /// </summary>
        /// <param name="x">abscissa of the 2D point</param>
        /// <param name="y">ordinate of the 2D point</param>
        public Point2D(double x, double y)
        {
            this.X = x;
            this.Y = y;
        }

        /// <summary>
        /// Build a new <see cref="Point2D"/>, copy of the point given as argument.
        /// </summary>
        /// <param name="point">another instance of <see cref="Point2D"/> class</param>
        public Point2D(Point2D point) : this(point.X, point.Y)
        {
        }

        /// <inheritdoc/>
        public override string ToString() => "Point2D (" + X + ", " + Y + ")";

        /**
         * Adds the supplied value to the abscissa of the point.
         * 
         * @param value
         *          the value to sum
         */
        public void SumX(double value) => this.X += value;

        /**
         * Adds the supplied value to the ordinate of the point.
         * 
         * @param value
         *          the value to sum
         */
        public void sumY(final double value)
        {
            this.y += value;
        }

        /**
         * {@inheritDoc}
         */
        @Override
    public int hashCode()
        {
            return Double.hashCode(x) ^ Double.hashCode(y);
        }

        /**
         * Compares this point to the specified object. The result is true if and
         * only if the argument is not null and is an instance of {@link Point2D} and 
         * contains the same coordinates.
         * 
         * @param obj
         *          the object to compare
         * 
         * @return true if the given object is equal to this point, false otherwise
         */
        @Override
    public boolean equals(final Object obj)
        {
            if (this == obj)
            {
                return true;
            }
            if (obj instanceof Point2D) {
                final Point2D p = (Point2D)obj;
                return Double.compare(this.x, p.getX()) == 0
                       && Double.compare(this.y, p.getY()) == 0;
            }
            return false;
        }

        /**
         * Get the distance between two {@link Point2D}.
         * @param pointA
         * @param pointB
         * @return
         *          the distance between point A and B
         */
        public static double getDistance(final Point2D pointA, final Point2D pointB)
        {
            return new Vector2D(pointA.getX() - pointB.getX(), pointA.getY() - pointB.getY()).getModule();
        }
    }
}
