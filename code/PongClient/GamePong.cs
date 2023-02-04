using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using PongClient.States;
using PongClient.Stats;

namespace PongClient
{
    public class GamePong : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private States.Screen _currentScreen;


        public GamePong()
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

            _currentScreen = new MenuScreen(this, _graphics.GraphicsDevice, Content);
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

            _currentScreen.Update(gameTime);

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Beige);

            _currentScreen.Draw(gameTime, _spriteBatch);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }

        public void changeScreen(GameTime gameTime, States.Screen screen)
        {
            _currentScreen.Dispose();
            _currentScreen = screen;
            _currentScreen.Draw(gameTime, _spriteBatch);
        }
    }
}