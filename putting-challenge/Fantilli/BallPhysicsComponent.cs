namespace putting_challenge.Fantilli
{
    using System;
    using Optional;
    using Optional.Unsafe;
    using putting_challenge.Giacobbi;

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
        private Option<Point2D> _lastPos;
        private Option<ActiveBoundingBox> _lastHitbox;

        /// <summary>
        /// Build a new <see cref="BallObjectImpl"/>.
        /// </summary>
        /// <param name="radius">the physic radius of the ball</param>
        public BallPhysicsComponent(double radius)
        {
            base.Velocity = new Vector2D(0, 0);
            this._radius = radius;
            this._lastPos = Option.None<Point2D>();
            this._lastHitbox = Option.None<ActiveBoundingBox>();
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

        /// <inheritdoc cref="IPhysicsComponent.Update(long, IGameObject, IEnvironment)"/>
        public override void Update(long dt, IGameObject obj, IEnvironment env)
        {
            if (this.IsMoving)
            {
                BallPhysicsComponent clone = new BallPhysicsComponent(this.Radius);
                clone.Velocity = new Vector2D(this.Velocity);

                Option<CollisionTest> infoOpt = env.CheckCollisions(((BallObjectImpl)obj).HitBox, clone, obj.Position, dt);
                Point2D nextPos;
                if (infoOpt.HasValue)
                {
                    Option<CollisionTest> info = infoOpt.ValueOrFailure();

                    double radius = ((BallObjectImpl)obj).HitBox.Radius();
                    Vector2D normale = info.GetActiveBBSideNormal().ValueOrFailure();
                    Vector2D lastVel = this.Velocity;

                    nextPos = info.GetEstimatedPointOfImpact().ValueOrFailure();
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

                    normale = bTangent ? info.GetActiveBBSideTanget().ValueOrFailure() : normale;
                    Vector2D finVel = this.VelAfterCollision(normale, lastVel, bTangent);
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
            if (this._lastHitbox.HasValue && this._lastPos.HasValue)
            {
                Vector2D vel = this.Velocity;
                if (Point2D.GetDistance(pos, this._lastPos.ValueOrFailure()) < this.Radius * MIN_BOUNCING_DIFFERENCE_FACTOR
                    && hitbox.equals(this._lastHitbox.ValueOrFailure())
                    && (-Y_ACCELERATION * (1 / pos.Y) * 100) < MIN_POTENTIAL_ENERGY
                    && Math.Abs(vel.X) + Math.Abs(vel.Y) < MIN_KINETICS_ENERGY)
                {
                    this.Velocity = new Vector2D(0, 0);
                }
            }
            this._lastPos = Option.Some(pos);
            this._lastHitbox = Option.Some(hitbox);
        }

        /// <summary>
        /// Given a delta time, it calculates the next position of the object, starting from an initial position.
        /// Follow the formulas of the motion of the projectile in a viscous medium.
        /// </summary>
        /// <param name="dt">elapsed time from the previous state</param>
        /// <param name="curPos">starting position</param>
        /// <returns>the next expected position</returns>
        public Point2D NextPos(long dt, Point2D curPos)
        {
            double t = 0.001 * dt * 1.5;
            Vector2D vel = this.Velocity;

            this.ReduceVel(dt);
            double x = curPos.X + (vel.X * t);
            double y = curPos.Y + (vel.Y * t) - (0.5 * Y_ACCELERATION * t * t);
            return new Point2D(x, y);
        }

        private void ReduceVel(long dt)
        {
            double velX = Math.Abs(this.Velocity.X);
            double velY = this.Velocity.Y;
            double t = 0.001 * dt;

            velY -= Y_ACCELERATION * t;
            if (velX != 0)
            {
                velX -= 3 * Math.PI * FRICTION * velX * this.Radius * t;
                if (this.Velocity.X < 0)
                {
                    velX *= -1;
                }
            }
            this.Velocity = new Vector2D(velX, velY);
        }
    }
}
