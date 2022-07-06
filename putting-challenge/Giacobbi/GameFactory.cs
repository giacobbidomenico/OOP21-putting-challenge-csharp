using Fantilli;
using System;
using System.Collections.Generic;
using System.Text;

namespace Giacobbi
{
    public class GameFactory
    {
        private const double BALL_GRAPHIC_FACTOR = 1.27;
        private const double RECT_GRAPHIC_FACTOR = 1.05;
        
        /// <summary>
        /// Build the ball of the game. 
        /// </summary>
        /// <param name="pos">initial position of the ball</param>
        /// <param name="radius">radius of the ball</param>
        /// <returns>an instance of <see cref="IGameObject"/>> representing the ball</returns>
        public IGameObject CreateBall(Point2D pos, double radius)
        {
            Point2D center = new Point2D(pos);
            center.SumX(-radius);
            center.SumY(-radius);
            return new BallObjectImpl(IGameObject.GameObjectType.BALL,
                                      pos,
                                      new BallPhysicsComponent(radius),
                                      new ConcretePassiveCircleBoundingBox(center, radius));
        }

        /// <summary>
        /// Build the player.
        /// </summary>
        /// <param name="pos">initial position of the player</param>
        /// <param name="skinPath">path of the player's skin</param>
        /// <param name="w">the width of the player</param>
        /// <param name="h">the height of the player</param>
        /// <returns>an instance of <see cref="PlayerObject"/> representing the player</returns>
        public PlayerObject CreatePlayer(Point2D pos,
                                         String skinPath,
                                         double w,
                                         double h)
        {
            return new PlayerObject(IGameObject.GameObjectType.PLAYER,
                                    pos,
                                    new StaticPhysicsComponent(),
                                    new ConcreteDynamicBoundingBox(new AxisAlignedBoundingBox(pos, h, w)),
                                    w,
                                    h);
        }

        /// <summary>
        /// Build the land in the game. 
        /// </summary>
        /// <param name="pos">static position of the land</param>
        /// <param name="w">the width of the land</param>
        /// <param name="h">the height of the land</param>
        /// <returns>an instance of <see cref="IGameObject"/>> representing the land.</returns>
        public IGameObject CreateLand(Point2D pos,
                                      double w,
                                      double h)
        {
            return new GameObjectImpl(IGameObject.GameObjectType.LAND,
                                      pos,
                                      new StaticPhysicsComponent(),
                                      new ConcreteDynamicBoundingBox(new AxisAlignedBoundingBox(pos, h, w)));
        }

        /// <summary>
        /// Build a new wall in the game.
        /// </summary>
        /// <param name="pos">static position of the wall</param>
        /// <param name="w">the width of the wall</param>
        /// <param name="h">the height of the wall</param>
        /// <returns>an instance of <see cref="IGameObject"/> representing a wall.</returns>
        public IGameObject CreateWall(Point2D pos,
                                     double w,
                                     double h)
        {
            return new GameObjectImpl(IGameObject.GameObjectType.WALL,
                                      pos,
                                      new StaticPhysicsComponent(),
                                      new ConcreteDynamicBoundingBox(new AxisAlignedBoundingBox(pos, h, w)));
        }

        /// <summary>
        /// Build a new tree in the game. 
        /// </summary>
        /// <param name="pos">initial position of the tree</param>
        /// <param name="w">the width of the tree</param>
        /// <param name="h">the height of the tree</param>
        /// <returns>an instance of <see cref="IGameObject"/> representing a tree.</returns>
        public IGameObject CreateTree(Point2D pos,
                                      double w,
                                      double h)
        {
            return new GameObjectImpl(IGameObject.GameObjectType.TREE,
                                      pos,
                                      new StaticPhysicsComponent(),
                                      new ConcreteDynamicBoundingBox(new CircleBoundingBox(new Point2D(pos.X + w / 2, pos.Y + w / 2), w / 2)));
        }

        /// <summary>
        /// Build a new football ball in the game. 
        /// </summary>
        /// <param name="pos">position of the football ball</param>
        /// <param name="w">width of the football ball</param>
        /// <param name="h">height of the football ball</param>
        /// <returns>an instance of <see cref="IGameObject"/> representing a football ball.</returns>
        public IGameObject CreateFootball(Point2D pos,
                                          double w,
                                          double h)
        {
            return new GameObjectImpl(IGameObject.GameObjectType.FOOTBALL,
                                      pos,
                                      new StaticPhysicsComponent(),
                                      new ConcreteDynamicBoundingBox(new CircleBoundingBox(new Point2D(pos.X + w / 2, pos.Y + w / 2), w / 2)));
        }

        /// <summary>
        /// Build a new hole in the game. 
        /// </summary>
        /// <param name="pos">position of the hole</param>
        /// <param name="w">width of the hole</param>
        /// <param name="h">height of the hole</param>
        /// <returns>an instance of <see cref="IGameObject"/> representing a hole.</returns>
        public IGameObject CreateHole(Point2D pos,
                                      double w,
                                      double h)
        {
            return new GameObjectImpl(IGameObject.GameObjectType.HOLE,
                                      pos,
                                      new StaticPhysicsComponent(),
                                      new ConcreteDynamicBoundingBox(new AxisAlignedBoundingBox(pos, h, w)));
        }
    }
}
