using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Modele.PlayerPackage;
using Modele.SkinPackage;
using MonoGame.Extended;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Screens.Transitions;
using PongClient.Screens;
using PongClient.Screens.MenuPackage;

namespace PongClient
{
    public class GamePong : Game
    {
        private GraphicsDeviceManager _graphics;
        private readonly ScreenManager _screenManager;

        private User user;
        public User User { get { return user; } }

        public GamePong()
        {
            _graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width,
                PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height,
                IsFullScreen = true,
            };

            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            _screenManager = Components.Add<ScreenManager>();
        }

        protected override void LoadContent()
        {
            base.LoadContent();

            _screenManager.LoadScreen(new MenuHome(this));

            user = new User("loris");
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

            base.Update(gameTime);
        }
    }
}