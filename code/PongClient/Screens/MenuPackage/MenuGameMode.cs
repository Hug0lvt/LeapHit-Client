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
using static System.Net.Mime.MediaTypeNames;
using System.Drawing;
using MonoGame.Extended.BitmapFonts;
using Modele.MovementPackage.MotionSensorPackage;
using Microsoft.Xna.Framework.Input;
using System.Text.Json;
using ServerCommunication.Server;
using Modele.Network;

namespace PongClient.Screens.MenuPackage
{
    public class MenuGameMode : MenuScreen
    {
        private List<Component> _components;
        private SpriteBatch _spriteBatch;

        private UserPlayer _localPlayer;
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
            var hostTexture = new Sprite(Content.Load<Texture2D>("Text/Host"));
            var joinTexture = new Sprite(Content.Load<Texture2D>("Text/Join"));

            var onlineButton = new ButtonHovered(onlineTexture, _whiteRectangleTexture, new Vector2(_widthCenter,
                                                                                                    _heightCenter - 150));
            onlineButton.Click += OnlineButton_Click;


            var localButton = new ButtonHovered(localTexture, _whiteRectangleTexture, new Vector2(_widthCenter,
                                                                                                  onlineButton._position.Y + 100));
            localButton.Click += LocalButton_Click;

            var hostButton = new ButtonHovered(hostTexture, _whiteRectangleTexture, new Vector2(_widthCenter,
                                                                                                  localButton._position.Y + 100));
            hostButton.Click += HostButton_Click;

            var joinButton = new ButtonHovered(joinTexture, _whiteRectangleTexture, new Vector2(_widthCenter,
                                                                                                  hostButton._position.Y + 100));
            joinButton.Click += JoinButton_Click;

            _components = new List<Component>()
            {
                onlineButton,
                localButton,
                joinButton,
                hostButton,
            };

            LoadUserPlayer();
        }

        private void LoadUserPlayer()
        {
            _localPlayer = new UserPlayer(_game.User, 50, _game, _game.GameMode.GetValueOrDefault(_game.SelectedMovement));
        }

        private void OnlineButton_Click(object sender, EventArgs e)
        {
            _menuSoundEffectInstance.Stop();

            var socket = new ClientSocket("hulivet.fr", 3131);

            var playerToSend = new Shared.DTO.UserPlayer()
            {
                Pseudo = _localPlayer.Profile.Pseudo,
                X = (int)(_widthCenter * 2 - _localPlayer.Paddle.X)
            };

            var playerDto = new Shared.DTO.Player()
            {
                name = _localPlayer.Profile.Pseudo,
                nbBallTouchTotal = _localPlayer.Profile.GlobalStat.TouchBallCount,
                timePlayed = _localPlayer.Profile.GlobalStat.TimePlayed,
                playerId = _localPlayer.Profile.Pseudo,
            };

            socket.Connect(playerDto);

            NetworkPlayer.SendPlayer(socket, playerToSend);
            var playerReceive = NetworkPlayer.ReceivePlayer(socket);
            var externalPlayer = new UserPlayer(new User(playerReceive.Pseudo), playerReceive.X, _game, new Modele.MovementPackage.MotionSensorPackage.Mouse());
            externalPlayer.Paddle.Sprite = new Sprite(Content.Load<Texture2D>(_localPlayer.Paddle.Skin));
            
            var gameStat = new GameStat();

            var ballSkin = new BallSkin("Form/ball", "simple ball");
            var ball = new Ball(_widthCenter, _heightCenter, ballSkin, new Sprite(Content.Load<Texture2D>(ballSkin.Asset)));

            _pongGame = new GameOnline(_localPlayer, externalPlayer, gameStat, ball, _widthCenter * 2, _heightCenter * 2, Content, socket);

            ScreenManager.LoadScreen(new LoadScreen(_game, _pongGame));
            (_pongGame.LocalPlayer.StrategieMovement as MotionSensor).StartMovement();
            (_pongGame.ExternalPlayer.StrategieMovement as MotionSensor).StartMovement();
        }

        private void LocalButton_Click(object sender, EventArgs e)
        {
            _menuSoundEffectInstance.Stop();
            var paddleSkin = new PaddleSkin("Form/paddle", "simple paddle");            
            var paddleExternalPlayer = new Paddle(_widthCenter * 2 - 100, _heightCenter, paddleSkin, new Sprite(Content.Load<Texture2D>(paddleSkin.Asset)));

            var ballSkin = new BallSkin("Form/ball", "simple ball");
            var ball = new Ball(_widthCenter, _heightCenter, ballSkin, new Sprite(Content.Load<Texture2D>(ballSkin.Asset)));

            var externalPlayer = new Bot(paddleExternalPlayer, ball, _game.BotLevel)
            {
                Ready = true
            };


            var gameStat = new GameStat();

            _pongGame = new Modele.GamePackage.Game(_localPlayer, externalPlayer, gameStat, ball, _widthCenter*2, _heightCenter*2, Content);

            ScreenManager.LoadScreen(new LoadScreen(_game, _pongGame));
            (_pongGame.LocalPlayer.StrategieMovement as MotionSensor).StartMovement();
        }

        private void HostButton_Click(object sender, EventArgs e)
        {
            ScreenManager.LoadScreen(new HostScreen(_game, _localPlayer));
        }

        private void JoinButton_Click(object sender, EventArgs e)
        {
            ScreenManager.LoadScreen(new JoinScreen(_game, _localPlayer));
        }

        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();

            base.Draw(gameTime);

            foreach (var component in _components)
                component.Draw(gameTime, _spriteBatch);

            var text = "Selected mode : " + _game.SelectedMovement;
            _spriteBatch.DrawString(_game.Font, text, new Vector2(_widthCenter - _game.Font.MeasureString(text).Length() / 2, _heightCenter * 2 - 200), new Microsoft.Xna.Framework.Color(0, 140, 0), 0, Vector2.Zero, Vector2.One, SpriteEffects.None, 0);
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
