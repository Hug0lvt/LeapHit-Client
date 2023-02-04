using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using PongClient.Controls;
using PongClient.States;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection.Metadata;
using PongClient.Screen;

namespace PongClient.Stats
{
    public class MenuScreen : States.Screen
    {
        private List<Component> _components;

        private Texture2D _backgroundTexture;
        private Texture2D _leapHitTexture;
        private Texture2D _playIcoTexture;
        private Texture2D _quitTexture;
        private Texture2D _optionTexture;
        private Texture2D _statisticsTexture;
        private Texture2D _friendIcoTexture;

        public int widthCenter;
        public int heightCenter;

        public MenuScreen(GamePong game, GraphicsDevice graphicsDevice, ContentManager content)
          : base(game, graphicsDevice, content)
        {
            _playIcoTexture = _content.Load<Texture2D>("Icon/PlayIco");
            _quitTexture = _content.Load<Texture2D>("Text/Quit");
            _optionTexture = _content.Load<Texture2D>("Text/Option");
            _statisticsTexture = _content.Load<Texture2D>("Text/Statistics");
            _backgroundTexture = _content.Load<Texture2D>("fond");
            _leapHitTexture = _content.Load<Texture2D>("Text/LeapHit");
            _friendIcoTexture = _content.Load<Texture2D>("Icon/FriendsIco");

            widthCenter = graphicsDevice.Viewport.Width / 2;
            heightCenter = graphicsDevice.Viewport.Height / 2;

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


            var friendsButton = new Button(_friendIcoTexture, new Vector2(20,
                                                                                    heightCenter*2 - _friendIcoTexture.Height));
            friendsButton.Click += FriendButton_Click;


            _components = new List<Component>()
            {
                newGameButton,
                optionButton,
                statisticsButton,
                quitGameButton,
                friendsButton,
            };
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(_backgroundTexture, new Vector2(0, 0), Color.White);

            spriteBatch.Draw(_leapHitTexture, new Vector2(widthCenter - _leapHitTexture.Width / 2, 100), Color.White);

            foreach (var component in _components)
                component.Draw(gameTime, spriteBatch);

            spriteBatch.End();
        }

        private void OptionButton_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("Option");
            _game.changeScreen(new GameTime(), new OptionScreen(_game, _graphicsDevice, _content));
        }

        private void NewGameButton_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("New Game");
            _game.changeScreen(new GameTime(), this);
        }

        private void StatisticsButton_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("Statistics");
        }

        private void FriendButton_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("Fiends");
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
