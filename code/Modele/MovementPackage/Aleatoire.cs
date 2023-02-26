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
    public abstract class Aleatoire : IMovement
    {
        protected Ball ball;
        protected GameEntity paddle;
        protected float elapsedSeconds;

        public float ElapsedSeconds { set { this.elapsedSeconds = value; } }

        public Aleatoire(GameEntity ball, GameEntity paddle)
        {
            this.ball = (Ball)ball;
            this.paddle = paddle;
        }

        public abstract float GetAleatoireMovement();

        public float GetMovement()
        {
            return GetAleatoireMovement();
        }
        public void startMovement()
        {
            return;
        }
    }
}
