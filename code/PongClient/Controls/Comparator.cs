using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongClient.Controls
{
    public abstract class Comparator
    {
        public abstract bool Compare(float x, float y);
        public abstract Comparator Change();
    }

    public class Superior : Comparator
    {
        public override bool Compare(float x, float y)
        {
            return x > y;
        }

        public override Comparator Change()
        {
            return new Inferior();
        }
    }

    public class Inferior : Comparator
    {
        public override bool Compare(float x, float y)
        {
            return x < y;
        }

        public override Comparator Change()
        {
            return new Superior();
        }
    }
}
