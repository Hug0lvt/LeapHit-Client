using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Input;
using MonoGame.Extended.Input.InputListeners;
using System;
using System.Diagnostics;

namespace PongClient.Controls
{

    public class TextInput
    {
        private KeyboardListener _keyboardListener;
        private string _text;
        private string _displayText;
        private SpriteFont _font;
        private bool _isEditing;

        public string Text { get { return _text; } set { _text = value; } }
        public bool IsEditing { get { return _isEditing; } }

        public TextInput(SpriteFont font)
        {
            _keyboardListener = new KeyboardListener();
            _keyboardListener.KeyTyped += KeyTyped;
            _keyboardListener.KeyReleased += KeyReleased;
            _text = "";
            _displayText = "";
            _font = font;
            _isEditing = false;
        }

        public void StartEditing()
        {
            _isEditing = true;
        }

        public void StopEditing()
        {
            _isEditing = false;
        }

        public void Update(GameTime gameTime)
        {
            _keyboardListener.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position, Color color)
        {
            spriteBatch.DrawString(_font, _displayText, position, color);
        }

        private void KeyTyped(object sender, KeyboardEventArgs e)
        {
            if (_isEditing && e.Key != Keys.Back && e.Key != Keys.Enter && e.Key != Keys.Escape && e.Key != Keys.Tab)
            {
                if(_displayText.Length < 6 ) 
                { 
                    _text += e.Character;
                    _displayText = _text;
                }
            }
        }

        private void KeyReleased(object sender, KeyboardEventArgs e)
        {
            if (_isEditing && e.Key == Keys.Back && _text.Length > 0)
            {
                _text = _text.Substring(0, _text.Length - 1);
                _displayText = _text;
            }
            else if (_isEditing && e.Key == Keys.Enter)
            {
                StopEditing();
            }
            else if (_isEditing && e.Key == Keys.Escape)
            {
                _text = "";
                _displayText = "";
                StopEditing();
            }
        }
    }

}
