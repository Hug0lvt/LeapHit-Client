using Microsoft.Xna.Framework.Content;
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
        public SnipeItem(int screenWidth, ContentManager contentManager) : base(screenWidth, contentManager)
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

        public override void Move(float delta, int screenHeight, int screenWidth)
        {
            {
                y += 1;
                if (y >= screenHeight)
                    throw new ExceptionItemDelete();
            }
        }
    }
}
