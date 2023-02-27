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
    public abstract class Aleatoire : IMovement
    {
        protected Ball ball;
        protected GameEntity paddle;
        protected float elapsedSeconds;
        protected Vector2 velocity;

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
    }
}
