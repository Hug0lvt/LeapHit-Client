using Modele.EntityPackage;
using System;
using System.Collections.Generic;
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
        }

        public override float GetAleatoireMovement()
        {
            var newPosition = 0f;
            const float difficulty = 1.0f;
            var paddleSpeed = Math.Abs(ball.Velocity.Y) * difficulty;

            if (paddleSpeed < 0)
                paddleSpeed = -paddleSpeed;

            //ball moving down
            if (ball.Velocity.Y > 0)
            {
                if (ball.Y > paddle.Y)
                    newPosition = paddle.Y + paddleSpeed * elapsedSeconds;
                else
                    newPosition = paddle.Y - paddleSpeed * elapsedSeconds;
            }

            //ball moving up
            if (ball.Velocity.Y < 0)
            {
                if (ball.Y < paddle.Y)
                    newPosition = paddle.Y + paddleSpeed * elapsedSeconds;
                else
                    newPosition = paddle.Y - paddleSpeed * elapsedSeconds;
            }

            return newPosition;
        }
    }
}
