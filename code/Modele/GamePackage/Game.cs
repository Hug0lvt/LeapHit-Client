using Leap;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Modele.EntityPackage;
using Modele.EntityPackage.Items;
using Modele.MovementPackage;
using Modele.PlayerPackage;
using MonoGame.Extended;
using MonoGame.Extended.Timers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modele.GamePackage
{
    public class Game
    {
        private bool pause;
        private Player localPlayer;
        private Player externalPlayer;
        private GameStat gameStat;
        private WebSocket webSocket;
        //Item
        public Item _item;
        private int _screenWidth;
        private int _screenHeight;
        private ContentManager _contentManager;
        private float timeItemGnerate;


        private float _time = 0;




        public Player LocalPlayer { get { return localPlayer; } }
        public Player ExternalPlayer { get { return externalPlayer; } }
        public GameStat GameStat { get { return gameStat; } }

        public Game(Player localPlayer, Player externalPlayer, GameStat gameStat, int screenWidth, int screenHeight,ContentManager contentManager)
        {
            this.localPlayer = localPlayer;
            this.externalPlayer = externalPlayer;
            this.gameStat = gameStat;
            _screenWidth = screenWidth;
            _screenHeight = screenHeight;
            _contentManager = contentManager;

            this.gameStat.Score = new Score(this.localPlayer, this.externalPlayer);
           
            

        }

        public virtual void Play(int screenWidth, int screenHeight, float elapsedSecond)
        {
            _time += elapsedSecond;
            if (_item == null)
            {
                Random rand = new Random();
                timeItemGnerate = 2/*_time + 2 + (float)(rand.NextDouble() * (120 - _time + 2))*/;
                if (_time >= timeItemGnerate)
                {
                    _item = new SnipeItem(_screenWidth, _contentManager);
                }
            }
            
               
            localPlayer.Paddle.Move(localPlayer.StrategieMovement.GetMovement(), screenHeight, screenWidth);
            externalPlayer.Paddle.Move(externalPlayer.StrategieMovement.GetMovement(), screenHeight, screenWidth);

            SetScore(localPlayer.Ball, screenWidth, screenHeight, elapsedSecond);

            localPlayer.Paddle.BallHitPaddle(localPlayer.Ball);
            externalPlayer.Paddle.BallHitPaddle(localPlayer.Ball);
            try
            {

                _item?.Move(elapsedSecond, screenWidth, screenHeight);
                _item?.BallHitItem(LocalPlayer.Ball);
            }
            catch (ExceptionItemDelete)
            {
                _item = null;
            }

        }

        protected virtual void SetScore(Ball ball, int screenWidth, int screenHeight, float elapsedSecond)
        {
            ball.Move(elapsedSecond, screenHeight, screenWidth);

            var halfWidth = ball.Zone.Width / 2;

            if (ball.X > screenWidth + halfWidth && ball.Velocity.X > 0)
            {
                gameStat.Score.IncrementScore(localPlayer);
                ball.Reset(screenHeight, screenWidth, true);
            }

            if (ball.X < -halfWidth && ball.Velocity.X < 0)
            {
                gameStat.Score.IncrementScore(externalPlayer);
                ball.Reset(screenHeight, screenWidth, false);
            }
        }
    }
}
