using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using PuttingChallenge.Fantilli.Common;
using PuttingChallenge.Giacobbi.Gameobjects;

namespace PuttingChallenge.Lucioli
{
    [TestFixture]
    public class PlayerObjectTest
    {
        GameFactory _factory = new GameFactory();

        [Test]
        public void PlayerCreationTest()
        {
            PlayerObject player = _factory.CreatePlayer(new Point2D(0, 0), 40, 10, false);
            Assert.AreEqual(player.Height, 10);
            Assert.AreEqual(player.Width, 40);
            Assert.AreEqual(player.Position, new Point2D(0, 0));
            Assert.AreEqual(player.Flip, false);
        }

        [Test]
        public void FlipTest()
        {
            PlayerObject player = _factory.CreatePlayer(new Point2D(20, 50), 10, 5, false);
            player.Flip = true;
            Assert.AreEqual(player.Flip, true);
        }
    }
}
