using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using PongClient.Controls;
using PongClient.Screens.HeaderPackage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoGame.Extended.Sprites;
using System.Diagnostics;

namespace PongClient.Screens.MenuPackage
{
    public class MenuHome : MenuScreen
    {
        private List<Component> _components;
        private SpriteBatch _spriteBatch;
        private double timer = 0;
        private Sprite whitePlay;

        public MenuHome(GamePong game)
          : base(game)
        {
        }

        public override void LoadContent()
        {
            base.LoadContent();

            _spriteBatch = new SpriteBatch(GraphicsDevice);

            var playIcoTexture = new Sprite(Content.Load<Texture2D>("Icon/PlayIco"));
            var quitTexture = new Sprite(Content.Load<Texture2D>("Text/Quit"));
            var optionTexture = new Sprite(Content.Load<Texture2D>("Text/Options"));
            var statisticsTexture = new Sprite(Content.Load<Texture2D>("Text/Statistics"));
            var friendIcoTexture = new Sprite(Content.Load<Texture2D>("Icon/FriendsIco"));
            
            whitePlay = new Sprite(Content.Load<Texture2D>("Form/PlayWhite"));

            var newGameButton = new ButtonHovered(playIcoTexture, whitePlay, new Vector2(_widthCenter,
                                                                                 _heightCenter - 200));
            newGameButton.Click += NewGameButton_Click;


            var optionButton = new ButtonHovered(optionTexture, _whiteRectangleTexture, new Vector2(_widthCenter,
                                                                                                    _heightCenter + 30));
            optionButton.Click += OptionButton_Click;


            var statisticsButton = new ButtonHovered(statisticsTexture, _whiteRectangleTexture, new Vector2(_widthCenter,
                                                                                                            optionButton._position.Y + 100));
            statisticsButton.Click += StatisticsButton_Click;


            var quitGameButton = new ButtonHovered(quitTexture, _whiteRectangleTexture, new Vector2(_widthCenter,
                                                                                                    statisticsButton._position.Y + 100));
            quitGameButton.Click += QuitGameButton_Click;


            var friendsButton = new Button(friendIcoTexture, new Vector2(friendIcoTexture.TextureRegion.Width / 2,
                                                                         _heightCenter * 2 - friendIcoTexture.TextureRegion.Height / 2));
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

            if (timer > 10)
            {
                _spriteBatch.Draw(whitePlay, new Vector2(_widthCenter, _heightCenter - 200));
            }

            if (timer > 11) 
            {
                timer = 9;
            }

            foreach (var component in _components)
                component.Draw(gameTime, _spriteBatch);

            _spriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
            timer += gameTime.ElapsedGameTime.TotalSeconds;
            

            base.Update(gameTime);

            foreach (var component in _components)
                component.Update(gameTime);
        }
    }
}
