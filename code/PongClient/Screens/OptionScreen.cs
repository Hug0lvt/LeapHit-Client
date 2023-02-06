using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using PongClient.Controls;
using System.Diagnostics;

namespace PongClient.Screens
{
    public class OptionScreen : Screen
    {
        private List<Component> _components;

        private Texture2D _backgroundTexture;
        private Texture2D _returnBarTexture;
        private Texture2D _rondBackTexture;
        private Texture2D _rondFrontTexture;
        private Texture2D _returnOptionTexture;
        private Texture2D _returnIcoTexture;

        public int widthCenter;
        public int heightCenter;

        public OptionScreen(GamePong game, GraphicsDevice graphicsDevice, ContentManager content)
            : base(game, graphicsDevice, content)
        {
            _backgroundTexture = _content.Load<Texture2D>("fond");
            _returnBarTexture = _content.Load<Texture2D>("Form/returnBar");
            _rondBackTexture = _content.Load<Texture2D>("Form/rondBack");
            _rondFrontTexture = _content.Load<Texture2D>("Form/rondFront");
            _returnOptionTexture = _content.Load<Texture2D>("Text/returnOption");
            _returnIcoTexture = _content.Load<Texture2D>("Icon/returnIco");

            widthCenter = graphicsDevice.Viewport.Width / 2;
            heightCenter = graphicsDevice.Viewport.Height / 2;

            var buttonReturn = new Button(_returnIcoTexture, new Vector2(20, 40-_returnIcoTexture.Height/2));
            buttonReturn.Click += ReturnButton_Clicked;

            _components = new List<Component>()
            {
                buttonReturn,
            };
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(_backgroundTexture, new Vector2(0, 0), Color.White);
            spriteBatch.Draw(_returnBarTexture, new Vector2(0, 0), Color.White);
            spriteBatch.Draw(_returnOptionTexture, new Vector2(widthCenter*2-_returnOptionTexture.Width, 40-_returnOptionTexture.Height/2), Color.White);
            spriteBatch.Draw(_rondBackTexture, new Vector2(0, 40-_rondBackTexture.Height/2), Color.White);
            spriteBatch.Draw(_rondFrontTexture, new Vector2(-10, 40 - _rondFrontTexture.Height / 2), Color.White);

            foreach (var component in _components)
                component.Draw(gameTime, spriteBatch);

            spriteBatch.End();
        }

        private void ReturnButton_Clicked(object sender, EventArgs e)
        {
            Debug.WriteLine("Return");
            _game.changeScreen(new GameTime(), new MenuScreen(_game, _graphicsDevice, _content));
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var component in _components)
                component.Update(gameTime);
        }
    }
}
