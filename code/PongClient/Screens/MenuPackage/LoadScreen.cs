using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Modele.EntityPackage;
using Modele.GamePackage;
using Modele.MovementPackage;
using Modele.MovementPackage.MotionSensorPackage;
using Modele.Network;
using Modele.PlayerPackage;
using Modele.SkinPackage;
using MonoGame.Extended.Sprites;
using PongClient.Controls;
using ServerCommunication.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game = Modele.GamePackage.Game;

namespace PongClient.Screens.MenuPackage
{
    public class LoadScreen : MenuScreen
    {
        private Texture2D _textureLoadBall;
        private SpriteBatch _spriteBatch;
        private Vector2 centerBall;

        private List<LoadBall> loadedBalls;

        private Game _loadedGame;
        private UserPlayer _localPlayer;
        private string _type;

        public LoadScreen(GamePong game, UserPlayer localPlayer, string type)
            : base(game)
        {
            //_loadedGame = loadedGame;
            _localPlayer = localPlayer;
            _type = type;
        }

        public override void LoadContent()
        {
            base.LoadContent();

            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _textureLoadBall = Content.Load<Texture2D>("Form/loadBall");

            centerBall = new Vector2(_widthCenter - _textureLoadBall.Width / 2, _heightCenter - _textureLoadBall.Height / 2);

            var max = 300;
            var speed = 5;
            var rightBall = new LoadBall(max, centerBall, speed);
            var leftBall = new LoadBall(max, centerBall, -speed);

            var longMax = max * 2;
            var longSpeed = speed * 2;
            var rightLongBall = new LoadBall(longMax, centerBall, longSpeed);
            var leftLongBall = new LoadBall(longMax, centerBall, -longSpeed);

            loadedBalls = new List<LoadBall>()
            {
                rightBall,
                leftBall,
                rightLongBall,
                leftLongBall,
            };

            (_localPlayer.StrategieMovement as MotionSensor).PropertyChanged += OnReadyChanged;
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            _spriteBatch.Begin();

            _spriteBatch.Draw(_textureLoadBall, centerBall, null, Color.White, 0, Vector2.Zero, Vector2.One * 0.5f, SpriteEffects.None, 0);
            foreach (var loadedBall in loadedBalls)
            {
                _spriteBatch.Draw(_textureLoadBall, loadedBall.Position, null, Color.White * 0.5f, 0, Vector2.Zero, Vector2.One * 0.5f, SpriteEffects.None, 0);
            }

            _spriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape)) (_loadedGame.LocalPlayer.StrategieMovement as MotionSensor).StopMovement();
            base.Update(gameTime);

            foreach (var loadedBall in loadedBalls)
            {
                loadedBall.Move();
            }
        }

        private void OnReadyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Ready")
            {
                MotionSensor sensor = sender as MotionSensor;
                bool ready = sensor.Ready;
                if (ready)
                {
                    _localPlayer.Ready = true;
                    switch (_type)
                    {
                        case "local":
                            StartLocalGame();
                            break;
                        default:
                            StartOnlineGame(_type);
                            break;
                    }
                    ScreenManager.LoadScreen(new CountdownScreen(_game, _loadedGame));
                }
            }
        }

        private void StartOnlineGame(string type)
        {
            var socket = new ClientSocket();

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

            switch (type)
            {
                case "online":
                    socket.Connect(playerDto);
                    break;
                case "host":
                    socket.Host(playerDto);
                    break;
                case "join":
                    socket.Join(playerDto, _localPlayer.Profile.Pseudo);
                    break;
            }

            NetworkPlayer.SendPlayer(socket, playerToSend);
            var playerReceive = NetworkPlayer.ReceivePlayer(socket);
            var externalPlayer = new UserPlayer(new User(playerReceive.Pseudo), playerReceive.X, _game, new Modele.MovementPackage.MotionSensorPackage.Mouse());
            externalPlayer.Paddle.Sprite = new Sprite(Content.Load<Texture2D>(_localPlayer.Paddle.Skin));

            var gameStat = new GameStat();

            var ballSkin = new BallSkin("Form/ball", "simple ball");
            var ball = new Ball(_widthCenter, _heightCenter, ballSkin, new Sprite(Content.Load<Texture2D>(ballSkin.Asset)));

            _loadedGame = new GameOnline(_localPlayer, externalPlayer, gameStat, ball, _widthCenter * 2, _heightCenter * 2, Content, socket);
            (_loadedGame.ExternalPlayer.StrategieMovement as MotionSensor).StartMovement();
        }

        private void StartLocalGame()
        {
            var paddleSkin = new PaddleSkin("Form/paddle", "simple paddle");
            var paddleExternalPlayer = new Paddle(_widthCenter * 2 - 100, _heightCenter, paddleSkin, new Sprite(Content.Load<Texture2D>(paddleSkin.Asset)));

            var ballSkin = new BallSkin("Form/ball", "simple ball");
            var ball = new Ball(_widthCenter, _heightCenter, ballSkin, new Sprite(Content.Load<Texture2D>(ballSkin.Asset)));

            var externalPlayer = new Bot(paddleExternalPlayer, ball, _game.BotLevel)
            {
                Ready = true
            };

            var gameStat = new GameStat();

            _loadedGame = new Game(_localPlayer, externalPlayer, gameStat, ball, _widthCenter * 2, _heightCenter * 2, Content);
        }
    }
}
