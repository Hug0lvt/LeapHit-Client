using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Modele.EntityPackage;
using Modele.MovementPackage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modele.PlayerPackage
{
    public class Bot : Player
    {
        public Bot(GameEntity paddle, GameEntity ball, int difficulty)
        {
            this.paddle = paddle;
            this.ball = ball;

            switch (difficulty)
            {
                default:
                    strategyMouvement = new Facile(ball, paddle);
                    break;
            }

            switch (difficulty)
            {
                case 1:
                    strategyMouvement = new Facile(ball, paddle);
                    break;
                case 2:
                    strategyMouvement = new Facile(ball, paddle);
                    break;
                case 3:
                    strategyMouvement = new Facile(ball, paddle);
                    break;
                default:
                    break;
            }

        }
    }
}
