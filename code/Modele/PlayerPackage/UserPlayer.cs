using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Modele.EntityPackage;
using Modele.MovementPackage;
using Modele.MovementPackage.MotionSensorPackage;
using Modele.SkinPackage;
using MonoGame.Extended.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modele.PlayerPackage
{
    public class UserPlayer : Player
    {
        public UserPlayer(User profile, int startPosition, Game game, MotionSensor strategie)
        {
            var _widthCenter = game.GraphicsDevice.Viewport.Width / 2;
            var _heightCenter = game.GraphicsDevice.Viewport.Height / 2;

            paddle = new Paddle(startPosition, _heightCenter, profile.SelectedPaddle, new Sprite(game.Content.Load<Texture2D>(profile.SelectedPaddle.Asset)));
            ball = new Ball(_widthCenter, _heightCenter, profile.SelectedBall, new Sprite(game.Content.Load<Texture2D>(profile.SelectedBall.Asset)));
            strategyMouvement = strategie;
        }
    }
}
