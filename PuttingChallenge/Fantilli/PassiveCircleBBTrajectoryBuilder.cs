namespace PuttingChallenge.Fantilli
{
    using System;
    using Optional;
    using Optional.Unsafe;
    using PuttingChallenge.Colletta.Collisions;
    using PuttingChallenge.Fantilli.Common;
    using PuttingChallenge.Fantilli.Physics;

    /// <summary>
    /// Class that build a <see cref="IPassiveCircleBoundingBox"/> positioned on a 
    /// physical trajectory, based on a given delta time.
    /// </summary>
    public class PassiveCircleBBTrajectoryBuilder
    {
        private Option<IPassiveCircleBoundingBox> _hitBox;
        private Option<BallPhysicsComponent> _physicComponent;
        private Vector2D _initialVel;
        private Option<Point2D> _position;

        /// <summary>
        /// Build a new <see cref="PassiveCircleBBTrajectoryBuilder"/>.
        /// </summary>
        public PassiveCircleBBTrajectoryBuilder()
        {
            this._hitBox = Option.None<IPassiveCircleBoundingBox>();
            this._physicComponent = Option.None<BallPhysicsComponent>();
            this._position = Option.None<Point2D>();
        }

        /// <summary>
        /// Sets the original hit-box.
        /// </summary>
        /// <exception cref="InvalidOperationException">if the physic component is already set</exception>
        public IPassiveCircleBoundingBox HitBox
        {
            set
            {
                if (!this._hitBox.HasValue)
                {
                    this._hitBox = Option.Some(value);
                }
                else
                {
                    throw new InvalidOperationException("Hit-box already set");
                }
            }
        }

        /// <summary>
        /// Sets the physic component.
        /// </summary>
        /// <exception cref="InvalidOperationException">if the physic component is already set</exception>
        public BallPhysicsComponent Physic
        {
            set
            {
                if (!this._physicComponent.HasValue)
                {
                    this._physicComponent = Option.Some(value);
                    this._initialVel = value.Velocity;
                }
                else
                {
                    throw new InvalidOperationException("Physic component already set");
                }
            } 
        }

        /// <summary>
        /// Sets the original position.
        /// </summary>
        /// <exception cref="InvalidOperationException">if the physic component is already set</exception>
        public Point2D Position
        {
            set
            {
                if (!this._position.HasValue)
                {
                    this._position = Option.Some(value);
                }
                else
                {
                    throw new InvalidOperationException("Position already set");
                }
            }
        }

        /// <summary>
        /// Build a <see cref="IPassiveCircleBoundingBox"/> positioned on a physical 
        /// trajectory in a certain time.
        /// </summary>
        /// <param name="dt">time elapsed</param>
        /// <returns>the builded <see cref="IPassiveCircleBoundingBox"/></returns>
        /// <exception cref="InvalidOperationException">if at least one component has not been set</exception>
        public IPassiveCircleBoundingBox Build(long dt)
        {
            if (this._hitBox.HasValue
                && this._physicComponent.HasValue
                && this._position.HasValue)
            {
                this._physicComponent.ValueOrFailure().Velocity = new Vector2D(this._initialVel);
                Point2D pos = this._physicComponent.ValueOrFailure().NextPos(dt, this._position.ValueOrFailure());
                return new ConcretePassiveCircleBoundingBox(pos, this._hitBox.ValueOrFailure().Radius);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }
    }
}
