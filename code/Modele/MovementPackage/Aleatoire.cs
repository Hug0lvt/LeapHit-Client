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


        public Aleatoire(Ball ball, GameEntity paddle, int difficulty)
        {
            _ball = ball;
            _paddle = paddle;

            _ball.Difficulty = 2;
            _speed = difficulty * 3 - 2;
        }

        public float GetMovement()
        {
            // Ajouter une latence pour que le bot ne suive pas la balle de manière trop précise
            float ballY = _ball.Y + _ball.Velocity.Y * 50;
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
