using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Modele.PlayerPackage;
using MonoGame.Extended.Sprites;
using PongClient.Controls;
using PongClient.Screens.HeaderPackage;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Color = Microsoft.Xna.Framework.Color;
using System.Windows.Forms;
using System.Threading;
using Modele.EntityPackage;
using Modele.GamePackage;
using Modele.MovementPackage.MotionSensorPackage;
using Modele.Network;
using Modele.SkinPackage;
using PongClient.Screens.MenuPackage;
using ServerCommunication.Server;
using System.Diagnostics;
using Microsoft.Xna.Framework.Input;
using Keys = Microsoft.Xna.Framework.Input.Keys;

namespace PongClient.Screens
{
    public class HostScreen : ScreenHeader
    {
        private Sprite _whiteRectangleTexture;
        private SpriteFont _font;
        private UserPlayer _localPlayer;

        public HostScreen(GamePong game, UserPlayer localPlayer) : base(game)
        {
            _localPlayer = localPlayer;

            Thread staThread = new Thread(() =>
            {
                Clipboard.SetText(_localPlayer.Profile.Pseudo);
            });

            staThread.SetApartmentState(ApartmentState.STA); // Set the thread to use STA model
            staThread.Start();
        }

        public override void LoadContent()
        {
            base.LoadContent();

            _font = Content.Load<SpriteFont>("Font/font-20");
            _whiteRectangleTexture = new Sprite(Content.Load<Texture2D>("Form/whiteRectangle"));
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            _spriteBatch.Begin();

            _spriteBatch.Draw(_whiteRectangleTexture, new Vector2(_widthCenter, _heightCenter - 170));

            _spriteBatch.DrawString(_font, _localPlayer.Profile.Pseudo, new Vector2(_widthCenter - _font.MeasureString(_localPlayer.Profile.Pseudo).Length() / 2, _heightCenter - 200), Color.White);

            _spriteBatch.DrawString(_font, "The room id is in your clipboard", new Vector2(_widthCenter - _font.MeasureString("The room id is in your clipboard").Length() / 2, _heightCenter + 60), Color.White);
            _spriteBatch.DrawString(_font, "Send it to your friend !!! <3", new Vector2(_widthCenter - _font.MeasureString("Send it to your friend !!! <3").Length() / 2, _heightCenter + 120), Color.White);
            _spriteBatch.DrawString(_font, "Enter to start game", new Vector2(_widthCenter - _font.MeasureString("Enter to start game").Length() / 2, _heightCenter + 180), Color.White);

            _spriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                StartGame();
            }
        }

        private void StartGame()
        {
            ScreenManager.LoadScreen(new LoadScreen(_game, _localPlayer, "host"));
            (_localPlayer.StrategieMovement as MotionSensor).StartMovement();
        }
    }
}
