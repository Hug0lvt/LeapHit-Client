using Modele.EntityPackage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Modele.MovementPackage
{
    public class Aleatoire : IMovement
    {
        private Ball ball;
        private GameEntity paddle;
        private float paddleSpeed;

        public Aleatoire(GameEntity ball, GameEntity paddle, float difficulty)
        {
            this.ball = (Ball)ball;
            this.paddle = paddle;
            this.paddleSpeed = difficulty;
        }

        public float GetMovement()
        {
            var newPosition = 0f;

            //ball moving down
            if (ball.Velocity.Y > 0)
            {
                if (ball.Y > paddle.Y)
                    newPosition = paddle.Y + paddleSpeed;
                else
                    newPosition = paddle.Y - paddleSpeed;
            }

            //ball moving up
            if (ball.Velocity.Y < 0)
            {
                if (ball.Y < paddle.Y)
                    newPosition = paddle.Y + paddleSpeed;
                else
                    newPosition = paddle.Y - paddleSpeed;
            }

            return newPosition;
        }
    }
}
