using System;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Screens;

namespace PongClient.Screens
{
    public abstract class PongScreen : GameScreen
    {
        protected Texture2D _backgroundTexture;

        protected int _widthCenter;
        protected int _heightCenter;
        protected GamePong _game;

        public PongScreen(GamePong game)
            : base(game)
        {
            _game = game;
            _backgroundTexture = Content.Load<Texture2D>("fond");

            _widthCenter = GraphicsDevice.Viewport.Width / 2;
            _heightCenter = GraphicsDevice.Viewport.Height / 2;
        }
    }
}
