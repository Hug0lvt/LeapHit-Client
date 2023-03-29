using Microsoft.Xna.Framework.Content;
using Modele.EntityPackage;
using Modele.Network;
using Modele.PlayerPackage;
using ServerCommunication.Server;
using Shared.DTO;
using System.Diagnostics;
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
        private float _elapsedtime = 0;

        private Thread thread;
        private Thread threadBall;

        public GameOnline(Player localPlayer, Player externalPlayer, GameStat gameStat, Ball ball, int screenWidth, int screenHeight, ContentManager contentManager, ClientSocket socket) 
            : base(localPlayer, externalPlayer, gameStat, ball, screenWidth, screenHeight, contentManager)
        {
            this.localPlayer = localPlayer;
            this.externalPlayer = externalPlayer;
            this.ball = ball;
            clientSocket = socket;

            thread = new Thread(() => ExchangeData(screenWidth, screenHeight));
            if (clientSocket._isHost)
            {
                threadBall = new Thread(() => MoveBall(screenWidth, screenHeight));
            }

            thread.Start();
        }

        public void ExchangeData(int screenWidth, int screenHeight)
        {
            while (true)
            {
                // Send Data
                var data = new GameEntities(
                                            new Tuple<float, float>(
                                                screenWidth - ball.X,
                                                ball.Y
                                            ),
                                            localPlayer.Paddle.Y
                                        );

                NetworkGameEntities.Send(clientSocket,
                                        data,
                                        frame
                                    );

                // Receive Data
                GameEntities datas = NetworkGameEntities.Receive(clientSocket);
                float playerReceive = datas.Paddle;

                if (!clientSocket._isHost)
                {
                    Tuple<float, float> ballReceive = datas.Ball;
                    // Set coordonate
                    ball.X = ballReceive.Item1;
                    ball.Y = ballReceive.Item2;
                }
                
                // Move
                externalPlayer.Paddle.Move(playerReceive, screenHeight, screenWidth);

                frame++;
            }
        }

        public void MoveBall(int screenWidth, int screenHeight)
        {
            ball.Move(_elapsedtime, screenHeight, screenWidth);
            localPlayer.Paddle.BallHitPaddle(ball);
            externalPlayer.Paddle.BallHitPaddle(ball);
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


            /*// Send Data
            var data = new GameEntities(
                                        new Tuple<float, float>(
                                            screenWidth - ball.X,
                                            ball.Y
                                        ),
                                        localPlayer.Paddle.Y
                                    );
            Debug.WriteLine("envoie : " + data.Paddle );

            NetworkGameEntities.Send(clientSocket,
                                    data,
                                    frame
                                );

            // Receive Data
            GameEntities datas = NetworkGameEntities.Receive(clientSocket);
            float playerReceive = datas.Paddle;
            Debug.WriteLine( "reçu : " + playerReceive );
            Tuple<float, float> ballReceive = datas.Ball;

            // Set coordonate
            /*ball.X = ballReceive.Item1;
            ball.Y = ballReceive.Item2;*/

            // Move*/
            localPlayer.Paddle.Move(localPlayer.StrategieMovement.GetMovement(), screenHeight, screenWidth);
            //externalPlayer.Paddle.Move(playerReceive, screenHeight, screenWidth);

            //ball.Move(elapsedSecond, screenHeight, screenWidth);
            

            _elapsedtime = elapsedSecond;

            SetScore(ball, screenWidth, screenHeight, elapsedSecond);


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
                if(clientSocket._isHost)
                {
                    ball.Reset(screenHeight, screenWidth, true);
                }
            }

            if (ball.X < -halfWidth && ball.Velocity.X < 0)
            {
                gameStat.Score.IncrementScore(externalPlayer);
                if (clientSocket._isHost)
                {
                    ball.Reset(screenHeight, screenWidth, false);
                }
            }
        }
    }
}
