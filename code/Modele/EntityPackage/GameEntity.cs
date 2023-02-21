using Modele.SkinPackage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Modele.EntityPackage
{
    public class GameEntity
    {
        protected float x;
        protected float y;
        protected Vector2 velocity;
        protected Skin skin;

        protected GameEntity(float x, float y, Vector2 velocity, Skin skin)
        {
            this.x = x;
            this.y = y;
            this.velocity = velocity;
            this.skin = skin;
        }

        public float X { get { return x; } }
        public float Y { get { return y; } }
        public string Skin { get { return skin.Asset; } }
    }
}
