using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongClient.Controls
{
    public class ButtonHovered : Button
    {
        private bool _isHovering;
        private Sprite _whiteRectangleTexture;
        public event EventHandler Click;

        public ButtonHovered(Sprite texture, Sprite whiteRectangleTexture, Vector2 position)
            : base(texture, position)
        {
            _whiteRectangleTexture = whiteRectangleTexture;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (_isHovering)
            {
                spriteBatch.Draw(_whiteRectangleTexture, _position, 0, Vector2.One);
            }
            spriteBatch.Draw(_texture, _position, 0, Vector2.One);
        }

        public override void Update(GameTime gameTime)
        {
            _previousMouse = _currentMouse;
            _currentMouse = Mouse.GetState();

            var mouseRectangle = new Rectangle(_currentMouse.X, _currentMouse.Y, 1, 1);

            _isHovering = false;

            if (mouseRectangle.Intersects(new Rectangle((int)_position.X - _texture.TextureRegion.Width / 2, (int)_position.Y - _texture.TextureRegion.Height / 2, _texture.TextureRegion.Width, _texture.TextureRegion.Height)))
            {
                _isHovering = true;

                if (_currentMouse.LeftButton == ButtonState.Released && _previousMouse.LeftButton == ButtonState.Pressed)
                {
                    Click?.Invoke(this, new EventArgs());
                }
            }
        }
    }
}
