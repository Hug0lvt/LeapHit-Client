using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Modele.EntityPackage;
using Modele.MovementPackage;
using Modele.SkinPackage;
using MonoGame.Extended.Sprites;

namespace Modele.PlayerPackage
{
    public abstract class Player
    {
        protected GameEntity paddle;
        protected GameEntity ball;
        protected IMovement strategyMouvement;
        protected bool ready;

        public Paddle Paddle => (Paddle)paddle;
        public Ball Ball => (Ball)ball;
        public IMovement StrategieMovement => strategyMouvement;
        public bool Ready { get; set; }
    }
}
