using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Modele.MovementPackage.MotionSensorPackage;
using Modele.PlayerPackage;
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
    public class CountdownScreen : MenuScreen
    {
        private readonly Game _loadedGame;

        TimeSpan timerLength = TimeSpan.FromSeconds(3);
        TimeSpan elapsedTime = TimeSpan.Zero;

        SpriteBatch _spriteBatch;

        public CountdownScreen(GamePong game, Game loadedGame) : base(game)
        {
            _loadedGame = loadedGame;
        }

        public override void LoadContent()
        {
            base.LoadContent();

            _spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            _spriteBatch.Begin();

            DrawTime();

            _spriteBatch.End();
        }

        public void DrawTime()
        {
            string text = $"{(timerLength - elapsedTime).Seconds:0}";
            _spriteBatch.DrawString(_game.Font, text, new Vector2(_widthCenter - _game.Font.MeasureString(text).Length() / 2, _heightCenter), Color.White, 0, Vector2.Zero, Vector2.One * 3f, SpriteEffects.None, 0);
        }

        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape)) (_loadedGame.LocalPlayer.StrategieMovement as MotionSensor).StopMovement();
            base.Update(gameTime);

            if(_loadedGame.LocalPlayer.Ready && _loadedGame.ExternalPlayer.Ready)
            {
                elapsedTime += gameTime.ElapsedGameTime;
            }

            if (elapsedTime >= timerLength)
            {
                if (_menuSoundEffectInstance != null && _menuSoundEffectInstance.State == SoundState.Playing)
                {
                    _menuSoundEffectInstance.Stop();
                }
                ScreenManager.LoadScreen(new PartyScreen(_game, _loadedGame));
            }
        }
    }
}
