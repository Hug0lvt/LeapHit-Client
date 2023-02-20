using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PongClient
{
    public abstract class Component
    {
        public Vector2 _position;
        public Texture2D _texture;

        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);
        public abstract void Update(GameTime gameTime);
        public abstract void UpdatePosition(int widthOld, int heightOld, int widthNew, int heightNew);
    }
}
