using puttingchallenge.Fantilli.common;
using puttingchallenge.Fantilli.gameobjects;
using puttingchallenge.Fantilli.physics;
using puttingchallenge.Lucioli;
using System.Collections.Generic;
using System.Drawing;
using Optional;

namespace puttingchallenge.Giacobbi
{
    public interface IEnvironment
    {
        public IGameObject Ball { get; }
        public PlayerObject Player { get; }
        public Rectangle Container { get; }
        public IList<IGameObject> StaticObstacle { get; }
        public IGameObject Hole { get; }

        /// <summary>
        /// Update the game <see cref="IEnvironment"/>. 
        /// </summary>
        /// <param name="dt">instant of time.</param>
        void Update(long dt);

        /// <summary>
        /// Check if there has been a collision between several <see cref="IGameObject"/>s.  
        /// </summary>
        /// <param name="ballHitbox"><see cref="HitBox"/> to collide with</param>
        /// <param name="ballPhysics">physics to move the <see cref="HitBox"/></param>
        /// <param name="ballPosition">previous position of the ball</param>
        /// <param name="dt">time from last frame</param>
        /// <returns>
        /// return a info about the collision occurred, empty 
        /// if no collision has occurred.
        /// </returns>
        Option<CollisionTest> CheckCollisions(PassiveCircleBoundingBox ballHitbox,
                BallPhysicsComponent ballPhysics,
                Point2D ballPosition, long dt);

        /// <summary>
        /// Adds a static obstacle to the game <see cref="IEnvironment"/>. 
        /// </summary>
        /// <param name="obstacle">static obstacle to add</param>
        void AddStaticObstacle(IGameObject obstacle);

        /// <summary>
        /// Notifies if an event has been intercepted. 
        /// </summary>
        void NotifyEvents();

        /// <summary>
        /// Reads the events sent by the <see cref="IGameObject"/>. 
        /// </summary>
        void ReceiveEvents();

        /// <summary>
        /// Configure the <see cref="IObservableEvents{ModelEventType}"/>, 
        /// that allows the communication from the <see cref="IEnvironment"/> 
        /// to the <see cref="IGameObject"/>.
        /// </summary>
        /// <param name="observable"></param>
        void ConfigureObservable(IObservableEvents<ModelEventType> observable);

        /// <returns>
        /// return the <see cref="IObservableEvents{T}"/>>, that allows the
        /// communication from the <see cref="GameState{T}"/> to the <see cref="IEnvironment"/>.
        /// </returns>
        IObservableEvents<ModelEventType> GetObservable();

        /// <returns>
        /// returns a <see cref="IList{T}"/> of <see cref="IGameObject"/>s, where:
        ///        -the first element is the player
        ///        -the second element is the ball
        ///        -the last element is the hole
        ///        -the other elements are the obstacles
        /// </returns>
        IList<IGameObject> GetObjects();
    }
}
