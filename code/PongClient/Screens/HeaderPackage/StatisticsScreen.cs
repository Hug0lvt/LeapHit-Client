using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongClient.Screens.HeaderPackage
{
    public class StatisticsScreen : ScreenHeader
    {
        private Texture2D _statisticsTexture;
        private Sprite _soonTexture;
        private SpriteBatch _spriteBatch;

        public StatisticsScreen(GamePong game)
            : base(game)
        {
        }

        public override void LoadContent()
        {
            base.LoadContent();

            _statisticsTexture = Content.Load<Texture2D>("Text/Statistics");
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _soonTexture = new Sprite(Content.Load<Texture2D>("Comming Soon"));
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            _spriteBatch.Begin();

            _spriteBatch.Draw(_statisticsTexture, new Vector2(_widthCenter * 2 - _statisticsTexture.Width, 40 - _statisticsTexture.Height / 2), Color.White);
            _spriteBatch.Draw(_soonTexture, new Vector2(_widthCenter, _heightCenter));

            _spriteBatch.End();
        }
    }
}
