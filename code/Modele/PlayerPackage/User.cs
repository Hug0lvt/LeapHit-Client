using Modele.EntityPackage;
using Modele.MovementPackage;
using Modele.SkinPackage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modele.PlayerPackage
{
    public class User : Player
    {
        private string pseudo;

        public User(GameEntity paddle, GameEntity ball, IMovement movement, string pseudo) : 
            base(paddle, ball)
        {
            this.pseudo = pseudo;
            strategyMouvement = movement;
        }
    }
}
