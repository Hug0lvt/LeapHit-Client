using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modele.GamePackage
{
    public class GameStat
    {
        private TimeSpan time = TimeSpan.Zero;
        private Score score;

        public TimeSpan Time { get { return time; } set { time = value; } }
        public Score Score { get { return score; } set { score = value; } }
    }
}
