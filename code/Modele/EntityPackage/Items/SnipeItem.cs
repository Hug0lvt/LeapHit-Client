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
    public class SnipeItem : Item
    {
        private static Skin skinItem = new Skin("Icon/flashsmall", "speed");
        public SnipeItem(int screenWidth, ContentManager contentManager) : 
            base(_random.Next(screenWidth / 6, screenWidth - screenWidth / 6), 0, skinItem, new Sprite(contentManager.Load<Texture2D>(skinItem.Asset)))
        {
        }

        public override void BallHitItem(Ball ball)
        {    
            if (ball.Zone.Intersects(zone))
            {
                ball.Velocity = new Vector2(ball.Velocity.X * 5, Y);
                throw new ExceptionItemDelete();
            }            
        }
    }
}
