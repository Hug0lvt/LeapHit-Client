using System;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace PongClient.Screens
{
    public abstract class Screen : IDisposable
    {
        protected ContentManager _content;
        protected GraphicsDeviceManager _graphicsDevice;
        protected GamePong _game;

        protected Texture2D _backgroundTexture;

        protected int _widthCenter;
        protected int _heightCenter;

        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);
        public abstract void Update(GameTime gameTime);

        public void Dispose()
        {
        }

        public Screen(GamePong game, GraphicsDeviceManager graphicsDevice, ContentManager content)
        {
            _game = game;
            _graphicsDevice = graphicsDevice;
            _content = content;

            _backgroundTexture = content.Load<Texture2D>("fond");

            _widthCenter = graphicsDevice.PreferredBackBufferWidth / 2;
            _heightCenter = graphicsDevice.PreferredBackBufferHeight / 2;
        }

     
    }
}
