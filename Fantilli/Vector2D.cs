namespace Fantilli
{
    using System;

    public class Vector2D
    {
        /// <summary>
        /// Gets or sets the x-component of the 2D vector.
        /// </summary>
        public double X { get; set; }

        /// <summary>
        /// Gets or sets the y-component of the 2D vector.
        /// </summary>
        public double Y { get; set; }

        /// <summary>
        /// Build a new <see cref="Vector2D"/>.
        /// </summary>
        /// <param name="x">x-component of the 2D vector</param>
        /// <param name="y">y-component of the 2D vector</param>
        public Vector2D(double x, double y)
        {
            this.X = x;
            this.Y = y;
        }

        /// <summary>
        /// Build a new <see cref="Vector2D"/>, copy of the vector given as argument.
        /// </summary>
        /// <param name="vector">an instance of <see cref="Vector2D"/> class</param>
        public Vector2D(Vector2D vector) : this(vector.X, vector.Y)
        {
        }

        /// <inheritdoc cref="object.ToString()"/>
        public override String ToString() => "Vector2D (" + this.X + ", " + this.Y + ")";

        /// <returns>the module of the vector</returns>
        public double GetModule() => Math.Sqrt(this.X * this.X + this.Y * this.Y);

        /// <summary>
        /// Adds the supplied value to the x-component of the vector.
        /// </summary>
        /// <param name="value">the value to sum</param>
        public void SumX(double value) => this.X += value;

        /// <summary>
        /// Adds the supplied value to the y-component of the vector.
        /// </summary>
        /// <param name="value">the value to sum</param>
        public void SumY(double value) => this.Y += value;

        /// <inheritdoc cref="object.GetHashCode()"/>
        public override int GetHashCode() => HashCode.Combine(this.X, this.Y);

        /// <inheritdoc cref="object.Equals(object?)"/>
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj.GetType() != this.GetType()) return false;
            return this.Equals(obj as Vector2D);
        }

        /// <summary>
        /// Compares this instance with given <paramref name="vector"/>.
        /// The result is true if and only if the argument is not null and is 
        /// an instance of <see cref="Vector2D"/> and contains the same components.
        /// </summary>
        /// <param name="vector">the vector to compare</param>
        /// <returns>true if the given object is equal to this vector, false otherwise</returns>
        public bool Equals(Vector2D vector)
        {
            if (this == vector)
            {
                return true;
            }
            else
            {
                return this.X.CompareTo(vector.X) == 0
                       && this.Y.CompareTo(vector.Y) == 0;
            }
        }

        /// <summary>
        /// Return a new <see cref="Vector2D"/> object starting from <paramref name="pointA"/>
        /// and ending at <paramref name="pointB"/>.
        /// </summary>
        /// <param name="pointA">the startin point of the result vector</param>
        /// <param name="pointB">the ending point of the result vector</param>
        /// <returns>the new vector</returns>
        public static Vector2D GetVectorFrom(Point2D pointA, Point2D pointB) => 
            new Vector2D(pointA.X - pointB.X, pointA.Y - pointB.Y);

        /// <summary>
        /// Flips the vector components.
        /// </summary>
        public void FlipVector()
        {
            this.X = -X;
            this.Y = -Y;
        }

        /// <summary>
        /// Gets the dot product bewtween this vector and the given.
        /// </summary>
        /// <param name="vector">the vector to multiply</param>
        /// <returns>the dot product</returns>
        public double DotProduct(Vector2D vector) => this.X * vector.X + this.Y * vector.Y;
    }
}
