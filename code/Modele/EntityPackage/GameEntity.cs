using Modele.SkinPackage;
using MonoGame.Extended.Sprites;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Modele.EntityPackage
{
    public abstract class GameEntity
    {
        protected float x;
        protected float y;
        protected Skin skin;
        protected Sprite sprite;
        protected MonoGame.Extended.RectangleF zone => sprite.GetBoundingRectangle(new Vector2(x, y), 0, Vector2.One);

        protected GameEntity(float x, float y, Skin skin, Sprite sprite)
        {
            this.x = x;
            this.y = y;
            this.skin = skin;
            this.sprite = sprite;
        }

        public float X { get { return x; } set { x = value; } }
        public float Y { get { return y; } set { y = value; } }
        public string Skin { get { return skin.Asset; } }
        public MonoGame.Extended.RectangleF Zone { get { return this.zone; } }
        public Sprite Sprite { get { return this.sprite; } set { this.sprite = value; } }

        public abstract void Move(float delta, int screenHeight, int screenWidth);
    }
}
