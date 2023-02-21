using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using PongClient.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection.Metadata;
using PongClient.Screens;

namespace PongClient.Screens
{
    public class PartyScreen : Screen
    {
        private Texture2D _rectangleHautTexture;
        private Texture2D _rectangleBasTexture;

        public PartyScreen(GamePong game)
          : base(game)
        {
            _rectangleBasTexture = _content.Load<Texture2D>("Form/RectangleBas");
            _rectangleHautTexture = _content.Load<Texture2D>("Form/RectangleHaut");
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

            spriteBatch.Begin();

            spriteBatch.Draw(_backgroundTexture, new Vector2(0, 0), Color.White);
            spriteBatch.Draw(_rectangleHautTexture, new Vector2(0, 0), Color.White);
            spriteBatch.Draw(_rectangleBasTexture, new Vector2(0, _heightCenter * 2 - _rectangleBasTexture.Height), Color.White);

            spriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
        }
    }
}
