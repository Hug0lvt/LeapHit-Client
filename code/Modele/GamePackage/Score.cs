using Modele.PlayerPackage;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modele.GamePackage
{
    public class Score
    {
        private Tuple<Player, int> player1;
        private Tuple<Player, int> player2;

        public Score(Player player1, Player player2)
        {
            this.player1 = new Tuple<Player, int>(player1, 0);
            this.player2 = new Tuple<Player, int>(player2, 0);
        }

        public Player GetWinner()
        {
            if(player1.Item2 > player2.Item2)
                return player1.Item1;
            else
            {
                if (player1.Item2 < player2.Item2)
                    return player2.Item1;
                else
                    return null;
            }
        }

        public Tuple<int, int> GetScore()
        {
            return new Tuple<int, int>(player1.Item2, player2.Item2);
        }

        public bool IsWin(int max)
        {
            if (GetScore().Item1 >= max || GetScore().Item2 >= max) return true;
            return false;
        }

        public void SetScore(Tuple<int, int> score)
        {
            player1 = new Tuple<Player, int>(player1.Item1, score.Item1);
            player2 = new Tuple<Player, int>(player2.Item1, score.Item2);
        }

        public void IncrementScore(Player player)
        {
            if (player.Equals(player1.Item1)) player1 = new Tuple<Player, int>(player1.Item1, player1.Item2+1);
            else player2 = new Tuple<Player, int>(player2.Item1, player2.Item2+1);
        }
    }
}
