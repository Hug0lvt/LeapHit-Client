using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongClient.ModeleMonoGame
{
    public abstract class DivisionOrMultiple
    {
        public abstract float Calcul(float x, float y);

    }

    public  class Division : DivisionOrMultiple
    {
        override
        public float Calcul(float x, float y)
        {
            return x / y;
        }

    }

    public  class Multiple : DivisionOrMultiple
    {
        override
        public float Calcul(float x, float y)
        {
            return x * y;
        }

    }
}
