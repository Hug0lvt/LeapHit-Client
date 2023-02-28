using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using PongClient.Controls;
using System.Diagnostics;
using MonoGame.Extended.Content;
using MonoGame.Extended.Timers;

namespace PongClient.Screens.HeaderPackage
{
    public class OptionScreen : ScreenHeader
    {
        private Texture2D _optionTexture;
        private Texture2D _gameModeTexture;
        private SpriteBatch _spriteBatch;
        private List<Component> _components;

        public OptionScreen(GamePong game)
            : base(game)
        {
        }

        public override void LoadContent()
        {
            base.LoadContent();

            _optionTexture = Content.Load<Texture2D>("Text/Options");
            _gameModeTexture = Content.Load<Texture2D>("Text/Game Mode");
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            var mouseTexture = Content.Load<Texture2D>("Text/Mouse");
            var leapTexture = Content.Load<Texture2D>("Text/Leap");
            var cameraTexture = Content.Load<Texture2D>("Text/Camera");
            var withRectangle = Content.Load<Texture2D>("Form/whiteRectangle");

            var mouseButton = new ButtonHovered(mouseTexture, withRectangle, new Vector2(_widthCenter / 2 - mouseTexture.Width / 2,
                                                                                         _heightCenter - 100 + _gameModeTexture.Height), 
                                                                             new Vector2(0, 0));

            var leapButton = new ButtonHovered(leapTexture, withRectangle, new Vector2(_widthCenter / 2 - leapTexture.Width / 2,
                                                                                       mouseButton._position.Y + 70),
                                                                           new Vector2(0, 0));

            var cameraButton = new ButtonHovered(cameraTexture, withRectangle, new Vector2(_widthCenter / 2 - cameraTexture.Width / 2,
                                                                                           leapButton._position.Y + 70),
                                                                               new Vector2(0, 0));

            _components = new List<Component>()
            {
                mouseButton, 
                leapButton, 
                cameraButton 
            };
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            _spriteBatch.Begin();

            _spriteBatch.Draw(_optionTexture, new Vector2(_widthCenter * 2 - _optionTexture.Width, 40 - _optionTexture.Height / 2), Color.White);
            _spriteBatch.Draw(_gameModeTexture, new Vector2(_widthCenter / 2 - _gameModeTexture.Width / 2,
                                                             _heightCenter - 100), Color.White);

            foreach (var component in _components)
                component.Draw(gameTime, _spriteBatch);

            _spriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            foreach (var component in _components)
                component.Update(gameTime);
        }
}
}
