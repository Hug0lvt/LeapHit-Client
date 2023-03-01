using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using PongClient.Controls;
using PongClient.Screens.HeaderPackage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoGame.Extended.Content;
using Modele.EntityPackage;
using Modele.GamePackage;
using Modele.PlayerPackage;
using Modele.SkinPackage;
using MonoGame.Extended.Sprites;
using System.Diagnostics;

namespace PongClient.Screens.MenuPackage
{
    public class MenuGameMode : MenuScreen
    {
        private List<Component> _components;
        private SpriteBatch _spriteBatch;

        private Player _localPlayer;
        private Modele.GamePackage.Game _pongGame;

        public MenuGameMode(GamePong game) 
            : base(game)
        {
        }

        public override void LoadContent()
        {
            base.LoadContent();

            _spriteBatch = new SpriteBatch(GraphicsDevice);

            var onlineTexture = new Sprite(Content.Load<Texture2D>("Text/Online"));
            var localTexture = new Sprite(Content.Load<Texture2D>("Text/Local"));

            var onlineButton = new ButtonHovered(onlineTexture, _whiteRectangleTexture, new Vector2(_widthCenter,
                                                                                                    _heightCenter - 100));
            onlineButton.Click += OnlineButton_Click;


            var localButton = new ButtonHovered(localTexture, _whiteRectangleTexture, new Vector2(_widthCenter,
                                                                                                  onlineButton._position.Y + 150));
            localButton.Click += LocalButton_Click;

            _components = new List<Component>()
            {
                onlineButton,
                localButton,
            };

            LoadUserPlayer();
        }

        private void LoadUserPlayer()
        {
            _localPlayer = new UserPlayer(_game.User, 50, _game, _game.SelectedMovement);
        }

        private void OnlineButton_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("Online");
        }

        private void LocalButton_Click(object sender, EventArgs e)
        {
            var paddleSkin = new PaddleSkin("Form/paddle", "simplePaddle");            
            var paddleExternalPlayer = new Paddle(_widthCenter * 2 - 100, _heightCenter, _game.User.SelectedPaddle, new Sprite(Content.Load<Texture2D>(paddleSkin.Asset)));

            var externalPlayer = new Bot(paddleExternalPlayer, _localPlayer.Ball, 1);
            
            var gameStat = new GameStat();

            _pongGame = new Modele.GamePackage.Game(_localPlayer, externalPlayer, gameStat);

            ScreenManager.LoadScreen(new PartyScreen(_game, _pongGame));
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
