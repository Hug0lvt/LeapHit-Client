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
using System.IO;
using System.Reflection;
using Modele.MovementPackage.MotionSensorPackage.LeapMotionPackage;
using Modele.MovementPackage.MotionSensorPackage;
using Mouse = Modele.MovementPackage.MotionSensorPackage.Mouse;
using System.Drawing.Text;
using System.Text;

namespace PongClient
{
    public class GamePong : Game
    {
        private readonly GraphicsDeviceManager _graphics;
        private readonly ScreenManager _screenManager;
        public SpriteFont Font { get; set; }

        public string SelectedMovement { get; set; }
        public User User { get; }
        public int BotLevel { get; set; }

        private int ScreenResolutionWidth;
        private int ScreenResolutionHeight;




        public Dictionary<string, MotionSensor> GameMode { get; }

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
            SelectedMovement = "mouse";

            _screenManager = Components.Add<ScreenManager>();
            GameMode = new Dictionary<string, MotionSensor>();
        }

        protected override void LoadContent()
        {
            base.LoadContent();

            _screenManager.LoadScreen(new MenuHome(this));
         
            GameMode.Add("mouse", new Mouse());
            GameMode.Add("leap", new LeapMotion());
            GameMode.Add("camera", new Camera());

            BotLevel = 2;

            Font = Content.Load<SpriteFont>("Font/gugi");
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

            base.Update(gameTime);
        }

        protected override void OnExiting(object sender, EventArgs args)
        {
            Debug.WriteLine("fin");
            base.OnExiting(sender, args);
        }
    }
}