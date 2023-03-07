using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Modele.MovementPackage.MotionSensorPackage;
using Modele.PlayerPackage;
using MonoGame.Extended.Sprites;
using PongClient.Controls;
using PongClient.Screens.MenuPackage;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game = Modele.GamePackage.Game;

namespace PongClient.Screens
{
    public class LoadScreen : MenuScreen
    {
        private Texture2D _textureLoadBall;
        private SpriteBatch _spriteBatch;
        private Vector2 centerBall;

        private LoadBall rightBall;
        private LoadBall leftBall;

        private Game _loadedGame;

        public LoadScreen(GamePong game, Game loadedGame) 
            : base(game)
        {
            _loadedGame = loadedGame;
        }

        public override void LoadContent()
        {
            base.LoadContent();

            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _textureLoadBall = Content.Load<Texture2D>("Form/loadBall");

            centerBall = new Vector2(_widthCenter - _textureLoadBall.Width, _heightCenter - _textureLoadBall.Height);

            rightBall = new LoadBall(300, centerBall, 5);
            leftBall = new LoadBall(300, centerBall, -5);

            (_loadedGame.LocalPlayer.StrategieMovement as MotionSensor).PropertyChanged += OnReadyChanged;
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            _spriteBatch.Begin();
            _spriteBatch.Draw(_textureLoadBall, centerBall, Color.White);
            _spriteBatch.Draw(_textureLoadBall, rightBall.Position, Color.White * 0.5f);
            _spriteBatch.Draw(_textureLoadBall, leftBall.Position, Color.White * 0.5f);

            _spriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            rightBall.Move();
            leftBall.Move();
        }

        private void OnReadyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Ready")
            {
                MotionSensor sensor = (MotionSensor)sender;
                bool ready = sensor.Ready;
                if (ready)
                {
                    ScreenManager.LoadScreen(new PartyScreen(_game, _loadedGame));
                }
            }
        }
    }
}
