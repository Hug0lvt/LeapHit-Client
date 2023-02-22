using Modele.SkinPackage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Modele.EntityPackage
{
    public class Ball : GameEntity
    {
        protected Vector2 velocity;

        public Ball(float x, float y, Skin skin)
            : base(x, y, skin)
        {
            this.velocity = new Vector2(200, 250);
        }

        public Vector2 Velocity { get { return velocity; } }

        public void Move(float delta)
        {
            x += Velocity.X * delta;
            y += Velocity.Y * delta;
        }
    }
}
//Vector2 est une classe de la bibliothèque standard de C# qui représente un vecteur en deux dimensions, c'est-à-dire un couple de valeurs numériques qui indiquent une direction et une magnitude (longueur). Les composantes de Vector2 sont typiquement des nombres à virgule flottante, représentant les valeurs x et y du vecteur.