using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PongClient.States;
using PongClient.Stats;

namespace PongClient
{
    public class HomeScreen : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Texture2D _backgroundTexture;
        private Texture2D _playIcoTexture;
        private Texture2D _leapHitTexture;
        private Texture2D _quitTexture;
        private Texture2D _optionTexture;
        private Texture2D _statisticsTexture;
        private Texture2D _friendsTexture;
        private Texture2D _friendIcoTexture;

        private State _currentState;

        public int ScreenWidth => GraphicsDevice.Viewport.Width;
        public int ScreenHeight => GraphicsDevice.Viewport.Height;

        public HomeScreen()
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
            _leapHitTexture = Content.Load<Texture2D>("Text/LeapHit");
            _friendIcoTexture = Content.Load<Texture2D>("Icon/FriendsIco");

            _currentState = new MenuState(this, _graphics.GraphicsDevice, Content, ScreenWidth, ScreenHeight);
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            /*if (Keyboard.GetState().IsKeyDown(Keys.F11) )
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
            }*/

            if(Keyboard.GetState().IsKeyDown(Keys.Escape)) Exit();

            _currentState.Update(gameTime);

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Beige);

            _spriteBatch.Begin();

            _spriteBatch.Draw(_backgroundTexture, new Vector2(0, 0), Color.White);

            _spriteBatch.Draw(_leapHitTexture, new Vector2((ScreenWidth / 2) - _leapHitTexture.Width / 2, 100), Color.White);
            
            _spriteBatch.End();

            _currentState.Draw(gameTime, _spriteBatch);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}