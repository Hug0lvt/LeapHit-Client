using Modele.SkinPackage;
using MonoGame.Extended;
using MonoGame.Extended.Sprites;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace Modele.EntityPackage
{
    public class Ball : GameEntity
    {
        private Vector2 velocity;
        private readonly Random _random = new();
        private int _difficulty;
        private int _speed;
        private int _defaultSpeed;
        private float timePass = 0;

        public Ball(float x, float y, Skin skin, Sprite sprite,int difficulty=2,int speed=2)
            : base(x, y, skin, sprite)
        {
            velocity = new Vector2(speed * -100, _random.NextAngle() * 100);
            _difficulty = difficulty;
            _speed = speed;
            _defaultSpeed= speed;
        }

        public Vector2 Velocity { get { return velocity; } set { velocity = value; } }
        public int Difficulty { set { _difficulty = value; } }

        public override void Move(float delta, int screenHeight, int screenWidth)
        {
            timePass += delta;
            if (timePass > 3)
            {
                int mult =(int) velocity.X / _speed;
                if(_speed < 15)
                    _speed += 1;
                timePass = 0;
                velocity.X = _speed * mult;
                
            }
            x += Velocity.X * delta * _difficulty;
            y += Velocity.Y * delta * _difficulty;
          
            var halfHeight = Zone.Height / 2;

            if (y - halfHeight < 0)
            {
                y = halfHeight;
                velocity = new Vector2(velocity.X, -velocity.Y);
            }

            if (y + halfHeight > screenHeight)
            {
                y = screenHeight - halfHeight;
                velocity = new Vector2(velocity.X, -velocity.Y);
            }
        }

        public void Reset(int screenHeight, int screenWidth, bool local)
        {
            x = screenWidth / 2f;
            y = screenHeight / 2f;
            timePass = 0;
            _speed = _defaultSpeed+1;
            _defaultSpeed++;
            if (local)
            {
                velocity = new Vector2(_speed * -100, _random.NextAngle() * 100);
            } else
            {
                velocity = new Vector2(_speed * 100, _random.NextAngle() * 100);
            }
        }
    }
}