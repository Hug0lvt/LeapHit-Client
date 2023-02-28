using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using PongClient.Controls;
using PongClient.Screens.HeaderPackage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongClient.Screens.MenuPackage
{
    public class MenuHome : MenuScreen
    {
        private List<Component> _components;
        private SpriteBatch _spriteBatch;

        public MenuHome(GamePong game)
          : base(game)
        {
        }

        public override void LoadContent()
        {
            base.LoadContent();

            _spriteBatch = new SpriteBatch(GraphicsDevice);

            var playIcoTexture = Content.Load<Texture2D>("Icon/PlayIco");
            var quitTexture = Content.Load<Texture2D>("Text/Quit");
            var optionTexture = Content.Load<Texture2D>("Text/Options");
            var statisticsTexture = Content.Load<Texture2D>("Text/Statistics");
            var friendIcoTexture = Content.Load<Texture2D>("Icon/FriendsIco");
            
            var whitePlay = Content.Load<Texture2D>("Form/PlayWhite");

            var newGameButton = new ButtonHovered(playIcoTexture, whitePlay, new Vector2(_widthCenter - playIcoTexture.Width / 2,
                                                                                 _heightCenter - playIcoTexture.Height - 60),
                                                                             new Vector2(_widthCenter - whitePlay.Width / 2,
                                                                                 _heightCenter - whitePlay.Height - 60));
            newGameButton.Click += NewGameButton_Click;


            var optionButton = new ButtonHovered(optionTexture, _whiteRectangleTexture, new Vector2(_widthCenter - optionTexture.Width / 2,
                                                                                                    _heightCenter + 30),
                                                                                        new Vector2(_widthCenter - _whiteRectangleTexture.Width / 2,
                                                                                                    _heightCenter + 20));
            optionButton.Click += OptionButton_Click;


            var statisticsButton = new ButtonHovered(statisticsTexture, _whiteRectangleTexture, new Vector2(_widthCenter - statisticsTexture.Width / 2,
                                                                                                            _heightCenter + optionTexture.Height + 30),
                                                                                                new Vector2(_widthCenter - _whiteRectangleTexture.Width / 2,
                                                                                                            _heightCenter + optionTexture.Height + 20));
            statisticsButton.Click += StatisticsButton_Click;


            var quitGameButton = new ButtonHovered(quitTexture, _whiteRectangleTexture, new Vector2(_widthCenter - quitTexture.Width / 2,
                                                                                                    _heightCenter + optionTexture.Height + statisticsTexture.Height + 30),
                                                                                        new Vector2(_widthCenter - _whiteRectangleTexture.Width / 2,
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
            ScreenManager.LoadScreen(new MenuGameMode(_game));
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

            base.Draw(gameTime);

            foreach (var component in _components)
                component.Draw(gameTime, _spriteBatch);

            _spriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            foreach (var component in _components)
                component.Update(gameTime);
        }
    }
}
