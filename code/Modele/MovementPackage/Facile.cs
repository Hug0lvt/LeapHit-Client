using Microsoft.Xna.Framework;
using Modele.EntityPackage;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Modele.MovementPackage
{
    public class Facile : Aleatoire
    {
        public Facile(GameEntity ball, GameEntity paddle) 
            : base(ball, paddle)
        {
            velocity = new Vector2(200, 10);
        }

        public override float GetAleatoireMovement()
        {
            var newPosition = 0f;
            const float difficulty = 0.9f;

            //if (paddleSpeed < 0)
            //    paddleSpeed = -paddleSpeed;

            ////ball moving down
            //if (ball.Velocity.Y > 0)
            //{
            //    if (ball.Y > paddle.Y)
            //        newPosition = paddle.Y + paddleSpeed * elapsedSeconds;
            //    else
            //        newPosition = paddle.Y - paddleSpeed * elapsedSeconds;
            //}

            ////ball moving up
            //if (ball.Velocity.Y < 0)
            //{
            if (ball.Y > paddle.Y)
                newPosition = paddle.Y + (ball.Y - paddle.Y );
            else
                newPosition = paddle.Y + (paddle.Y -  ball.Y );


            //newPosition = paddle.Y + ball.Y * velocity.Y;

            //x += Velocity.X * delta * difficulty;
            //y += Velocity.Y * delta * difficulty;

            //newPosition = ball.Y;

            return newPosition;
        }
    }
}
