﻿using Microsoft.Xna.Framework;
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
using ServerCommunication.Api;

namespace PongClient
{
    public class GamePong : Game
    {
        private readonly GraphicsDeviceManager _graphics;
        private readonly ScreenManager _screenManager;
        public SpriteFont Font { get; set; }

        public string SelectedMovement { get; set; }
        public User User { get; }
        public float BotLevel { get; set; }

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

            var id = new IdManager();
            User = new User(id.ManageId());
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

            BotLevel = 1.6f;

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

            base.Update(gameTime);
        }

        protected override void OnExiting(object sender, EventArgs args)
        {
            Debug.WriteLine("fin");
            base.OnExiting(sender, args);
        }
    }
}