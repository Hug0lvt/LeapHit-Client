using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modele.EntityPackage;
using Modele.MovementPackage;
using Modele.SkinPackage;

namespace Modele.PlayerPackage
{
    public abstract class Player
    {
        private GameEntity paddle;
        public Paddle Paddle => (Paddle)paddle;
        private GameEntity ball;
        public Ball Ball => (Ball)ball;
        protected IMovement strategyMouvement;
        public IMovement StrategieMovement => strategyMouvement;

        public Player(GameEntity paddle, GameEntity ball)
        {
            this.paddle = paddle;
            this.ball = ball;
        }
    }
}
