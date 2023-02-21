using Modele.SkinPackage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Modele.EntityPackage
{
    public class Paddle : GameEntity
    {

        public Paddle(float x, float y, Vector2 velocity, Skin skin)
            : base(x, y, velocity, skin)
        {
        }

        public void Move(float deltaY)
        {
            y = deltaY;
        }
    }
}
