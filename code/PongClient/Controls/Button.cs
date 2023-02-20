using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using Color = Microsoft.Xna.Framework.Color;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace PongClient.Controls
{
    public class Button : Component
    {
        public MouseState _currentMouse;
        public MouseState _previousMouse;
        public event EventHandler Click;

        public Button(Texture2D texture, Vector2 position)
        {
            _texture = texture;
            _position = position;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _position, Color.White);
        }

        public override void Update(GameTime gameTime)
        {
            _previousMouse = _currentMouse;
            _currentMouse = Mouse.GetState();

            var mouseRectangle = new Rectangle(_currentMouse.X, _currentMouse.Y, 1, 1);

            if (mouseRectangle.Intersects(new Rectangle((int)_position.X, (int)_position.Y, _texture.Width, _texture.Height)))
            {
                if (_currentMouse.LeftButton == ButtonState.Released && _previousMouse.LeftButton == ButtonState.Pressed)
                {
                    Click?.Invoke(this, new EventArgs());
                }
            }
        }

        public override void UpdatePosition(int widthOld, int heightOld, int widthNew, int heightNew)
        {
            float newPosX = _position.X * widthNew / widthOld;
            float newPosY = _position.Y * heightNew / heightOld;
            _position = new Vector2(newPosX, newPosY);
        }
    }
}
