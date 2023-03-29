using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using PongClient.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection.Metadata;
using PongClient.Screens;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Screens;
using Modele.EntityPackage;
using Modele.PlayerPackage;
using Modele.SkinPackage;
using Modele.MovementPackage;
using System.Numerics;
using MonoGame.Extended;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.TextureAtlases;
using Vector2 = Microsoft.Xna.Framework.Vector2;
using Modele.GamePackage;
using System.Timers;
using MonoGame.Extended.BitmapFonts;
using PongClient.Screens.MenuPackage;
using Microsoft.Xna.Framework.Audio;
using Game = Modele.GamePackage.Game;
using static System.Net.Mime.MediaTypeNames;
using Modele.MovementPackage.MotionSensorPackage;
using Modele.EntityPackage.Items;

namespace PongClient.Screens
{
    public class PartyScreen : PongScreen
    {
        private SpriteBatch _spriteBatch;
        private Texture2D _rectangleHautTexture;
        private Texture2D _rectangleBasTexture;

        private readonly Game _pongGame;
        private SoundEffectInstance _musicInstance; // Instance de la musique
        private readonly TimeSpan timerLength = TimeSpan.FromMinutes(2);
        private readonly int maxScore = 6;

        public PartyScreen(GamePong game, Game pongGame)
          : base(game)
        {
            game.IsMouseVisible = false;
            _pongGame = pongGame;
          
        }

        public override void LoadContent()
        {
            base.LoadContent();

            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _rectangleBasTexture = Content.Load<Texture2D>("Form/RectangleBas");
            _rectangleHautTexture = Content.Load<Texture2D>("Form/RectangleHaut");

            var music = Content.Load<SoundEffect>("Sounds/gameplay");
            _musicInstance = music.CreateInstance();
            _musicInstance.IsLooped = true; // Lecture en boucle
            _musicInstance.Play(); // Démarrage de la musique
        }

        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();

            _spriteBatch.Draw(_backgroundTexture, new Vector2(0, 0), Color.White);

            DrawBall(_pongGame.Ball);

            _spriteBatch.Draw(_rectangleHautTexture, new Rectangle(0, 0, _widthCenter * 2, _rectangleHautTexture.Height), Color.White);
            _spriteBatch.Draw(_rectangleBasTexture, new Rectangle(0, _heightCenter * 2 - _rectangleBasTexture.Height, _widthCenter * 2, _rectangleBasTexture.Height), Color.White);


            DrawItem(_pongGame._item);
            DrawScore();
            DrawTime();

            DrawPaddle(_pongGame.LocalPlayer);
            DrawPaddle(_pongGame.ExternalPlayer);

            _spriteBatch.End();
        }

        public void DrawPaddle(Player player)
        { 
            _spriteBatch.Draw(player.Paddle.Sprite, new Vector2(player.Paddle.X, player.Paddle.Y), 0, Vector2.One);
        }

        public void DrawBall(Ball ball)
        {
            _spriteBatch.Draw(ball.Sprite, new Vector2(ball.X, ball.Y), 0, Vector2.One);
        }

        public void DrawItem(Item item)
        {

            if (item != null)
            {
                _spriteBatch.Draw(item.Sprite, new Vector2(item.X, _pongGame._item.Y), 0, Vector2.One);
            }
        }

        public void DrawScore()
        {
            var text = _pongGame.GameStat.Score.GetScore().Item1 + " : " + _pongGame.GameStat.Score.GetScore().Item2;
            _spriteBatch.DrawString(_game.Font, text, new Vector2(_widthCenter - (_game.Font.MeasureString(text).Length()*1.5f) / 2, 10), Color.Black, 0.1f, Vector2.Zero, Vector2.One * 1.5f, SpriteEffects.None, 0);
        }

        public void DrawTime()
        {
            string text = $"{(timerLength - _pongGame.GameStat.Time).Minutes:00}:{(timerLength - _pongGame.GameStat.Time).Seconds:00}";
            _spriteBatch.DrawString(_game.Font, text, new Vector2(_widthCenter - _game.Font.MeasureString(text).Length() / 2, _heightCenter * 2 - 75), Color.Black, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, 0);
        }

        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape) 
                || _pongGame.GameStat.Time >= timerLength 
                || _pongGame.GameStat.Score.GetScore().Item1 >= maxScore 
                || _pongGame.GameStat.Score.GetScore().Item2 >= maxScore)
            {
                _game.IsMouseVisible = true;
                _musicInstance.Stop();
                (_pongGame.LocalPlayer.StrategieMovement as MotionSensor).StopMovement();
                if(Keyboard.GetState().IsKeyDown(Keys.Escape)) ScreenManager.LoadScreen(new MenuHome(_game));
                else ScreenManager.LoadScreen(new EndPartyScreen(_game, _pongGame));
            }

            _pongGame.GameStat.Time += gameTime.ElapsedGameTime;
            
          
            _pongGame.Play(_widthCenter * 2, _heightCenter * 2, gameTime.GetElapsedSeconds());
        }
    }
}
