using Modele.SkinPackage;
using MonoGame.Extended.Sprites;
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

        public Paddle(float x, float y, Skin skin, Sprite sprite)
            : base(x, y, skin, sprite)
        {
        }

        public void Move(float deltaY)
        {
            y = deltaY;
        }
    }
}
