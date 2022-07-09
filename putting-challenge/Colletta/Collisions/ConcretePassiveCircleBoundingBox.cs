using puttingchallenge.Fantilli.common;
using System;
using System.Collections.Generic;
using System.Text;

namespace PuttingChallenge.Colletta.Collisions
{
    /// <summary>
    /// Represents a concrete <see cref="IPassiveCircleBoundingBox"-/>
    /// </summary>
    public class ConcretePassiveCircleBoundingBox : IPassiveCircleBoundingBox
    {
        private readonly double _radius;
        private Point2D _centerPosition;

        /// <summary>
        /// Builds a <see cref="ConcretePassiveCircleBoundingBox"/>
        /// </summary>
        /// <param name="centerPosition">Position of the center of the circumference.</param>
        /// <param name="radius">Radius of the circumference.</param>
        public ConcretePassiveCircleBoundingBox(Point2D centerPosition, double radius)
        {
            _centerPosition = centerPosition;
            _radius = radius;
        }

        /// <inheritdoc cref="IPassiveCircleBoundingBox.Radius"/>
        public double Radius => _radius;

        /// <inheritdoc cref="IPassiveCircleBoundingBox.Position"/>
        public Point2D Position { get => _centerPosition; set => _centerPosition = value; }
}
}
