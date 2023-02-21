using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using PongClient.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongClient.Screens
{
    public class ScreenHeader : Screen
    {
        private Texture2D _returnBarTexture;
        private Texture2D _rondBackTexture;
        private Texture2D _rondFrontTexture;
        private Texture2D _returnIcoTexture;

        private Button buttonReturn;
        private int heightBar = 80;

        public ScreenHeader(GamePong game)
            : base(game)
        {
            _returnBarTexture = _content.Load<Texture2D>("Form/returnBar");
            _rondBackTexture = _content.Load<Texture2D>("Form/rondBack");
            _rondFrontTexture = _content.Load<Texture2D>("Form/rondFront");
            _returnIcoTexture = _content.Load<Texture2D>("Icon/returnIco");

            buttonReturn = new Button(_returnIcoTexture, new Vector2(20, heightBar/2 - _returnIcoTexture.Height / 2));
            buttonReturn.Click += ReturnButton_Clicked;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(_backgroundTexture, new Vector2(0, 0), Color.White);
            spriteBatch.Draw(_returnBarTexture, new Vector2(0, 0), Color.White);
            spriteBatch.Draw(_rondBackTexture, new Vector2(0, heightBar / 2 - _rondBackTexture.Height / 2), Color.White);
            spriteBatch.Draw(_rondFrontTexture, new Vector2(-10, heightBar / 2 - _rondFrontTexture.Height / 2), Color.White);
            buttonReturn.Draw(gameTime, spriteBatch);

            spriteBatch.End();
        }

        private void ReturnButton_Clicked(object sender, EventArgs e)
        {
            _game.changeScreen(new GameTime(), new MenuScreen(_game));
        }

        public override void Update(GameTime gameTime)
        {
            buttonReturn.Update(gameTime);
        }
    }
}
