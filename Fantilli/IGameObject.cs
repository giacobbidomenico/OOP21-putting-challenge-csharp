namespace Fantilli
{
    using System;

    /// <summary>
    ///  Class that represent an element of the game.
    /// </summary>
    public interface IGameObject
    {
        
        /// <summary>
        /// Types of the game objects.
        /// </summary>
        enum GameObjectType
        {
            /// <summary>
            /// The type of the game ball.
            /// </summary>
            BALL,

            /// <summary>
            /// The type of the game player.
            /// </summary
            PLAYER,

            /// <summary>
            /// The type of the land in the game.
            /// </summary>
            LAND,

            /// <summary>
            /// The type of a wall static obstacle.
            /// </summary>
            WALL,

            /// <summary>
            /// The type of a tree static obstacle.
            /// </summary>
            TREE,

            /// <summary>
            /// The type of a football ball static obstacle.
            /// </summary>
            FOOTBALL,

            /// <summary>
            /// The type of the game hole.
            /// </summary>
            HOLE
        }

        /// <summary>
        /// Gets or sets the coordinates corresponding to the position of the object.
        /// </summary>
        Point2D Position { get; set; }

        /// <summary>
        /// Gets or sets the velocity of the object.
        /// </summary>
        Vector2D Velocity { get; set; }

        /// <summary>
        /// Gets the <see cref="GameObjectType"/> of the object.
        /// </summary>
        GameObjectType Type { get; }

        /// <summary>
        /// Update physic state of the object.
        /// </summary>
        /// <param name="dt">elapsed time from the previous state</param>
        /// <param name="env">environment of the game</param>
        void UpdatePhysics(long dt, Environment env);

        /// <summary>
        /// Gets the <see cref="PhysicsComponent"/> of the game object.
        /// </summary>
        PhysicsComponent PhysicsComponent { get; }

        /// <summary>
        /// Gets the <see cref="GraphicComponent"/> of the game object.
        /// </summary>
        GraphicComponent GraphicComponent { get; }

        /// <summary>
        /// Draw the game object skin in the actual scene.
        /// </summary>
        /// <param name="graphicsContext"></param>
        void Draw(GraphicsContext graphicsContext);
    }
}
