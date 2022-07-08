using puttingchallenge.Fantilli.common;
using puttingchallenge.Fantilli.gameobjects;
using System;
using System.Drawing;

namespace puttingchallenge.Giacobbi
{
    public interface IBuilderEnvironment
    {
        /// <summary>
        /// Sets the <see cref="Rectangle"/> that contains the game <see cref="IEnvironment"/>.
        /// </summary>
        /// <param name="container">
        /// the <see cref="Rectangle"/> that contains the game <see cref="IEnvironment"/>
        /// </param>
        /// <returns>
        /// an instance of <see cref="IBuilderEnvironment"/>,the builder of the game 
        /// <see cref="IEnvironment"/>
        /// </returns>
        IBuilderEnvironment AddContainer(Rectangle container);

        /// <summary>
        /// Sets the ball configuration.
        /// </summary>
        /// <param name="pos">initial position of the ball</param>
        /// <param name="radius">radius of the ball</param>
        /// <returns>
        /// an instance of <see cref="IBuilderEnvironment"/> ,the builder of 
        /// the game <see cref="IEnvironment"/>
        /// </returns>
        IBuilderEnvironment AddBall(Point2D pos, double radius);

        /// <summary>
        /// Sets the player configuration.
        /// </summary>
        /// <param name="pos">initial position of the player</param>
        /// <param name="w">
        /// width of the rectangle where the player will be contained
        /// </param>
        /// <param name="h">
        /// height of the rectangle where the player will be contained
        /// </param>
        /// <param name="flip">flip of the player</param>
        /// <returns>
        /// an instance of <see cref="IBuilderEnvironment"/> ,the builder of 
        /// the game <see cref="IEnvironment"/>
        /// </returns>
        IBuilderEnvironment AddPlayer(Point2D pos,
                                      String skinPath,
                                      double w,
                                      double h,
                                      bool flip);

        /// <summary>
        /// Sets the configuration of a new static obstacle. 
        /// </summary>
        /// <param name="typeOfObstacle">
        /// type of the static obstacle
        /// </param>
        /// <param name="pos">
        /// position of the obstacle
        /// </param>
        /// <param name="w">width of the obstacle</param>
        /// <param name="h">height of the obstacle</param>
        /// <returns>
        /// an instance of <see cref="IBuilderEnvironment"/> ,the builder of 
        /// the game <see cref="IEnvironment"/>
        /// </returns>
        IBuilderEnvironment AddStaticObstacle(IGameObject.GameObjectType typeOfObstacle,
                                              Point2D pos,
                                              double w,
                                              double h);

        /// <summary>
        /// Sets the hole configuration.
        /// </summary>
        /// <param name="pos">initial position of the hole</param>
        /// <param name="w">
        /// width of the rectangle where the hole will be contained
        /// </param>
        /// <param name="h">
        /// height of the rectangle where the hole will be contained
        /// </param>
        /// <returns>
        /// an instance of <see cref="IBuilderEnvironment"/> ,the builder of 
        /// the game <see cref="IEnvironment"/>
        /// </returns>
        IBuilderEnvironment AddHole(Point2D pos, double w, double h);

        /// <summary>
        /// Builds the game <see cref="IEnvironment"/>.
        /// </summary>
        /// <returns>
        /// an instance of <see cref="IEnvironment"/>, representing the
        /// game <see cref="IEnvironment"/>
        /// </returns>
        IEnvironment Build();
    }
}
