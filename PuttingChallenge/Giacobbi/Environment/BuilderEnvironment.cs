using PuttingChallenge.Fantilli.Common;
using PuttingChallenge.Fantilli.GameObjects;
using PuttingChallenge.Giacobbi.Gameobjects;
using PuttingChallenge.Lucioli;
using System;
using System.Collections.Generic;
using System.Drawing;
using Optional;
using Optional.Unsafe;

namespace PuttingChallenge.Giacobbi.Environment
{
    /// <summary>
    /// Interface that defines the builder of the game environment.
    /// </summary>
    public class BuilderEnvironment : IBuilderEnvironment
    {
        private readonly GameFactory _factory;
        private readonly IList<IGameObject> _gameObjects;
        private Option<Rectangle> _container;
        private Option<IGameObject> _ball;
        private Option<PlayerObject> _player;
        private Option<IGameObject> _hole;

        /// <summary>
        /// Build a new <see cref="BuilderEnvironment"/>.
        /// </summary>
        public BuilderEnvironment()
        {
            _factory = new GameFactory();
            _gameObjects = new List<IGameObject>();
            _container = Option.None<Rectangle>();
            _ball = Option.None<IGameObject>();
            _player = Option.None<PlayerObject>();
            _hole = Option.None<IGameObject>();
        }

        /// <summary>
        /// Add a generic element
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="element1">element to check</param>
        /// <param name="element2">element to add if element1 is empty</param>
        /// <returns>the correct element</returns>
        private Option<T> AddElement<T>(Option<T> element1, T element2) =>
            !element1.HasValue ? Option.Some<T>(element2) : element1;

        /// <inheritdoc/>
        public IBuilderEnvironment AddContainer(Rectangle container)
        {
            _container = AddElement<Rectangle>(_container, container);
            return this;
        }

        /// <inheritdoc/>
        public IBuilderEnvironment AddBall(Point2D pos, double radius)
        {
            _ball = AddElement<IGameObject>(_ball, _factory.CreateBall(pos, radius));
            return this;
        }

        /// <inheritdoc/>
        public IBuilderEnvironment AddPlayer(Point2D pos,
                                             double w,
                                             double h,
                                             bool flip)
        {
            _player = AddElement<PlayerObject>(_player, _factory.CreatePlayer(pos, w, h, true));
            return this;
        }

        /// <inheritdoc/>
        /// <exception cref="ArgumentException">
        /// GameObjectType doesn't exist
        /// </exception>
        public IBuilderEnvironment AddStaticObstacle(IGameObject.GameObjectType gameObjectType,
                                                     Point2D pos,
                                                     double w,
                                                     double h)
        {
            switch (gameObjectType)
            {
                case IGameObject.GameObjectType.LAND:
                    _gameObjects.Add(_factory.CreateLand(pos, w, h));
                    break;
                case IGameObject.GameObjectType.WALL:
                    _gameObjects.Add(_factory.CreateWall(pos, w, h));
                    break;
                case IGameObject.GameObjectType.TREE:
                    _gameObjects.Add(_factory.CreateTree(pos, w, h));
                    break;
                case IGameObject.GameObjectType.FOOTBALL:
                    _gameObjects.Add(_factory.CreateFootball(pos, w, h));
                    break;
                default:
                    throw new ArgumentException();
            }

            return this;
        }

        /// <inheritdoc/>
        public IBuilderEnvironment AddHole(Point2D pos,
                                           double w,
                                           double h)
        {
            _hole = AddElement<IGameObject>(_hole, _factory.CreateHole(pos, w, h));
            return this;
        }

        /// <inheritdoc/>
        /// <exception cref="InvalidOperationException">
        /// if the method doens't build the environment for an error.
        /// </exception>
        public IEnvironment Build()
        {
            if (!_container.HasValue
                    || !_ball.HasValue
                    || !_player.HasValue
                    || !_hole.HasValue)
            {
                throw new InvalidOperationException();
            }
            return new Environment(_container.ValueOrFailure(),
                                   _ball.ValueOrFailure(),
                                   _player.ValueOrFailure(),
                                   _gameObjects,
                                   _hole.ValueOrFailure());
        }

    }
}