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
        private ClientSocket clientSocket;
        public ClientSocket Socket { get { return clientSocket; } }
        private long frame = 0;

        private Thread thread;

        public GameOnline(Player localPlayer, Player externalPlayer, GameStat gameStat, Ball ball, int screenWidth, int screenHeight, ContentManager contentManager, ClientSocket socket) 
            : base(localPlayer, externalPlayer, gameStat, ball, screenWidth, screenHeight, contentManager)
        {
            this.localPlayer = localPlayer;
            this.externalPlayer = externalPlayer;
            this.ball = ball;
            clientSocket = socket;

            thread = new Thread(() => ExchangeData(screenWidth, screenHeight));
            thread.Start();
        }

        public void ExchangeData(int screenWidth, int screenHeight)
        {
            //while (!isFinish)
            while(true)
            {
                Debug.WriteLine("thread");
                // Send Data
                var data = new Tuple<GameEntities, Tuple<int, int>>(new GameEntities(
                                                                        new Tuple<float, float>(
                                                                            ball.X,
                                                                            ball.Y
                                                                        ),
                                                                        localPlayer.Paddle.Y
                                                                    ), 
                                                                    GameStat.Score.GetScore()
                                                                );

                NetworkGameEntities.Send(clientSocket,
                                        data,
                                        frame
                                    );

                // Receive Data
                ObjectTransfert<Tuple<GameEntities, Tuple<int, int>>> tmp = NetworkGameEntities.Receive(clientSocket);
                Tuple<GameEntities, Tuple<int, int>> datas = tmp.Data;



                if (tmp.Informations.Action == Shared.DTO.Action.End)
                {
                    isFinish = true;
                    return;
                }


                float playerReceive = datas.Item1.Paddle;

                if (!clientSocket._isHost)
                {
                    Tuple<float, float> ballReceive = datas.Item1.Ball;
                    // Set coordonate
                    ball.X = screenWidth - ballReceive.Item1;
                    ball.Y = ballReceive.Item2;

                    Tuple<int, int> score = new Tuple<int, int>(datas.Item2.Item2, datas.Item2.Item1);

                    GameStat.Score.SetScore(score);
                }

                // Move
                externalPlayer.Paddle.Move(playerReceive, screenHeight, screenWidth);

                frame++;
            }
        }

        public void Finish()
        {
            Debug.WriteLine("fin partie");

            isFinish = true;
        }

        public override void Play(int screenWidth, int screenHeight, float elapsedSecond)
        {

            // Move
            localPlayer.Paddle.Move(localPlayer.StrategieMovement.GetMovement(), screenHeight, screenWidth);


            if (clientSocket._isHost)
            {
                ball.Move(elapsedSecond, screenHeight, screenWidth);
                localPlayer.Paddle.BallHitPaddle(ball);
                externalPlayer.Paddle.BallHitPaddle(ball);
                SetScore(ball, screenWidth, screenHeight, elapsedSecond);
            }
        }

        protected override void SetScore(Ball ball, int screenWidth, int screenHeight, float elapsedSecond)
        {

            var halfWidth = ball.Zone.Width / 2;

            var right = screenWidth - halfWidth;
            var left = halfWidth;

            if (!clientSocket._isHost)
            {
                right -= 10;
                left += 10;
            }

            if (ball.X > right && ball.Velocity.X > 0)
            {
                GameStat.Score.IncrementScore(localPlayer);
                ball.Reset(screenHeight, screenWidth, true);
            }

            if (ball.X < left && ball.Velocity.X < 0)
            {
                GameStat.Score.IncrementScore(externalPlayer);
                ball.Reset(screenHeight, screenWidth, false);
            }
        }
    }
}
