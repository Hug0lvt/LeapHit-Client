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

namespace Modele.EntityPackage
{
    public class Item : GameEntity
    {
        private static Skin _skin= new Skin("Icon/returnIco", "speed");
        private static Random _random=new Random();
        public Item(int screenWidth,ContentManager contentManager) :
            base(500, 0, _skin, new Sprite(contentManager.Load<Texture2D>(_skin.Asset)))
        {

        }

        public override void Move(float delta, int screenHeight, int screenWidth)
        {
            y += 1;
            if (y >= screenHeight)
                throw new ExceptionItemDelete();
        }

        public void BallHitItem(Ball ball)
        {
            if (ball.Zone.Intersects(zone))
            {
                ball.Velocity = new Vector2(ball.Velocity.X*10, Y );
                throw new ExceptionItemDelete();         
            }
          
        }

       
    }
}
