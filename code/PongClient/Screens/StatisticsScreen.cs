using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongClient.Screens
{
    public class StatisticsScreen : ScreenHeader
    {
        private Texture2D _statisticsTexture;

        public StatisticsScreen(GamePong game, GraphicsDevice graphicsDevice, ContentManager content) 
            : base(game, graphicsDevice, content)
        {
            _statisticsTexture = content.Load<Texture2D>("Text/Statistics");
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);

            spriteBatch.Begin();

            spriteBatch.Draw(_statisticsTexture, new Vector2(_widthCenter * 2 - _statisticsTexture.Width, 40 - _statisticsTexture.Height / 2), Color.White);

            spriteBatch.End();
        }
    }
}
