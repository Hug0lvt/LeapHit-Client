using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Modele.EntityPackage;
using Modele.MovementPackage.MotionSensorPackage;
using Modele.PlayerPackage;
using MonoGame.Extended;
using PongClient.Screens.MenuPackage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game = Modele.GamePackage.Game;

namespace PongClient.Screens
{
    public class EndPartyScreen : PongScreen
    {
        private SpriteBatch _spriteBatch;

        private readonly Game _pongGame;
        private SoundEffectInstance _musicInstance; // Instance de la musique

        public EndPartyScreen(GamePong game, Game pongGame)
          : base(game)
        {
            game.IsMouseVisible = true;
            _pongGame = pongGame;

        }

        public override void LoadContent()
        {
            base.LoadContent();

            _spriteBatch = new SpriteBatch(GraphicsDevice);

            var music = Content.Load<SoundEffect>("Sounds/mainMusic");
            _musicInstance = music.CreateInstance();
            _musicInstance.IsLooped = true; // Lecture en boucle
            _musicInstance.Play(); // Démarrage de la musique
        }

        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();

            _spriteBatch.Draw(_backgroundTexture, new Vector2(0, 0), Color.White);

            _spriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                _game.IsMouseVisible = true;
                _musicInstance.Stop();
                ScreenManager.LoadScreen(new MenuHome(_game));
            }
        }
    }
}
