using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PongClient
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Texture2D _backgroundTexture;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);

            _graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            _graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;

            _graphics.IsFullScreen = true;
            _graphics.ApplyChanges();

            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _backgroundTexture = Content.Load<Texture2D>("fond");

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.F11) )
            {
                if (_graphics.IsFullScreen)
                {
                    _graphics.PreferredBackBufferWidth = 1080;
                    _graphics.PreferredBackBufferHeight = 720;
                }
                else
                {
                    _graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
                    _graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
                }

                _graphics.IsFullScreen = !_graphics.IsFullScreen;
                _graphics.ApplyChanges();
            }

            if(Keyboard.GetState().IsKeyDown(Keys.Escape)) Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Beige);

            _spriteBatch.Begin();
            _spriteBatch.Draw(_backgroundTexture, new Vector2(0, 0), Color.White);
            _spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}