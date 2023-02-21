using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using PongClient.Controls;
using System;
using System.Collections.Generic;

namespace PongClient.Screens
{
    public class MenuScreen : Screen
    {
        private readonly List<Component> _components;

        private readonly Texture2D _leapHitTexture;

        public MenuScreen(GamePong game)
          : base(game)
        {
            var playIcoTexture = _content.Load<Texture2D>("Icon/PlayIco");
            var quitTexture = _content.Load<Texture2D>("Text/Quit");
            var optionTexture = _content.Load<Texture2D>("Text/Option");
            var statisticsTexture = _content.Load<Texture2D>("Text/Statistics");
            _leapHitTexture = _content.Load<Texture2D>("Text/LeapHit");
            var friendIcoTexture = _content.Load<Texture2D>("Icon/FriendsIco");
            var whiteRectangleTexture = _content.Load<Texture2D>("Form/whiteRectangle");
            var whitePlay = _content.Load<Texture2D>("Form/PlayWhite");

            var newGameButton = new ButtonHovered(playIcoTexture, whitePlay, new Vector2(_widthCenter - playIcoTexture.Width / 2,
                                                                                _heightCenter - playIcoTexture.Height - 60), 
                                                                            new Vector2(_widthCenter - whitePlay.Width / 2,
                                                                                _heightCenter - whitePlay.Height - 60));
            newGameButton.Click += NewGameButton_Click;


            var optionButton = new ButtonHovered(optionTexture, whiteRectangleTexture, new Vector2(_widthCenter - optionTexture.Width / 2,
                                                                                                                                     _heightCenter + 30), 
                                                                                                            new Vector2(_widthCenter - whiteRectangleTexture.Width / 2,
                                                                                                                                     _heightCenter + 20 ));
            optionButton.Click += OptionButton_Click;


            var statisticsButton = new ButtonHovered(statisticsTexture, whiteRectangleTexture, new Vector2(_widthCenter - statisticsTexture.Width / 2,
                                                                                                                                              _heightCenter + optionTexture.Height + 30),
                                                                                                                        new Vector2(_widthCenter - whiteRectangleTexture.Width / 2,
                                                                                                                                                 _heightCenter + optionTexture.Height + 20));
            statisticsButton.Click += StatisticsButton_Click;


            var quitGameButton = new ButtonHovered(quitTexture, whiteRectangleTexture, new Vector2(_widthCenter - quitTexture.Width / 2,
                                                                                                                                   _heightCenter + optionTexture.Height + statisticsTexture.Height + 30), 
                                                                                                                new Vector2(_widthCenter - whiteRectangleTexture.Width / 2,
                                                                                                                                    _heightCenter + optionTexture.Height + statisticsTexture.Height + 20));
            quitGameButton.Click += QuitGameButton_Click;


            var friendsButton = new Button(friendIcoTexture, new Vector2(20,
                                                                          _heightCenter *2 - friendIcoTexture.Height));
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

            spriteBatch.Draw(_backgroundTexture, new Rectangle(0, 0, _widthCenter*2, _heightCenter*2), Color.White);
            spriteBatch.Draw(_leapHitTexture, new Vector2(_widthCenter - _leapHitTexture.Width / 2, 100), Color.White);

            foreach (var component in _components)
                component.Draw(gameTime, spriteBatch);

            spriteBatch.End();
        }

        private void OptionButton_Click(object sender, EventArgs e)
        {
            _game.changeScreen(new GameTime(), new OptionScreen(_game));
        }

        private void NewGameButton_Click(object sender, EventArgs e)
        {
            _game.changeScreen(new GameTime(), new PartyScreen(_game));
        }

        private void StatisticsButton_Click(object sender, EventArgs e)
        {
            _game.changeScreen(new GameTime(), new StatisticsScreen(_game));
        }

        private void FriendButton_Click(object sender, EventArgs e)
        {
            _game.changeScreen(new GameTime(), new FriendScreen(_game));
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
