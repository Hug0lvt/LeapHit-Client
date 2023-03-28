using Microsoft.Xna.Framework.Content;
using Modele.EntityPackage;
using Modele.Network;
using Modele.PlayerPackage;
using ServerCommunication.Server;
using Shared.DTO;
using Player = Modele.PlayerPackage.Player;

namespace Modele.GamePackage
{
    public class GameOnline : Game
    {
        private Player localPlayer;
        private Player externalPlayer;
        protected Ball ball;
        private GameStat gameStat;
        private ClientSocket clientSocket;
        public ClientSocket Socket { get { return clientSocket; } }
        private long frame = 0;

        public GameOnline(Player localPlayer, Player externalPlayer, GameStat gameStat, Ball ball, int screenWidth, int screenHeight, ContentManager contentManager, ClientSocket socket) 
            : base(localPlayer, externalPlayer, gameStat, ball, screenWidth, screenHeight, contentManager)
        {
            this.localPlayer = localPlayer;
            this.externalPlayer = externalPlayer;
            this.ball = ball;
            clientSocket = socket;
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

            // Send Data
            var data = new GameEntities(
                                        new Tuple<float, float>(
                                            screenWidth - ball.X,
                                            ball.Y
                                        ),
                                        screenWidth - localPlayer.Paddle.Y
                                    );

            NetworkGameEntities.Send(clientSocket,
                                    data,
                                    frame
                                );

            // Receive Data
            GameEntities datas = NetworkGameEntities.Receive(clientSocket);
            float playerReceive = datas.Paddle;
            Tuple<float, float> ballReceive = datas.Ball;

            // Set coordonate
            /*ball.X = ballReceive.Item1;
            ball.Y = ballReceive.Item2;*/

            // Move
            localPlayer.Paddle.Move(localPlayer.StrategieMovement.GetMovement(), screenHeight, screenWidth);
            externalPlayer.Paddle.Move(playerReceive, screenHeight, screenWidth);
                    
            //ball.Move(elapsedSecond, screenHeight, screenWidth);

            //SetScore(ball, screenWidth, screenHeight, elapsedSecond);

            

            frame++;
            //localPlayer.Paddle.BallHitPaddle(ball);
            //externalPlayer.Paddle.BallHitPaddle(ball);
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

        protected override void SetScore(Ball ball, int screenWidth, int screenHeight, float elapsedSecond)
        {

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
