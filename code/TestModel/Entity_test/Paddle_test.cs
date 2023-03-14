using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Modele.EntityPackage;
using Modele.GamePackage;
using Modele.SkinPackage;
using MonoGame.Extended.Sprites;
using PongClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace TestModel.Entity_test
{
    [TestClass]
    public class Paddle_test
    {
        private readonly int SCREEN_WIDTH = 800;
        private readonly int SCREEN_HEIGHT = 600;

        [TestMethod]
        public void TestInitialPosition()
        {
            // Arrange
            Vector2 initialPosition = new(0, 0);

            // Act
            var game = new GamePong();
            var graphique = game.Content.Load<Texture2D>("Form/paddle");
            Paddle paddle = new(initialPosition.X, initialPosition.Y, null, new Sprite(new Texture2D(new GamePong().GraphicsDevice, 10, 50)));

            // Assert
            Assert.AreEqual(initialPosition, new Vector2(paddle.X, paddle.Y));
        }

        [TestMethod]
        public void TestMove()
        {
            // Arrange
            Vector2 initialPosition = new(0, 0);
            var graphique = new GamePong().GraphicsDevice;
            Paddle paddle = new(initialPosition.X, initialPosition.Y, null, new Sprite(new Texture2D(new GamePong().GraphicsDevice, 10, 50)));

            // Act
            paddle.Move(10, SCREEN_HEIGHT, SCREEN_WIDTH);

            // Assert
            Assert.AreEqual(new Vector2(initialPosition.X, 10), new Vector2(paddle.X, paddle.Y));
        }
    }
}
