using Microsoft.Xna.Framework.Content;
using Modele.EntityPackage;
using Modele.PlayerPackage;
using ServerCommunication.Server;

namespace Modele.GamePackage
{
    public class GameOnline : Game
    {
        public GameOnline(Player localPlayer, Player externalPlayer, GameStat gameStat, int screenWidth, int screenHeight, ContentManager contentManager, ClientSocket socket) 
            : base(localPlayer, externalPlayer, gameStat, screenWidth, screenHeight, contentManager)
        {
        }

        public override void Play(int screenWidth, int screenHeight, float elapsedSecond)
        {
            //_time += elapsedSecond;
            //if (_item == null)
            //{
            //    Random rand = new Random();
            //    timeItemGnerate = 2/*_time + 2 + (float)(rand.NextDouble() * (120 - _time + 2))*/;
            //    if (_time >= timeItemGnerate)
            //    {
            //        _item = new SnipeItem(_screenWidth, _contentManager);
            //    }
            //}


            //localPlayer.Paddle.Move(localPlayer.StrategieMovement.GetMovement(), screenHeight, screenWidth);
            //externalPlayer.Paddle.Move(externalPlayer.StrategieMovement.GetMovement(), screenHeight, screenWidth);

            //SetScore(localPlayer.Ball, screenWidth, screenHeight, elapsedSecond);

            //localPlayer.Paddle.BallHitPaddle(localPlayer.Ball);
            //externalPlayer.Paddle.BallHitPaddle(localPlayer.Ball);
            //try
            //{

            //    _item?.Move(elapsedSecond, screenWidth, screenHeight);
            //    _item?.BallHitItem(LocalPlayer.Ball);
            //}
            //catch (ExceptionItemDelete)
            //{
            //    _item = null;
            //}

        }

        private override void SetScore(Ball ball, int screenWidth, int screenHeight, float elapsedSecond)
        {

            //ball.Move(elapsedSecond, screenHeight, screenWidth);

            //var halfWidth = ball.Zone.Width / 2;

            //if (ball.X > screenWidth + halfWidth && ball.Velocity.X > 0)
            //{
            //    gameStat.Score.IncrementScore(localPlayer);
            //    ball.Reset(screenHeight, screenWidth, true);
            //}

            //if (ball.X < -halfWidth && ball.Velocity.X < 0)
            //{
            //    gameStat.Score.IncrementScore(externalPlayer);
            //    ball.Reset(screenHeight, screenWidth, false);
            //}
        }
    }
}
