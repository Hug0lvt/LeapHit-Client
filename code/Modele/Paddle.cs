using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modele
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
            this.posY += deltaY;
        }
    }
}
