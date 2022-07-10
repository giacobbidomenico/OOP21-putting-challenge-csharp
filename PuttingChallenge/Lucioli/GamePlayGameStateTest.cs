using NUnit.Framework;
using Optional;
using PuttingChallenge.Colletta.Mediator;
using PuttingChallenge.Fantilli.Common;
using PuttingChallenge.Giacobbi.Environment;
using PuttingChallenge.Lucioli;
using System;
using System.Collections.Generic;
using System.Text;

namespace PuttingChallenge.Lucioli
{
    [TestFixture]
    public class GamePlayGameStateTests
    {
        private const int None = 0;
        private const int MaxLives = 3;
        GameStateManager manager;
        ConcreteMediator mediator;

        [SetUp]
        public void SetUp()
        {
            mediator = new ConcreteMediator();
            manager = new GameStateManager();
            mediator.AddColleague(manager);
            manager.Mediator = mediator;
        }

        [Test]
        public void InitStateTest()
        {
            manager.SwitchState(GameStatus.Play);
            Assert.AreEqual(manager.CurrentGameState.Status, GameStatus.Play);
            GamePlayGameState gamePlayState = (GamePlayGameState)manager.CurrentGameState;
            Assert.AreEqual(gamePlayState.Score, None);
            Assert.AreEqual(gamePlayState.Lives, MaxLives);
        }

        [Test]
        public void LeavingStateTest()
        {
            manager.SwitchState(GameStatus.Play);
            manager.CurrentGameState.LeavingState(GameStatus.GameOver);
            Assert.AreEqual(manager.CurrentGameState.Status, GameStatus.GameOver);
        }

        [Test]
        public void GameOverTest()
        {
            manager.SwitchState(GameStatus.Play);
            GamePlayGameState gamePlayState = (GamePlayGameState)manager.CurrentGameState;
            for (int i = MaxLives; i > 0; i--)
            {
                gamePlayState.Shoot(new Tuple<Point2D, Point2D>(new Point2D(0, 0), new Point2D(1, 1)));
                Assert.AreEqual(gamePlayState.Score, None);
                Assert.AreEqual(gamePlayState.Lives, i - 1);
            }
            Assert.AreEqual(gamePlayState.Score, None);
            Assert.AreEqual(gamePlayState.Lives, None);
        }
    }
}