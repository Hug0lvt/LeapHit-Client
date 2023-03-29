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
        private float elapsedtime = 0;

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
            while (true)
            {
                // Send Data
                var data = new GameEntities(
                                            new Tuple<float, float>(
                                                ball.X,
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
                    ball.X = screenWidth - ballReceive.Item1;
                    ball.Y = ballReceive.Item2;

                    Tuple<int, int> score = clientSocket.Receive<Tuple<int, int>>().Data;
                    GameStat.Score.SetScore(score);
                } 
                else
                {
                    clientSocket.Send(
                        new ObjectTransfert<Tuple<int, int>>() { 
                            Data = GameStat.Score.GetScore(), 
                            Informations = new Informations(
                                Shared.DTO.Action.SendScore, 
                                frame, 
                                typeof(Tuple<int, int>).ToString()
                            ) 
                        }
                    );
                }

                // Move
                externalPlayer.Paddle.Move(playerReceive, screenHeight, screenWidth);

                frame++;
            }
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

            // Move
            localPlayer.Paddle.Move(localPlayer.StrategieMovement.GetMovement(), screenHeight, screenWidth);


            if (clientSocket._isHost)
            {
                ball.Move(elapsedSecond, screenHeight, screenWidth);
                localPlayer.Paddle.BallHitPaddle(ball);
                externalPlayer.Paddle.BallHitPaddle(ball);
                SetScore(ball, screenWidth, screenHeight, elapsedSecond);
            }


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
