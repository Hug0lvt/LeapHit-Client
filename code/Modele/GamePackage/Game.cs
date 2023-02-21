using Modele.PlayerPackage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modele.GamePackage
{
    class Game
    {
        private bool pause;
        private List<Player> players;
        private GameStat gameStat;
        private WebSocket webSocket;
    }
}
