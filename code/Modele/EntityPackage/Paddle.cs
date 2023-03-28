using Modele.PlayerPackage;
using Modele.SkinPackage;
using MonoGame.Extended;
using MonoGame.Extended.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Modele.EntityPackage
{
    public class Paddle : GameEntity
    {

        private readonly Random _random = new();

        public Paddle(float x, float y, Skin skin, Sprite sprite) 
            : base(x, y, skin, sprite)
        {
        }

        public override void Move(float delta, int screenHeight, int screenWidht)
        {
            y = delta;

            if (Zone.Top < 0)
                y = Zone.Height / 2f;

            if (Zone.Bottom > screenHeight)
                y = screenHeight - Zone.Height / 2f;
        }

        public void BallHitPaddle(Ball ball)
        {
            if (ball.Zone.Intersects(zone))
            {
                if (ball.Zone.Left < zone.Left)
                    ball.X = zone.Left - ball.Zone.Width / 2;

                if (ball.Zone.Right > zone.Right)
                    ball.X = zone.Right + ball.Zone.Width / 2;

                ball.Velocity = new Vector2(-ball.Velocity.X, _random.NextAngle() * 100);
            }
        }
    }
}
