using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using PongClient.Controls;
using System;
using System.Collections.Generic;
using MonoGame.Extended.Screens;
using System.Xml.Linq;

namespace PongClient.Screens
{
    public class MenuScreen : PongScreen
    {
        private List<Component> _components;
        private Texture2D _leapHitTexture;
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

            var playIcoTexture = Content.Load<Texture2D>("Icon/PlayIco");
            var quitTexture = Content.Load<Texture2D>("Text/Quit");
            var optionTexture = Content.Load<Texture2D>("Text/Option");
            var statisticsTexture = Content.Load<Texture2D>("Text/Statistics");
            var friendIcoTexture = Content.Load<Texture2D>("Icon/FriendsIco");
            var whiteRectangleTexture = Content.Load<Texture2D>("Form/whiteRectangle");
            var whitePlay = Content.Load<Texture2D>("Form/PlayWhite");

            var newGameButton = new ButtonHovered(playIcoTexture, whitePlay, new Vector2(_widthCenter - playIcoTexture.Width / 2,
                                                                                 _heightCenter - playIcoTexture.Height - 60),
                                                                             new Vector2(_widthCenter - whitePlay.Width / 2,
                                                                                 _heightCenter - whitePlay.Height - 60));
            newGameButton.Click += NewGameButton_Click;


            var optionButton = new ButtonHovered(optionTexture, whiteRectangleTexture, new Vector2(_widthCenter - optionTexture.Width / 2,
                                                                                                                                     _heightCenter + 30),
                                                                                                            new Vector2(_widthCenter - whiteRectangleTexture.Width / 2,
                                                                                                                                     _heightCenter + 20));
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
                                                                          _heightCenter * 2 - friendIcoTexture.Height));
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
        
        private void NewGameButton_Click(object sender, EventArgs e)
        {
            ScreenManager.LoadScreen(new PartyScreen(_game));
        }

        private void OptionButton_Click(object sender, EventArgs e)
        {
            ScreenManager.LoadScreen(new OptionScreen(_game));
        }

        private void StatisticsButton_Click(object sender, EventArgs e)
        {
            ScreenManager.LoadScreen(new StatisticsScreen(_game));
        }

        private void FriendButton_Click(object sender, EventArgs e)
        {
            ScreenManager.LoadScreen(new FriendScreen(_game));
        }

        private void QuitGameButton_Click(object sender, EventArgs e)
        {
            _game.Exit();
        }

        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();

            _spriteBatch.Draw(_backgroundTexture, new Rectangle(0, 0, _widthCenter*2, _heightCenter*2), Color.White);
            _spriteBatch.Draw(_leapHitTexture, new Vector2(_widthCenter - _leapHitTexture.Width / 2, 100), Color.White);

            foreach (var component in _components)
                component.Draw(gameTime, _spriteBatch);

            _spriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var component in _components)
                component.Update(gameTime);
        }
    }
}
