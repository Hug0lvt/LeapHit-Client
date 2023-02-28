using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using PongClient.Controls;
using System.Diagnostics;

namespace PongClient.Screens.HeaderPackage
{
    public class OptionScreen : ScreenHeader
    {
        private Texture2D _optionTexture;
        private SpriteBatch _spriteBatch;
        protected Texture2D _whiteRectangleTexture;

        public OptionScreen(GamePong game)
            : base(game)
        {
        }

        public override void LoadContent()
        {
            base.LoadContent();

            _optionTexture = Content.Load<Texture2D>("Text/Options");
            _spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            _spriteBatch.Begin();

            _spriteBatch.Draw(_optionTexture, new Vector2(_widthCenter * 2 - _optionTexture.Width, 40 - _optionTexture.Height / 2), Color.White);

            _spriteBatch.End();
        }
    }
}
