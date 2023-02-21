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

namespace PongClient.Screens
{
    public class PartyScreen : PongScreen
    {
        private SpriteBatch _spriteBatch;
        private Texture2D _rectangleHautTexture;
        private Texture2D _rectangleBasTexture;

        private User localPlayer;

        public PartyScreen(GamePong game)
          : base(game)
        {
        }

        public override void LoadContent()
        {
            base.LoadContent();

            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _rectangleBasTexture = Content.Load<Texture2D>("Form/RectangleBas");
            _rectangleHautTexture = Content.Load<Texture2D>("Form/RectangleHaut");

            var paddlePlayer = new Paddle(50, _heightCenter,
                                 new System.Numerics.Vector2(1), new PaddleSkin("Form/paddle", "simplePaddle"));

            localPlayer = new User(paddlePlayer, null, new Modele.MovementPackage.Mouse(), "loris");
        }

        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();

            _spriteBatch.Draw(_backgroundTexture, new Vector2(0, 0), Color.White);
            _spriteBatch.Draw(_rectangleHautTexture, new Vector2(0, 0), Color.White);
            _spriteBatch.Draw(_rectangleBasTexture, new Vector2(0, _heightCenter * 2 - _rectangleBasTexture.Height), Color.White);

            DrawPaddle(localPlayer);

            _spriteBatch.End();
        }

        public void DrawPaddle(Player player)
        {
            var texturePaddle = Content.Load<Texture2D>(player.Paddle.Skin);
            _spriteBatch.Draw(texturePaddle, new Vector2(player.Paddle.X, player.Paddle.Y - texturePaddle.Height / 2), Color.White);
        }

        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                ScreenManager.LoadScreen(new MenuScreen(_game));
            }

            localPlayer.Paddle.Move(localPlayer.StrategieMovement.GetMovement());
        }
    }
}
