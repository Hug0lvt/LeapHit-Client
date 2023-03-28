using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongClient.Controls
{
    public class LoadBall
    {
        private Vector2 position;
        public Vector2 Position => position;
        private int movement;
        private float max;
        private Comparator cmp;
        private float start;

        public LoadBall(float max, Vector2 center, int movement) 
        { 
            position = center;
            this.start = center.X;
            if (movement > 0)
            {
                this.max = (int)center.X + max;
                cmp = new Superior();
            }
            else
            {
                this.max = (int)center.X - max;
                cmp = new Inferior();
            }
            
            this.movement = movement;
        }

        public void Move()
        {
            if(cmp.Compare(position.X + movement, max))
            {
                movement *= -1;
                (max, start) = (start, max);
                cmp = cmp.Change();
            }

            position.X += movement;
        }
    }   
}
