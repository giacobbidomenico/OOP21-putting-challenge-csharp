namespace Fantilli
{
    using System;

    public class BallPhysicsComponent : AbstractPhysicsComponent
    {
        private const double Y_ACCELERATION = 30 * -9.81;
        private const double FRICTION = 17.1E-6;
        private const double INCREASE_TANGENT = 1.7;
        private const double INCREASE_NORMAL = 1.5;
        private const double REDUCE_Y = 0.7;
        private const double REDUCE_X = 0.9;
        private const double MIN_POTENTIAL_ENERGY = 100;
        private const double MIN_BOUNCING_DIFFERENCE_FACTOR = 0.8;
        private const double MIN_KINETICS_ENERGY = 100;

        private readonly double _radius;
        private Point2D? _lastPos;
        private ActiveBoundingBox? _lastHitbox;

        /// <summary>
        /// Build a new <see cref="BallObjectImpl"/>.
        /// </summary>
        /// <param name="radius">the physic radius of the ball</param>
        public BallPhysicsComponent(double radius)
        {
            base.Velocity = new Vector2D(0, 0);
            this._radius = radius;
            this._lastPos = null;
            this._lastHitbox = null;
        }

        /// <summary>
        /// Gets the radius of the ball.
        /// </summary>
        public double Radius { get => this._radius; }

        /// <summary>
        /// Return true if the ball is stopped, false otherwise
        /// </summary>
        public bool IsMoving { get; private set; }

        /// <inheritdoc cref="IPhysicsComponent.Velocity"/>
        public override Vector2D Velocity 
        { 
            get => base.Velocity; 
            set
            {
                base.Velocity = value;
                if (this.Velocity.Equals(new Vector2D(0, 0)))
                {
                    this.IsMoving = false;
                }
                else
                {
                    this.IsMoving = true;
                }
            } 
        }

        /// <inheritdoc cref="IPhysicsComponent.Update(long, IGameObject, Environment)"/>
        public override void Update(long dt, IGameObject obj, Environment env)
        {
            if (this.IsMoving)
            {
                BallPhysicsComponent clone = new BallPhysicsComponent(this.Radius);
                clone.Velocity = new Vector2D(this.Velocity);

                CollisionTest? infoOpt = env.CheckCollisions(((BallObjectImpl)obj).HitBox, clone, obj.Position, dt);
                Point2D nextPos;
                if (infoOpt.HasValue)
                {
                    CollisionTest info = infoOpt.Value;

                    double radius = ((BallObjectImpl)obj).HitBox.Radius();
                    Vector2D normale = info.GetActiveBBSideNormal().Value;
                    Vector2D lastVel = this.Velocity;

                    nextPos = info.GetEstimatedPointOfImpact().Value;
                    bool bTangent = info.GetActiveBoundingBox().BounceAlongTanget();
                    if (bTangent)
                    {
                        double cat = Math.Sqrt(Math.Pow(radius, 2) / 2);
                        nextPos.SumX(normale.X * cat * INCREASE_TANGENT);
                        nextPos.SumY(normale.Y * cat * INCREASE_TANGENT);
                    }
                    else
                    {
                        nextPos.SumX(normale.X * radius * INCREASE_NORMAL);
                        nextPos.SumY(normale.Y * radius * INCREASE_NORMAL);
                    }
                    nextPos.SumX(-radius);
                    nextPos.SumY(-radius);

                    normale = bTangent ? info.GetActiveBBSideTanget().Value : normale;
                    Vector2D finVel = this.VelAfterCollision(normale, lastVel, info.GetActiveBoundingBox().BounceAlongTanget());
                    this.Velocity = finVel;
                    this.IsStopping(nextPos, info.GetActiveBoundingBox());
                }
                else
                {
                    nextPos = this.NextPos(dt, obj.Position);
                }

                obj.Position = nextPos;
            }
        }

        private Vector2D VelAfterCollision(Vector2D normale, Vector2D lastVel, bool usesTangent)
        {
            double x;
            double y;
            if (usesTangent)
            {
                double tangentX = normale.X;
                double tangentY = normale.Y;
                double dot = tangentX * lastVel.X + tangentY * lastVel.Y;
                x = dot * tangentX;
                y = dot * tangentY;
            }
            else
            {
                double sign = Math.Sign(normale.Y) == -1 ? 1 : -1;
                y = lastVel.Y * (normale.Y == 0 ? 1 : normale.Y * sign) * REDUCE_Y;
                sign = Math.Sign(normale.X) == -1 ? 1 : -1;
                x = lastVel.X * (normale.X == 0 ? 1 : normale.X * sign) * REDUCE_X;
            }
            return new Vector2D(x, y);
        }

        private void IsStopping(Point2D pos, ActiveBoundingBox hitbox)
        {
            if (((ActiveBoundingBox?)this._lastHitbox).HasValue && ((Point2D?)this._lastPos).)
            {
                final Vector2D vel = this.getVelocity();
                if (Point2D.getDistance(pos, this.lastPos.get()) < radius * MIN_BOUNCING_DIFFERENCE_FACTOR
                    && hitbox.equals(this.lastHitbox.get())
                    && (-Y_ACCELERATION * (1 / pos.getY()) * 100) < MIN_POTENTIAL_ENERGY
                    && Math.abs(vel.getX()) + Math.abs(vel.getY()) < MIN_KINETICS_ENERGY)
                {
                    this.setVelocity(new Vector2D(0, 0));
                }
            }
            this.lastPos = Optional.of(pos);
            this.lastHitbox = Optional.of(hitbox);
        }

        /**
         * Given a delta time, it calculates the next position of the object, starting from an initial position.
         * Follow the formulas of the motion of the projectile in a viscous medium.
         * 
         * @param dt
         *          delta time
         * @param curPos
         *          starting position
         * @return the next expected position
         */
        public Point2D nextPos(final long dt, final Point2D curPos)
        {
            final double t = 0.001 * dt * 1.5;
            final Vector2D vel = this.getVelocity();

            this.reduceVel(dt);
            final double x = curPos.getX() + (vel.getX() * t);
            final double y = curPos.getY()
                             + (vel.getY() * t)
                             - (0.5 * Y_ACCELERATION * t * t);
            return new Point2D(x, y);
        }

        private void reduceVel(final long dt)
        {
            double velX = Math.abs(this.getVelocity().getX());
            double velY = this.getVelocity().getY();
            final double t = 0.001 * dt;

            velY -= Y_ACCELERATION * t;
            if (velX != 0)
            {
                velX -= 3 * Math.PI * FRICTION * velX * this.radius * t;
                if (this.getVelocity().getX() < 0)
                {
                    velX *= -1;
                }
            }
            this.setVelocity(new Vector2D(velX, velY));
        }
    }
}
