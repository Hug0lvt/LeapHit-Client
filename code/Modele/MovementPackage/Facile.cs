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
            Init();
        }

        public override void Init()
        {
            ball.Difficulty = 1;
        }

        public override float GetAleatoireMovement()
        {
            // Ajouter une latence pour que le bot ne suive pas la balle de manière trop précise
            float ballY = ball.Y + ball.Velocity.Y * 50;
            float newPosition = 0;
            var speed = 1;

            // Suivre la position de la balle
            if (paddle.Y < ballY)
            {
                newPosition = paddle.Y += speed;
            }
            else if (paddle.Y > ballY)
            {
                newPosition = paddle.Y -= speed;
            }

            // Ajouter une petite aléatoire pour rendre le bot moins prévisible
            if (new Random().Next(0, 100) < 5)
            {
                newPosition += new Random().Next(-1, 2) * speed;
            }

            return newPosition;
        }
    }
}
