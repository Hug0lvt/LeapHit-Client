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

        public Aleatoire(GameEntity ball, GameEntity paddle)
        {
            this.ball = (Ball)ball;
            this.paddle = paddle;
        }

        public abstract float GetAleatoireMovement();
        public abstract void Init();

        public float GetMovement()
        {
            return GetAleatoireMovement();
        }

        public void Start()
        {
            return;
        }

        public void Stop()
        {
            return;
        }
    }
}
