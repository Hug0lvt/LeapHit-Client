using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Modele.PlayerPackage;
using MonoGame.Extended.Sprites;
using PongClient.Screens.MenuPackage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game = Modele.GamePackage.Game;

namespace PongClient.Screens
{
    public class LoadScreen : MenuScreen
    {
        private Sprite _textureLoad;
        private SpriteBatch _spriteBatch;

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
            _textureLoad = new Sprite(Content.Load<Texture2D>("Animation/loader"));
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            _spriteBatch.Begin();
            _spriteBatch.Draw(_textureLoad, new Vector2(_widthCenter, _heightCenter), 0, Vector2.One);
            _spriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
