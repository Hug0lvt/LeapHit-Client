using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace PongClient.Controls
{
    public class Card : Component
    {
        public Sprite Texture;
        public Vector2 Position;
        public RectangleF Zone => Texture.GetBoundingRectangle(Position, 0, Vector2.One);

        public Card(Sprite texture, Vector2 position)
        {
            Texture = texture;
            Position = position;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, 0, Vector2.One);
        }

        public override void Update(GameTime gameTime)
        {
            
        }
    }
}
