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
        private float angle;

        public Ball(float x, float y, Vector2 velocity, Skin skin, float angle)
            : base(x, y, velocity, skin)
        {
            this.angle = angle;
        }

        // TODO: add methods for updating ball position and angle based on velocity and collisions with other objects
    }
}
//Vector2 est une classe de la bibliothèque standard de C# qui représente un vecteur en deux dimensions, c'est-à-dire un couple de valeurs numériques qui indiquent une direction et une magnitude (longueur). Les composantes de Vector2 sont typiquement des nombres à virgule flottante, représentant les valeurs x et y du vecteur.