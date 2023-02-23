using Modele.SkinPackage;
using MonoGame.Extended.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace Modele.EntityPackage
{
    public class Ball : GameEntity
    {
        protected Vector2 velocity;

        public Ball(float x, float y, Skin skin, Sprite sprite)
            : base(x, y, skin, sprite)
        {
            this.velocity = new Vector2(200, 250);
        }

        public Vector2 Velocity { get { return velocity; } set { velocity = value; } }

        public void Move(float delta)
        {
            x += Velocity.X * delta;
            y += Velocity.Y * delta;
        }
    }
}
//Vector2 est une classe de la bibliothèque standard de C# qui représente un vecteur en deux dimensions, c'est-à-dire un couple de valeurs numériques qui indiquent une direction et une magnitude (longueur). Les composantes de Vector2 sont typiquement des nombres à virgule flottante, représentant les valeurs x et y du vecteur.