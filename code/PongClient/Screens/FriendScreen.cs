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
    public class FriendScreen : ScreenHeader
    {
        private Texture2D _friendsTexture;

        public FriendScreen(GamePong game, GraphicsDevice graphicsDevice, ContentManager content) 
            : base(game, graphicsDevice, content)
        {
            _friendsTexture = content.Load<Texture2D>("Text/Friends");
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);

            spriteBatch.Begin();

            spriteBatch.Draw(_friendsTexture, new Vector2(_widthCenter * 2 - _friendsTexture.Width, 40 - _friendsTexture.Height / 2), Color.White);

            spriteBatch.End();
        }
    }
}
