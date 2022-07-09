namespace PuttingChallenge.Fantilli.Tests
{
    using System;
    using NUnit.Framework;
    using PuttingChallenge.Fantilli.Physics;
    using PuttingChallenge.Fantilli.Common;

    [TestFixture]
    public class TestBallPhysic
    {
        private const double Y_ACCELERATION = 30 * -9.81;
        private const double FRICTION = 17.1E-6;
        private const double RADIUS = 2;
        private const double COMPONENT = 20;
        private const long MILLISEC = 2000;

        private BallPhysicsComponent _phys;

        [SetUp]
        public void SetUp()
        {
            this._phys = new BallPhysicsComponent(RADIUS);
        }

        [Test]
        public void InitTest()
        {
            Assert.AreEqual(this._phys.Radius, RADIUS);
            Assert.AreEqual(this._phys.Velocity, new Vector2D(0, 0));
            Assert.False(this._phys.IsMoving);
        }

        [Test]
        public void MovingTest()
        {
            this._phys.Velocity = new Vector2D(COMPONENT, COMPONENT);
            Assert.True(this._phys.IsMoving);
            Point2D newPos = this.NextPos(MILLISEC, new Point2D(COMPONENT, COMPONENT), new Vector2D(COMPONENT, COMPONENT));
            Vector2D newVel = this.NextVel(MILLISEC, new Vector2D(COMPONENT, COMPONENT));
            Assert.AreEqual(this._phys.NextPos(MILLISEC, new Point2D(COMPONENT, COMPONENT)), newPos);
            Assert.AreEqual(this._phys.Velocity, newVel);
            Assert.True(this._phys.IsMoving);
        }

        private Vector2D NextVel(long dt, Vector2D vel)
        {
            double velX = Math.Abs(vel.X);
            double velY = vel.Y;
            double t = 0.001 * dt;

            velY -= Y_ACCELERATION * t;
            if (velX != 0)
            {
                velX -= 3 * Math.PI * FRICTION * velX * this._phys.Radius * t;
                if (vel.X < 0)
                {
                    velX *= -1;
                }
            }
            return new Vector2D(velX, velY);
        }

        private Point2D NextPos(long dt, Point2D curPos, Vector2D vel)
        {
            double t = 0.001 * dt * 1.5;
            vel = this.NextVel(dt, vel);
            double x = curPos.X + vel.X * t;
            double y = curPos.Y + vel.Y * t - 0.5 * Y_ACCELERATION * t * t;
            return new Point2D(x, y);
        }
    }
}
