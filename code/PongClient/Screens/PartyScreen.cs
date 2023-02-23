using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using PongClient.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection.Metadata;
using PongClient.Screens;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Screens;
using Modele.EntityPackage;
using Modele.PlayerPackage;
using Modele.SkinPackage;
using Modele.MovementPackage;
using System.Numerics;
using MonoGame.Extended;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.TextureAtlases;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace PongClient.Screens
{
    public class PartyScreen : PongScreen
    {
        private SpriteBatch _spriteBatch;
        private Texture2D _rectangleHautTexture;
        private Texture2D _rectangleBasTexture;

        private FastRandom _random = new FastRandom();
        private User localPlayer;
        private Player externalPlayer;

        public PartyScreen(GamePong game)
          : base(game)
        {
            game.IsMouseVisible = false;
        }

        public override void LoadContent()
        {
            base.LoadContent();

            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _rectangleBasTexture = Content.Load<Texture2D>("Form/RectangleBas");
            _rectangleHautTexture = Content.Load<Texture2D>("Form/RectangleHaut");

            var paddleSkin = new PaddleSkin("Form/paddle", "simplePaddle");
            var ballSkin = new BallSkin("Form/ball", "simpleBall");

            var ball = new Ball(_widthCenter, _heightCenter, ballSkin, new Sprite(Content.Load<Texture2D>(ballSkin.Asset)));

            var paddleLocalPlayer = new Paddle(50, _heightCenter, paddleSkin, new Sprite(Content.Load<Texture2D>(paddleSkin.Asset)));
            var paddleExternalPlayer = new Paddle(_widthCenter*2 - 100, _heightCenter, paddleSkin, new Sprite(Content.Load<Texture2D>(paddleSkin.Asset)));


            localPlayer = new User(paddleLocalPlayer, ball, new Modele.MovementPackage.Mouse(), "loris");
            externalPlayer = new Bot(paddleExternalPlayer, ball, 1);
        }

        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();

            _spriteBatch.Draw(_backgroundTexture, new Vector2(0, 0), Color.White);
            _spriteBatch.Draw(_rectangleHautTexture, new Vector2(0, 0), Color.White);
            _spriteBatch.Draw(_rectangleBasTexture, new Vector2(0, _heightCenter * 2 - _rectangleBasTexture.Height), Color.White);

            DrawPaddle(localPlayer);
            DrawPaddle(externalPlayer);
            DrawBall(localPlayer.Ball);

            _spriteBatch.End();
        }

        public void DrawPaddle(Player player)
        { 
            _spriteBatch.Draw(player.Paddle.Sprite, new Vector2(player.Paddle.X, player.Paddle.Y), 0, Vector2.One);
        }

        public void DrawBall(Ball ball)
        {
            _spriteBatch.Draw(ball.Sprite, new Vector2(ball.X, ball.Y), 0, Vector2.One);
        }

        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                _game.IsMouseVisible = true;
                ScreenManager.LoadScreen(new MenuScreen(_game));
            }

            localPlayer.Paddle.Move(localPlayer.StrategieMovement.GetMovement());
            externalPlayer.Paddle.Move(externalPlayer.StrategieMovement.GetMovement());

            ConstrainPaddle(localPlayer);
            ConstrainPaddle(externalPlayer);

            var externalMovement = (Aleatoire)externalPlayer.StrategieMovement;
            externalMovement.ElapsedSeconds = gameTime.GetElapsedSeconds();
            
            ConstrainBall(localPlayer.Ball);
            localPlayer.Ball.Move(gameTime.GetElapsedSeconds());
            
        }

        private void ConstrainPaddle(Player player)
        {
            if (player.Paddle.Zone.Top < 0)
                player.Paddle.Y = player.Paddle.Zone.Height / 2f;

            if (player.Paddle.Zone.Bottom > _heightCenter*2)
                player.Paddle.Y = _heightCenter*2 - player.Paddle.Zone.Height / 2f;
        }

        private void ConstrainBall(Ball ball)
        {
            var halfHeight = ball.Zone.Height / 2;
            var halfWidth = ball.Zone.Width / 2;

            if (ball.Y - halfHeight < 0)
            {
                ball.Y = halfHeight;
                ball.Velocity = new Vector2(ball.Velocity.X, -ball.Velocity.Y);
            }

            if (ball.Y + halfHeight > _heightCenter * 2)
            {
                ball.Y = _heightCenter * 2 - halfHeight;
                ball.Velocity = new Vector2(ball.Velocity.X, -ball.Velocity.Y);
            }

            if (ball.X > _widthCenter * 2 + halfWidth && ball.Velocity.X > 0)
            {
                ball.X = _widthCenter * 2 / 2f;
                ball.Y = _heightCenter * 2 / 2f;
                ball.Velocity = new Vector2(_random.Next(2, 5) * -100, _random.NextAngle() * 100);
            }

            if (ball.X < -halfWidth && ball.Velocity.X < 0)
            {
                ball.X = _widthCenter * 2 / 2f;
                ball.Y = _heightCenter * 2 / 2f;
                ball.Velocity = new Vector2(_random.Next(2, 5) * 100, _random.NextAngle() * 100);
            }
        }
    }
}
