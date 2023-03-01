using Microsoft.Xna.Framework;
using Modele.EntityPackage;
using Modele.MovementPackage;
using Modele.PlayerPackage;
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

        public Player LocalPlayer { get { return localPlayer; } }
        public Player ExternalPlayer { get { return externalPlayer; } }
        public GameStat GameStat { get { return gameStat; } }

        public Game(Player localPlayer, Player externalPlayer, GameStat gameStat)
        {
            this.localPlayer = localPlayer;
            this.externalPlayer = externalPlayer;
            this.gameStat = gameStat;

            this.gameStat.Score = new Score(this.localPlayer, this.externalPlayer);
        }

        public void Play(int screenWidth, int screenHeight, float elapsedSecond)
        {
            localPlayer.Paddle.Move(localPlayer.StrategieMovement.GetMovement(), screenHeight, screenWidth);
            externalPlayer.Paddle.Move(externalPlayer.StrategieMovement.GetMovement(), screenHeight, screenWidth);

            SetScore(localPlayer.Ball, screenWidth, screenHeight, elapsedSecond);

            localPlayer.Paddle.BallHitPaddle(localPlayer.Ball);
            externalPlayer.Paddle.BallHitPaddle(localPlayer.Ball);

        }

        private void SetScore(Ball ball, int screenWidth, int screenHeight, float elapsedSecond)
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
