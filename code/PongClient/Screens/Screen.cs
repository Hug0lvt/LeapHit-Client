using System;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace PongClient.Screens
{
    public abstract class Screen : IDisposable
    {
        protected ContentManager _content;
        protected GraphicsDevice _graphicsDevice;
        protected GamePong _game;

        protected Texture2D _backgroundTexture;

        protected int _widthCenter;
        protected int _heightCenter;

        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);
        public abstract void Update(GameTime gameTime);

        public void Dispose()
        {
        }

        public Screen(GamePong game)
        {
            _game = game;
            _graphicsDevice = game.GraphicsDevice;
            _content = game.Content;

            _backgroundTexture = _content.Load<Texture2D>("fond");

            _widthCenter = _graphicsDevice.Viewport.Width / 2;
            _heightCenter = _graphicsDevice.Viewport.Height / 2;
        }
    }
}
