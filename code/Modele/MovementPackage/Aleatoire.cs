using Modele.EntityPackage;

namespace Modele.MovementPackage
{
    public class Aleatoire : IMovement
    {
        private readonly Ball _ball;
        private readonly GameEntity _paddle;

        public float ElapsedSeconds { get; set; }
        private float _difficulty;

        public Aleatoire(Ball ball, GameEntity paddle, float difficulty)
        {
            _ball = ball;
            _paddle = paddle;
            _difficulty = difficulty;

        }

        public float GetMovement()
        {
            var paddleSpeed = Math.Abs(_ball.Velocity.Y) * _difficulty;

            if (paddleSpeed < 0)
                paddleSpeed = -paddleSpeed;

            float newPosition = _paddle.Y;

            //ball moving down
            if (_ball.Velocity.Y > 0)
            {
                if (_ball.Y > newPosition)
                    newPosition += paddleSpeed * ElapsedSeconds;
                else
                    newPosition -= paddleSpeed * ElapsedSeconds;
            }

            //ball moving up
            if (_ball.Velocity.Y < 0)
            {
                if (_ball.Y < newPosition)
                    newPosition -= paddleSpeed * ElapsedSeconds;
                else
                    newPosition += paddleSpeed * ElapsedSeconds;
            }

            return newPosition;
        }
    }
}
