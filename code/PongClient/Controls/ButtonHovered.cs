using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongClient.Controls
{
    public class ButtonHovered : Button
    {
        private MouseState _currentMouse;
        private bool _isHovering;
        private MouseState _previousMouse;
        private Texture2D _texture;
        private Texture2D _whiteRectangleTexture;
        public event EventHandler Click;
        public Vector2 _position;
        public Vector2 _position2;

        public ButtonHovered(Texture2D texture, Texture2D whiteRectangleTexture, Vector2 position, Vector2 whitePosition)
            : base(texture, position)
        {
            _texture = texture;
            _position = position;
            _position2 = whitePosition;
            _whiteRectangleTexture = whiteRectangleTexture;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (_isHovering)
            {
                spriteBatch.Draw(_whiteRectangleTexture, _position2, Color.White);
            }
            spriteBatch.Draw(_texture, _position, Color.White);
        }

        public override void Update(GameTime gameTime)
        {
            _previousMouse = _currentMouse;
            _currentMouse = Mouse.GetState();

            var mouseRectangle = new Rectangle(_currentMouse.X, _currentMouse.Y, 1, 1);

            _isHovering = false;

            if (mouseRectangle.Intersects(new Rectangle((int)_position.X, (int)_position.Y, _texture.Width, _texture.Height)))
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
