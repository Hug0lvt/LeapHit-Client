using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using Color = Microsoft.Xna.Framework.Color;
using Rectangle = Microsoft.Xna.Framework.Rectangle;
using MonoGame.Extended.Sprites;

namespace PongClient.Controls
{
    public class Button : Component
    {
        public MouseState _currentMouse;
        public MouseState _previousMouse;
        public Sprite _texture;
        public event EventHandler Click;
        public Vector2 _position;

        public Button(Sprite texture, Vector2 position)
        {
            _texture = texture;
            _position = position;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _position, 0, Vector2.One);
        }

        public override void Update(GameTime gameTime)
        {
            _previousMouse = _currentMouse;
            _currentMouse = Mouse.GetState();

            var mouseRectangle = new Rectangle(_currentMouse.X, _currentMouse.Y, 1, 1);

            if (mouseRectangle.Intersects(new Rectangle((int)_position.X - _texture.TextureRegion.Width / 2, (int)_position.Y - _texture.TextureRegion.Height / 2, _texture.TextureRegion.Width, _texture.TextureRegion.Height)))
            {
                if (_currentMouse.LeftButton == ButtonState.Released && _previousMouse.LeftButton == ButtonState.Pressed)
                {
                    Click?.Invoke(this, new EventArgs());
                }
            }
        }
    }
}
