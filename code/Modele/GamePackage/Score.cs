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
        private Tuple<Player, double> player1;
        private Tuple<Player, double> player2;

        public Score(Player player1, Player player2)
        {
            this.player1 = new Tuple<Player, double>(player1, 0);
            this.player2 = new Tuple<Player, double>(player2, 0);
        }

        public Player GetWinner()
        {
            if(player1.Item2 > player2.Item2)
            {
                return player1.Item1;
            }

            return player2.Item1;
        }

        public Tuple<int, int> GetScore()
        {
            return new Tuple<int, int>((int)player1.Item2, (int)player2.Item2);
        }

        public void IncrementScore(Player player)
        {
            if (player.Equals(player1.Item1)) player1 = new Tuple<Player, double>(player1.Item1, player1.Item2+0.25);
            else player2 = new Tuple<Player, double>(player2.Item1, player2.Item2+0.25);
        }
    }
}
