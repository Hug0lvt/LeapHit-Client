using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using PongClient.Controls;
using PongClient.States;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection.Metadata;
using PongClient.Screen;

namespace PongClient.Stats
{
    public class Party : States.Screen
    {
        private Texture2D _backgroundTexture;
        private Texture2D _rectangleHautTexture;
        private Texture2D _rectangleBasTexture;

        public int widthCenter;
        public int heightCenter;

        public Party(GamePong game, GraphicsDevice graphicsDevice, ContentManager content)
          : base(game, graphicsDevice, content)
        {
            _backgroundTexture = _content.Load<Texture2D>("fond");

            widthCenter = graphicsDevice.Viewport.Width / 2;
            heightCenter = graphicsDevice.Viewport.Height / 2;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(_backgroundTexture, new Vector2(0, 0), Color.White);

            spriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
        }
    }
}
