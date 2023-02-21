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
        public Bot(GameEntity paddle, GameEntity ball) : 
            base(paddle, ball)
        {
            strategyMouvement = new Aleatoire();
        }
    }
}
