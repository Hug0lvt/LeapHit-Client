using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modele.Game;

namespace Modele.Object
{
    public class Paddle : GameObject
    {
        private float posY;
        private PaddleSkin skin;

        public Paddle(float posY, PaddleSkin skin)
        {
            this.posY = posY;
            this.skin = skin;
        }

        public void Move(float deltaY)
        {
            posY += deltaY;
        }
    }
}
