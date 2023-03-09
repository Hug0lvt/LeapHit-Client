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
                case 1:
                    strategyMouvement = new Aleatoire(ball, paddle, 2, 1);
                    break;
                case 2:
                    strategyMouvement = new Aleatoire(ball, paddle, 2, 3);
                    break;
                case 3:
                    strategyMouvement = new Aleatoire(ball, paddle, 2, 7);
                    break;
                default:
                    break;
            }
        }
    }
}
