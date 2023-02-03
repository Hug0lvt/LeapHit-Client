using System;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace PongClient.States
{
    public abstract class Screen : IDisposable
    {
        protected ContentManager _content;
        protected GraphicsDevice _graphicsDevice;
        protected GamePong _game;

        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);
        public abstract void Update(GameTime gameTime);

        public void Dispose()
        {
        }

        public Screen(GamePong game, GraphicsDevice graphicsDevice, ContentManager content)
        {
            _game = game;
            _graphicsDevice = graphicsDevice;
            _content = content;
        }

        
    }
}
