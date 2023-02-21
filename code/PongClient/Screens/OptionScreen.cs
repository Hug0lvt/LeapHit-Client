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

namespace PongClient.Screens
{
    public class OptionScreen : ScreenHeader
    {
        private Texture2D _optionTexture;

        public OptionScreen(GamePong game) 
            : base(game)
        {
            _optionTexture = _content.Load<Texture2D>("Text/Option");
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);

            spriteBatch.Begin();

            spriteBatch.Draw(_optionTexture, new Vector2(_widthCenter * 2 - _optionTexture.Width, 40 - _optionTexture.Height / 2), Color.White);

            spriteBatch.End();
        }
    }
}
