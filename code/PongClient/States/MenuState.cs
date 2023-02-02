using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using PongClient.Controls;
using PongClient.States;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace PongClient.Stats
{
    public class MenuState : State
    {
        private List<Component> _components;
        private Texture2D _playIcoTexture;
        private Texture2D _quitTexture;
        private Texture2D _optionTexture;
        private Texture2D _statisticsTexture;
        public int widthCenter;
        public int heightCenter;

        public MenuState(HomeScreen game, GraphicsDevice graphicsDevice, ContentManager content, int ScreenWidth, int ScreenHeight)
          : base(game, graphicsDevice, content)
        {
            _playIcoTexture = _content.Load<Texture2D>("Icon/PlayIco");
            _quitTexture = _content.Load<Texture2D>("Text/Quit");
            _optionTexture = _content.Load<Texture2D>("Text/Option");
            _statisticsTexture = _content.Load<Texture2D>("Text/Statistics");

            widthCenter = ScreenWidth / 2;
            heightCenter = ScreenHeight / 2;

            var newGameButton = new Button(_playIcoTexture, new Vector2(widthCenter - _playIcoTexture.Width / 2,
                                                                                heightCenter - _playIcoTexture.Height - 60));
            newGameButton.Click += NewGameButton_Click;

            var optionButton = new Button(_optionTexture, new Vector2(widthCenter - _optionTexture.Width / 2,
                                                                                heightCenter + 30));
            optionButton.Click += OptionButton_Click;

            var statisticsButton = new Button(_statisticsTexture, new Vector2(widthCenter - _statisticsTexture.Width / 2,
                                                                                    heightCenter + _optionTexture.Height + 30));
            statisticsButton.Click += StatisticsButton_Click;

            var quitGameButton = new Button(_quitTexture, new Vector2(widthCenter - _quitTexture.Width / 2,
                                                                              heightCenter + _optionTexture.Height + _statisticsTexture.Height + 30));
            quitGameButton.Click += QuitGameButton_Click;

            _components = new List<Component>()
            {
                newGameButton,
                optionButton,
                statisticsButton,
                quitGameButton,
            };
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            foreach (var component in _components)
                component.Draw(gameTime, spriteBatch);

            spriteBatch.End();
        }

        private void OptionButton_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("Option");
        }

        private void NewGameButton_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("New Game");
        }

        private void StatisticsButton_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("Statistics");
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var component in _components)
                component.Update(gameTime);
        }

        private void QuitGameButton_Click(object sender, EventArgs e)
        {
            _game.Exit();
        }
    }
}
