using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Modele
{
    public class Ball : GameObject
    {
        private float angle;
        private CapacityBall capacity;
        private BallSkin skin;

        public Ball(float posX, float posY, Vector2 velocity, float angle, CapacityBall capacity, BallSkin skin)
        {
            this.posX = posX;
            this.posY = posY;
            this.velocity = velocity;
            this.angle = angle;
            this.capacity = capacity;
            this.skin = skin;
        }

        // TODO: add methods for updating ball position and angle based on velocity and collisions with other objects
    }
}
//Vector2 est une classe de la bibliothèque standard de C# qui représente un vecteur en deux dimensions, c'est-à-dire un couple de valeurs numériques qui indiquent une direction et une magnitude (longueur). Les composantes de Vector2 sont typiquement des nombres à virgule flottante, représentant les valeurs x et y du vecteur.