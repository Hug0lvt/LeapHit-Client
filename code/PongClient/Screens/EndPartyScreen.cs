using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Modele.EntityPackage;
using Modele.MovementPackage.MotionSensorPackage;
using Modele.PlayerPackage;
using MonoGame.Extended;
using MonoGame.Extended.BitmapFonts;
using MonoGame.Extended.Sprites;
using PongClient.Controls;
using PongClient.Screens.MenuPackage;
using Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using Game = Modele.GamePackage.Game;
using Player = Modele.PlayerPackage.Player;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace PongClient.Screens
{
    public class EndPartyScreen : PongScreen
    {
        private SpriteBatch _spriteBatch;

        private readonly Game _pongGame;
        private SoundEffectInstance _musicInstance; // Instance de la musique

        private Button newGameButton;
        private Card localPlayerRectangle;
        private Card externalPlayerRectangle;
        private Card rectangle1;
        private Card rectangle2;
        private Card rectangle3;

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

            var playerRectangleTexture = new Sprite(Content.Load<Texture2D>("Form/sheet"));
            var rectangle1Texture = new Sprite(Content.Load<Texture2D>("Form/rectangle1"));
            var rectangle2Texture = new Sprite(Content.Load<Texture2D>("Form/rectangle2"));
            var rectangle3Texture = new Sprite(Content.Load<Texture2D>("Form/rectangle3"));

            localPlayerRectangle = new Card(playerRectangleTexture, new Vector2(_widthCenter / 2, _heightCenter / 2));
            externalPlayerRectangle = new Card(playerRectangleTexture, new Vector2(_widthCenter + _widthCenter / 2, _heightCenter / 2));

            rectangle1 = new Card(rectangle1Texture, new Vector2(_widthCenter, localPlayerRectangle.Zone.Bottom + rectangle1Texture.TextureRegion.Height));
            rectangle2 = new Card(rectangle2Texture, new Vector2(rectangle1.Zone.Left - rectangle1Texture.TextureRegion.Width, localPlayerRectangle.Zone.Bottom + rectangle1Texture.TextureRegion.Height));
            rectangle3 = new Card(rectangle3Texture, new Vector2(rectangle1.Zone.Right + rectangle1Texture.TextureRegion.Width, localPlayerRectangle.Zone.Bottom + rectangle1Texture.TextureRegion.Height));

            var newGameButtonTexture = new Sprite(Content.Load<Texture2D>("Form/newGameButton"));

            newGameButton = new Button(newGameButtonTexture, new Vector2(_widthCenter, rectangle1.Zone.Bottom + rectangle1Texture.TextureRegion.Height));
            newGameButton.Click += NewGameButton_Click;
        }
            

        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();

            _spriteBatch.Draw(_backgroundTexture, new Vector2(0, 0), Color.White);

            DrawParty(gameTime, _pongGame);
            DrawPlayer(gameTime, _pongGame.LocalPlayer, localPlayerRectangle, "You");
            DrawPlayer(gameTime, _pongGame.ExternalPlayer, externalPlayerRectangle, "Opponent");

            newGameButton.Draw(gameTime, _spriteBatch);

            _spriteBatch.End();
        }

        public void DrawPlayer(GameTime gameTime, Player player, Card playerCard, string text)
        {
            playerCard.Draw(gameTime, _spriteBatch);
            _spriteBatch.DrawString(_game.Font, text, new Vector2(playerCard.Position.X - _game.Font.MeasureString(text).Length() / 2, playerCard.Position.Y - 80), Color.White);    
        }

        public void DrawParty(GameTime gameTime, Game game)
        {
            rectangle1.Draw(gameTime, _spriteBatch);
            rectangle2.Draw(gameTime, _spriteBatch);
            rectangle3.Draw(gameTime, _spriteBatch);
            var text = game.GameStat.Score.GetWinner() == game.LocalPlayer ? "Winner" : "Loser";
            var you = game.GameStat.Score.GetScore().Item1.ToString();
            var opponent = game.GameStat.Score.GetScore().Item2.ToString();
            _spriteBatch.DrawString(_game.Font, text, new Vector2(rectangle1.Position.X - _game.Font.MeasureString(text).Length() / 2, rectangle1.Position.Y - 30), Color.Black);
            _spriteBatch.DrawString(_game.Font, you, new Vector2(rectangle2.Position.X - _game.Font.MeasureString(you).Length() / 2, rectangle2.Position.Y - 30), Color.Black);
            _spriteBatch.DrawString(_game.Font, opponent, new Vector2(rectangle3.Position.X - _game.Font.MeasureString(opponent).Length() / 2, rectangle3.Position.Y - 30), Color.Black);
        }

        private void NewGameButton_Click(object sender, EventArgs e)
        {
            ScreenManager.LoadScreen(new MenuGameMode(_game));
        }

        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                _game.IsMouseVisible = true;
                _musicInstance.Stop();
                ScreenManager.LoadScreen(new MenuHome(_game));
            }

            newGameButton.Update(gameTime);
        }
    }
}
