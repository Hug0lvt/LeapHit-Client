using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Modele.SkinPackage;
using MonoGame.Extended.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Modele.EntityPackage.Items
{
    public abstract class Item : GameEntity
    {
        protected static Random _random = new Random();

        public Item(float x, float y, Skin skin, Sprite sprite)
            : base (x, y, skin, sprite)
        {
            
        }

        public override void Move(float delta, int screenHeight, int screenWidth)
        {
            {
                y += 2;
                if (y >= screenHeight)
                    throw new ExceptionItemDelete();
            }
        }

        public abstract void BallHitItem(Ball ball);


    }
}
