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
        private readonly GameEntity _ball;
        private readonly GameEntity _paddle;
        private readonly int _speed;


        public Aleatoire(GameEntity ball, GameEntity paddle, int ballSpeed, int speed)
        {
            _ball = ball;
            _paddle = paddle;

            ((Ball)_ball).Difficulty = ballSpeed;
            _speed = speed;
        }

        public float GetMovement()
        {
            // Ajouter une latence pour que le bot ne suive pas la balle de manière trop précise
            float ballY = _ball.Y + ((Ball)_ball).Velocity.Y * 50;
            float newPosition = 0;

            // Suivre la position de la balle
            if (_paddle.Y < ballY)
            {
                newPosition = _paddle.Y += _speed;
            }
            else if (_paddle.Y > ballY)
            {
                newPosition = _paddle.Y -= _speed;
            }

            // Ajouter une petite aléatoire pour rendre le bot moins prévisible
            if (new Random().Next(0, 100) < 5)
            {
                newPosition += new Random().Next(-1, 2) * _speed;
            }

            return newPosition;
        }

        public void StartMovement()
        {
            throw new NotImplementedException();
        }

        public void StopMovement()
        {
            throw new NotImplementedException();
        }
    }
}
