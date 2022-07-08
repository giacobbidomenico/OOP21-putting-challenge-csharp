using puttingchallenge.Fantilli.common;
using System;
using System.Collections.Generic;
using System.Text;

namespace PuttingChallenge.Colletta.Collisions
{
    public class ConcretePassiveCircleBoundingBox : IPassiveCircleBoundingBox
    {
        private readonly double _radius;
        private Point2D _centerPosition;
        
        public ConcretePassiveCircleBoundingBox(Point2D centerPosition, double radius)
        {
            _centerPosition = centerPosition;
            _radius = radius;
        }

        public double Radius => _radius;

        public Point2D Position { get => _centerPosition; set => _centerPosition = value; }
}
}
