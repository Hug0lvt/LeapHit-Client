using Microsoft.Xna.Framework;
using Modele.EntityPackage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace TestModel.Entity_test
{
    [TestClass]
    public class Ball_test
    {
        private readonly int SCREEN_WIDTH = 800;
        private readonly int SCREEN_HEIGHT = 600;

        [TestMethod]
        public void TestInitialPosition()
        {
            // Arrange
            Vector2 initialPosition = new(0, 0);

            // Act
            Ball ball = new(initialPosition.X, initialPosition.Y, null, null);

            // Assert
            Assert.AreEqual(initialPosition, new Vector2(ball.X, ball.Y));
        }

        [TestMethod]
        public void TestMove()
        {
            // Arrange
            Vector2 initialPosition = new(SCREEN_HEIGHT / 2, SCREEN_WIDTH / 2);
            var ball = new Ball(initialPosition.X, initialPosition.Y, null, null)
            {
                Zone = new(initialPosition.X, initialPosition.Y, 10, 10),
                Velocity = new Vector2(100, 1)
            };

            // Act
            ball.Move(100, SCREEN_HEIGHT, SCREEN_WIDTH);

            // Assert
            Assert.AreEqual(new Vector2(initialPosition.X, 50), new Vector2(ball.X, ball.Y));
        }
    }
}
