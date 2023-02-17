using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modele
{
    //ici la velocity correspond à la vitesse de la balle
    public class CapacityBall
    {
        private float size;
        private float velocity;

        public CapacityBall(float size, float velocity)
        {
            this.size = size;
            this.velocity = velocity;
        }

        public void ChangeSize(float newSize)
        {
            this.size = newSize;
        }

        public void ChangeVelocity(float newVelocity)
        {
            this.velocity = newVelocity;
        }
    }
}
