using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Input;
using MonoGame.Extended.Input.InputListeners;
using PongClient.Controls;
using System.Drawing;
using Color = Microsoft.Xna.Framework.Color;
using static System.Net.Mime.MediaTypeNames;
using MonoGame.Extended.BitmapFonts;
using PongClient.Screens.HeaderPackage;
using MonoGame.Extended.Sprites;
using System.Diagnostics;
using Modele.EntityPackage;
using Modele.GamePackage;
using Modele.MovementPackage.MotionSensorPackage;
using Modele.Network;
using Modele.SkinPackage;
using PongClient.Screens.MenuPackage;
using ServerCommunication.Server;
using Modele.PlayerPackage;
using System.Windows.Forms;
using Keys = Microsoft.Xna.Framework.Input.Keys;

namespace PongClient.Screens
{
    public class JoinScreen : ScreenHeader
    {
        private TextInput _textInput;
        private Sprite _whiteRectangleTexture;
        private SpriteFont _font;
        private UserPlayer _localPlayer;

        public JoinScreen(GamePong game, UserPlayer localPlayer) : base(game)
        {
            _localPlayer = localPlayer;
        }

        public override void LoadContent()
        {
            base.LoadContent();

            _font = Content.Load<SpriteFont>("Font/font-20");
            _textInput = new TextInput(_font);
            _whiteRectangleTexture = new Sprite(Content.Load<Texture2D>("Form/whiteRectangle"));
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            _spriteBatch.Begin();

            _spriteBatch.Draw(_whiteRectangleTexture, new Vector2(_widthCenter, _heightCenter - 170));

            if (_textInput != null && !String.IsNullOrEmpty(_textInput.Text))
            {
                _textInput.Draw(_spriteBatch, new Vector2(_widthCenter - _font.MeasureString(_textInput.Text).Length() / 2, _heightCenter - 200), Color.White);
            }

            _spriteBatch.DrawString(_font, "Tab to write", new Vector2(_widthCenter - _font.MeasureString("Tab to write").Length() / 2, _heightCenter), Color.White);
            _spriteBatch.DrawString(_font, "Escape to delete", new Vector2(_widthCenter - _font.MeasureString("Escape to delete").Length() / 2, _heightCenter + 60), Color.White);
            _spriteBatch.DrawString(_font, "Enter to valid (6 letters)", new Vector2(_widthCenter - _font.MeasureString("Enter to valid (6 letters)").Length() / 2, _heightCenter + 120), Color.White);

            _spriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (Keyboard.GetState().IsKeyDown(Keys.Tab))
            {
                _textInput.StartEditing();
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Enter) && _textInput.Text.Length == 6 && _textInput.IsEditing)
            {
                _textInput.StopEditing();
                StartGame();
            }

            _textInput.Update(gameTime);
        }

        private void StartGame()
        {
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

            socket.Join(playerDto, _textInput.Text);

            NetworkPlayer.SendPlayer(socket, playerToSend);
            var playerReceive = NetworkPlayer.ReceivePlayer(socket);
            var externalPlayer = new UserPlayer(new User(playerReceive.Pseudo), playerReceive.X, _game, new Modele.MovementPackage.MotionSensorPackage.Mouse());
            externalPlayer.Paddle.Sprite = new Sprite(Content.Load<Texture2D>(_localPlayer.Paddle.Skin));

            var gameStat = new GameStat();

            var ballSkin = new BallSkin("Form/ball", "simple ball");
            var ball = new Ball(_widthCenter, _heightCenter, ballSkin, new Sprite(Content.Load<Texture2D>(ballSkin.Asset)));

            var _pongGame = new GameOnline(_localPlayer, externalPlayer, gameStat, ball, _widthCenter * 2, _heightCenter * 2, Content, socket);

            ScreenManager.LoadScreen(new LoadScreen(_game, _pongGame));
            (_pongGame.LocalPlayer.StrategieMovement as MotionSensor).StartMovement();
            (_pongGame.ExternalPlayer.StrategieMovement as MotionSensor).StartMovement();
        }
    }
}
