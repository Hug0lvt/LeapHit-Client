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
using Vector2 = Microsoft.Xna.Framework.Vector2;
using MonoGame.Extended;

namespace PongClient.Screens
{
    public class PartyScreen : PongScreen
    {
        private SpriteBatch _spriteBatch;
        private Texture2D _rectangleHautTexture;
        private Texture2D _rectangleBasTexture;

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

            var ball = new Ball(_widthCenter, _heightCenter, ballSkin);

            var paddleLocalPlayer = new Paddle(50, _heightCenter, paddleSkin);
            var paddleExternalPlayer = new Paddle(_widthCenter*2 - 100, _heightCenter, paddleSkin);


            localPlayer = new User(paddleLocalPlayer, ball, new Modele.MovementPackage.Mouse(), "loris");
            externalPlayer = new Bot(paddleExternalPlayer, ball, 5);
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
            var texturePaddle = Content.Load<Texture2D>(player.Paddle.Skin);
            _spriteBatch.Draw(texturePaddle, new Vector2(player.Paddle.X, player.Paddle.Y - texturePaddle.Height / 2), Color.White);
        }

        public void DrawBall(Ball ball)
        {
            _spriteBatch.Draw(Content.Load<Texture2D>(ball.Skin), new Vector2(ball.X, ball.Y), Color.White);
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
            localPlayer.Ball.Move(gameTime.GetElapsedSeconds());
        }

        //private void ConstrainPaddle(Paddle paddle)
        //{
        //    var texturePaddle = Content.Load<Texture2D>(player.Paddle.Skin);

        //    if (paddle.BoundingRectangle.Left < 0)
        //        paddle.Position.X = paddle.BoundingRectangle.Width / 2f;

        //    if (paddle.BoundingRectangle.Bottom > ScreenHeight)
        //        paddle.Position.Y = ScreenHeight - paddle.BoundingRectangle.Height / 2f;
        //}
    }
}
