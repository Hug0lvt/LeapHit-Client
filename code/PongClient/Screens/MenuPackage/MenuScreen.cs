using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using PongClient.Controls;
using System;
using System.Collections.Generic;
using MonoGame.Extended.Screens;
using System.Xml.Linq;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Sprites;

namespace PongClient.Screens.MenuPackage
{
    public class MenuScreen : PongScreen
    {
        private Sprite _leapHitTexture;
        protected Sprite _whiteRectangleTexture;
        private SpriteBatch _spriteBatch;

        public MenuScreen(GamePong game)
          : base(game)
        {
        }

        public override void LoadContent()
        {
            base.LoadContent();

            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _leapHitTexture = new Sprite(Content.Load<Texture2D>("Text/LeapHit"));
            _whiteRectangleTexture = new Sprite(Content.Load<Texture2D>("Form/whiteRectangle"));
        }

        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();

            _spriteBatch.Draw(_backgroundTexture, new Rectangle(0, 0, _widthCenter * 2, _heightCenter * 2), Color.White);
            _spriteBatch.Draw(_leapHitTexture, new Vector2(_widthCenter, 100), 0, Vector2.One);

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
