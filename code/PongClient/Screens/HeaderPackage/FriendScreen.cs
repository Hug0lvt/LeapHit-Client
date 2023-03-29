using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Sprites;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Color = Microsoft.Xna.Framework.Color;

namespace PongClient.Screens.HeaderPackage
{
    public class FriendScreen : ScreenHeader
    {
        private Texture2D _friendsTexture;
        private Sprite _soonTexture;
        private SpriteBatch _spriteBatch;

        public FriendScreen(GamePong game)
            : base(game)
        {
        }

        public override void LoadContent()
        {
            base.LoadContent();

            _friendsTexture = Content.Load<Texture2D>("Text/Friends");
            _soonTexture = new Sprite(Content.Load<Texture2D>("Comming Soon"));
            _spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            _spriteBatch.Begin();

            _spriteBatch.Draw(_friendsTexture, new Vector2(_widthCenter * 2 - _friendsTexture.Width, 40 - _friendsTexture.Height / 2), Color.White);
            _spriteBatch.Draw(_soonTexture, new Vector2(_widthCenter, _heightCenter));

            _spriteBatch.End();
        }
    }
}
