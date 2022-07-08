using System;
using System.Collections.Generic;
using System.Text;
using Optional;
using Optional.Unsafe;
using puttingchallenge.Fantilli.common;

namespace PuttingChallenge.Colletta.Collisions
{
    public interface IDynamicBoundingBox
    {

        ICollisionTest CollidesWith(IPassiveCircleBoundingBox circle, long dt);

        public interface ICollisionTest
        {
            Boolean IsColliding();

            IActiveBoundingBox GetActiveBoundingBox();

            Option<Point2D> GetEstimatedPointOfImpact();

            Option<Vector2D> GetActiveBBSideNormal();

            Option<Vector2D> GetActiveBBSideTanget();
            
            Option<Point2D> GetPassiveBoxPositionBeforeCollisions();
        }
    }
}
