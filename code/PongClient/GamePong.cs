using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Modele.MovementPackage;
using Modele.PlayerPackage;
using Modele.SkinPackage;
using MonoGame.Extended;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Screens.Transitions;
using PongClient.Screens;
using PongClient.Screens.MenuPackage;
using System.Collections.Generic;
using System.Diagnostics;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace PongClient
{
    public class GamePong : Game
    {
        private readonly GraphicsDeviceManager _graphics;
        private readonly ScreenManager _screenManager;
        public SpriteFont Font { get; set; }

        public IMovement SelectedMovement { get; set; }
        public User User { get; }
        public int BotLevel { get; set; }

        public Dictionary<string, IMovement> GameMode { get; }

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

            User = new User("loris");
            SelectedMovement = new Modele.MovementPackage.Mouse();

            _screenManager = Components.Add<ScreenManager>();
            GameMode = new Dictionary<string, IMovement>();
        }

        protected override void LoadContent()
        {
            base.LoadContent();

            _screenManager.LoadScreen(new MenuHome(this));

            GameMode.Add("mouse", new Modele.MovementPackage.Mouse());
            GameMode.Add("leap", new LeapMotion());
            GameMode.Add("camera", new Camera());

            BotLevel = 1;
            Font = Content.Load<SpriteFont>("Font/font-20");
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Microsoft.Xna.Framework.Input.Keys.F11) )
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

            System.Windows.Forms.Screen screen = System.Windows.Forms.Screen.PrimaryScreen;
            Debug.WriteLine(screen.Bounds.Width + " " + screen.Bounds.Height);

            base.Update(gameTime);
        }
    }
}