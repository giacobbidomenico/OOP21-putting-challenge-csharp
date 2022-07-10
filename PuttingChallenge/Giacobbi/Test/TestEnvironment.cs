using PuttingChallenge.Fantilli.Common;
using PuttingChallenge.Fantilli.GameObjects;
using PuttingChallenge.Fantilli.Physics;
using PuttingChallenge.Giacobbi.Environment;
using PuttingChallenge.Giacobbi.Gameobjects;
using PuttingChallenge.Lucioli;
using NUnit.Framework;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System;

namespace PuttingChallenge.Giacobbi.Test
{
    [TestFixture]
    public class TestEnvironment
    {
        private const double NUM1 = 20;
        private const double NUM2 = 30;
        private const double NUM3 = 40;
        private const double NUM4 = 50;
        private const double NUM5 = 100;
        private const double NUM6 = 80;

        public readonly Point2D mainPosition = new Point2D(NUM1, NUM2);
        public readonly Point2D anotherPosition = new Point2D(NUM3, NUM4);
        public readonly Rectangle _container;
        public readonly BallObjectImpl _ball;
        public readonly PlayerObject _player;
        public readonly IGameObject _land;
        public readonly IGameObject _wall;
        public readonly IGameObject _tree;
        public readonly IGameObject _football;
        public readonly IGameObject _hole;
        public readonly IList<IGameObject> _staticObstacles;
        public readonly GameFactory  _factory = new GameFactory();

        public TestEnvironment()
        {
            _container = new Rectangle(0, 0, Convert.ToInt32(NUM1), Convert.ToInt32(NUM2));
            _ball = (BallObjectImpl)_factory.CreateBall(anotherPosition, NUM1);
            _player = _factory.CreatePlayer(mainPosition, NUM1, NUM2, false);
            _land = _factory.CreateLand(mainPosition, NUM1, NUM2);
            _wall = _factory.CreateWall(mainPosition, NUM1, NUM2);
            _tree = _factory.CreateTree(mainPosition, NUM1, NUM2);
            _football = _factory.CreateFootball(mainPosition, NUM1, NUM2);
            _hole = _factory.CreateHole(mainPosition, NUM1, NUM2);
            _staticObstacles = new List<IGameObject>(){_land,
                                                       _wall,
                                                       _tree,
                                                       _football};
        }

        
        private IEnvironment InitEnvironment()
        {
            this.CheckNullException();
            return new Environment.Environment(_container,
                                               (BallObjectImpl)_ball,
                                               _player,
                                               _staticObstacles,
                                               _hole);
        }

        private void CheckNullException()
        {
        }

        /// <summary>
        /// Check if the environment is set up correctly. 
        /// </summary>
        [Test]
        public void TestCorrectEnvironment()
        {
            IEnvironment env = InitEnvironment();

            Assert.AreEqual(env.Ball.Position, anotherPosition);
            Assert.AreEqual(env.Player.Position, mainPosition);
            Assert.AreEqual(env.Hole.Position, mainPosition);

            IList<Point2D> obstaclesPosition = env.StaticObstacle
                .Select(e => e.Position).ToList();

            Assert.AreEqual(obstaclesPosition, new List<Point2D>{mainPosition,
                                                                 mainPosition,
                                                                 mainPosition,
                                                                 mainPosition});

            Assert.AreNotEqual(obstaclesPosition, new List<Point2D>{anotherPosition,
                                                                    mainPosition,
                                                                    mainPosition,
                                                                    mainPosition});

            Assert.AreEqual(_land.Velocity, new Vector2D(0, 0));
            Assert.AreEqual(_tree.Velocity, new Vector2D(0, 0));
            Assert.AreEqual(_wall.Velocity, new Vector2D(0, 0));
            Assert.AreEqual(_football.Velocity, new Vector2D(0, 0));
        }

        /// <summary>
        /// Check if the environment builder works correctly.
        /// </summary>
        [Test]
        public void TestBuilderEnvironment()
        {
            IBuilderEnvironment buildEnv = new BuilderEnvironment();
            IEnvironment env = buildEnv.AddBall(anotherPosition, NUM1)
                                       .AddContainer(_container)
                                       .AddPlayer(mainPosition, NUM1, NUM2, false)
                                       .AddStaticObstacle(IGameObject.GameObjectType.LAND, mainPosition, NUM1, NUM2)
                                       .AddStaticObstacle(IGameObject.GameObjectType.WALL, mainPosition, NUM1, NUM2)
                                       .AddStaticObstacle(IGameObject.GameObjectType.TREE, mainPosition, NUM1, NUM2)
                                       .AddStaticObstacle(IGameObject.GameObjectType.FOOTBALL, mainPosition, NUM1, NUM2)
                                       .AddHole(mainPosition, NUM1, NUM2)
                                       .Build();
            Assert.AreEqual(this.InitEnvironment(), env);
        }

        /// <summary>
        /// Checks if the the collisions are detected correctly.
        /// </summary>
        [Test]
        public void CheckCollisions()
        {
            var env = this.InitEnvironment();
            Assert.IsFalse(env.CheckCollisions(_ball.HitBox,
                                               (BallPhysicsComponent)_ball.PhysicsComponent,
                                               _ball.Position,
                                                Convert.ToInt64(NUM1)).HasValue);

            IGameObject landCopy = _factory.CreateHole(new Point2D(NUM5, NUM6),
                                                       NUM1,
                                                       NUM2);
            Assert.IsFalse(env.CheckCollisions(_ball.HitBox,
                                               (BallPhysicsComponent)_ball.PhysicsComponent,
                                               landCopy.Position,
                                               Convert.ToInt64(NUM1)).HasValue);
        }

        [Test]
        public void CheckCollisionWithHole()
        {
            var env = this.InitEnvironment();

            Assert.True(env.CheckCollisions(_ball.HitBox,
                                              (BallPhysicsComponent)_ball.PhysicsComponent,
                                              _hole.Position,
                                              Convert.ToInt64(NUM1)).HasValue);
            IGameObject holeCopy = _factory.CreateHole(new Point2D(NUM5, NUM6),
                                                       NUM1,
                                                       NUM2);
            Assert.False(env.CheckCollisions(_ball.HitBox,
                                               (BallPhysicsComponent)_ball.PhysicsComponent,
                                               holeCopy.Position,
                                               Convert.ToInt64(NUM1)).HasValue);
        }
    }
}
