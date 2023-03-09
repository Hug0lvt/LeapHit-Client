using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Modele.MovementPackage;
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

        private List<LoadBall> loadedBalls;

        private readonly Game _loadedGame;

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

            centerBall = new Vector2(_widthCenter - _textureLoadBall.Width/2, _heightCenter - _textureLoadBall.Height/2);
            
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

            (_loadedGame.LocalPlayer.StrategieMovement as MotionSensor).PropertyChanged += OnReadyChanged;
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
                    _loadedGame.LocalPlayer.Ready = true;
                    ScreenManager.LoadScreen(new CountdownScreen(_game, _loadedGame));
                }
            }
        }
    }
}
