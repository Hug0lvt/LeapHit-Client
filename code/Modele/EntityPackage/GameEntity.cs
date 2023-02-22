using Modele.SkinPackage;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Modele.EntityPackage
{
    public abstract  class GameEntity
    {
        protected float x;
        protected float y;
        protected Skin skin;
        protected Rectangle zone;

        protected GameEntity(float x, float y, Skin skin)
        {
            this.x = x;
            this.y = y;
            this.skin = skin;
        }

        public float X { get { return x; } set { this.x = value; } }
        public float Y { get { return y; } set { this.y = value; } }
        public string Skin { get { return skin.Asset; } }
        public Rectangle Zone { get { return this.zone; } set { this.zone = value; } }
    }
}
