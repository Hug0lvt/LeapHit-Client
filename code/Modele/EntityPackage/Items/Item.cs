using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Modele.SkinPackage;
using MonoGame.Extended.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Modele.EntityPackage.Items
{
    public abstract class Item : GameEntity
    {
        private static Skin _skin = new Skin("Icon/returnIco", "speed");
        private static Random _random = new Random();
        public Item(int screenWidth, ContentManager contentManager) :
            base(_random.Next(screenWidth / 3, screenWidth - screenWidth / 3), 0, _skin, new Sprite(contentManager.Load<Texture2D>(_skin.Asset)))
        {

        }

        public abstract override void Move(float delta, int screenHeight, int screenWidth);

        public abstract void BallHitItem(Ball ball);


    }
}
