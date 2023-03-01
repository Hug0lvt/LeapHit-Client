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
using MonoGame.Extended.Sprites;
using PongClient.Screens.MenuPackage;

namespace PongClient.Screens.HeaderPackage
{
    public class OptionScreen : ScreenHeader
    {
        private Sprite _optionTexture;
        private Sprite _gameModeTexture;
        private Sprite _botLevel;
        private SpriteBatch _spriteBatch;
        private List<Component> _components;

        public OptionScreen(GamePong game)
            : base(game)
        {
        }

        public override void LoadContent()
        {
            base.LoadContent();

            _optionTexture = new Sprite(Content.Load<Texture2D>("Text/Options"));
            _gameModeTexture = new Sprite(Content.Load<Texture2D>("Text/Game Mode"));
            _botLevel = new Sprite(Content.Load<Texture2D>("Text/Bot Level"));
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            var withRectangle = new Sprite(Content.Load<Texture2D>("Form/littleWhiteRectangle"));

            var mouseTexture = new Sprite(Content.Load<Texture2D>("Text/Mouse"));
            var leapTexture = new Sprite(Content.Load<Texture2D>("Text/Leap"));
            var cameraTexture = new Sprite(Content.Load<Texture2D>("Text/Camera"));

            var mouseButton = new ButtonHovered(mouseTexture, withRectangle, new Vector2(_widthCenter / 2,
                                                                                         _heightCenter - 100 + _gameModeTexture.TextureRegion.Height));
            mouseButton.Click += ChangeGameMode;

            var leapButton = new ButtonHovered(leapTexture, withRectangle, new Vector2(_widthCenter / 2,
                                                                                       mouseButton._position.Y + 90));
            leapButton.Click += ChangeGameMode;

            var cameraButton = new ButtonHovered(cameraTexture, withRectangle, new Vector2(_widthCenter / 2,
                                                                                           leapButton._position.Y + 90));
            cameraButton.Click += ChangeGameMode;

            var easyTexture = new Sprite(Content.Load<Texture2D>("Text/Easy"));
            var averageTexture = new Sprite(Content.Load<Texture2D>("Text/Average"));
            var hardTexture = new Sprite(Content.Load<Texture2D>("Text/Hard"));

            var easyButton = new ButtonHovered(easyTexture, withRectangle, new Vector2(_widthCenter * 2 - _widthCenter / 2,
                                                                                         _heightCenter - 100 + _gameModeTexture.TextureRegion.Height));

            var averageButton = new ButtonHovered(averageTexture, withRectangle, new Vector2(_widthCenter * 2 - _widthCenter / 2,
                                                                                       easyButton._position.Y + 90));

            var hardButton = new ButtonHovered(hardTexture, withRectangle, new Vector2(_widthCenter * 2 - _widthCenter / 2,
                                                                                           averageButton._position.Y + 90));

            _components = new List<Component>()
            {
                mouseButton, 
                leapButton, 
                cameraButton,
                easyButton,
                averageButton,
                hardButton,
            };
        }

        public void ChangeGameMode(object sender, EventArgs e)
        {
            var button = sender as Button;
            var mode = button._texture.TextureRegion.Texture.Name.ToLower().Substring(5);
            _game.SelectedMovement = _game.GameMode.GetValueOrDefault(mode);
            ScreenManager.LoadScreen(new MenuHome(_game));
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            _spriteBatch.Begin();

            _spriteBatch.Draw(_optionTexture, new Vector2(_widthCenter * 2 - _optionTexture.TextureRegion.Width / 2, 40), 0, Vector2.One);
            _spriteBatch.Draw(_gameModeTexture, new Vector2(_widthCenter / 2, _heightCenter - 100), 0, Vector2.One);
            _spriteBatch.Draw(_botLevel, new Vector2(_widthCenter * 2 - _widthCenter / 2, _heightCenter - 100), 0, Vector2.One);

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
