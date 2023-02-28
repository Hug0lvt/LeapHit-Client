using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using PongClient.Controls;
using System;
using System.Collections.Generic;
using MonoGame.Extended.Screens;
using System.Xml.Linq;
using Microsoft.Xna.Framework.Input;

namespace PongClient.Screens.MenuPackage
{
    public class MenuScreen : PongScreen
    {
        private Texture2D _leapHitTexture;
        protected Texture2D _whiteRectangleTexture;
        private SpriteBatch _spriteBatch;

        public MenuScreen(GamePong game)
          : base(game)
        {
        }

        public override void LoadContent()
        {
            base.LoadContent();

            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _leapHitTexture = Content.Load<Texture2D>("Text/LeapHit");
            _whiteRectangleTexture = Content.Load<Texture2D>("Form/whiteRectangle");
        }

        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();

            _spriteBatch.Draw(_backgroundTexture, new Rectangle(0, 0, _widthCenter * 2, _heightCenter * 2), Color.White);
            _spriteBatch.Draw(_leapHitTexture, new Vector2(_widthCenter - _leapHitTexture.Width / 2, 100), Color.White);

            _spriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                ScreenManager.LoadScreen(new MenuHome(_game));
            }
        }
    }
}
