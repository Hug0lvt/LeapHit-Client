using Modele.EntityPackage;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace Modele.MovementPackage
{
    public class Aleatoire : IMovement
    {
        private readonly Ball _ball;
        private readonly GameEntity _paddle;
        private readonly int _speed;
        private readonly int _difficulty;
        private float _targetY;



        public Aleatoire(Ball ball, GameEntity paddle, int difficulty)
        {
            _ball = ball;
            _paddle = paddle;
            _speed = difficulty * 4;
            _difficulty = difficulty;
            _targetY = _ball.Y;
        }

        public float GetMovement()
        {
            Random random= new Random();
            float ballY = _ball.Y - 20 + _ball.Velocity.Y * 50;

            // Calculer une cible de déplacement en fonction de la position de la balle
            if (Math.Abs(_targetY - ballY) > 50 && _ball.Velocity.X > 0)
            {
                _targetY = ballY + (_paddle.X - _ball.X) / _ball.Velocity.X * _ball.Velocity.Y;
            }

            // Suivre la cible de déplacement
            float newPosition = _paddle.Y;
            if (_paddle.Y < _targetY - _speed)
            {
                newPosition += _speed;
            }
            else if (_paddle.Y > _targetY + _speed)
            {
                newPosition -= _speed;
            }

            return newPosition;
        }
    }
}
