using Modele.PlayerPackage;
using ServerCommunication.Server;
using Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modele.Network
{
    public static class NetworkPlayer
    {
        public static void Send(ClientSocket clientSocket, Player player) 
        { 
            clientSocket.Send<Player>(new ObjectTransfert<Player>(new Informations(Shared.DTO.Action.SendPlayer,0,typeof(Player).ToString()),player));
        }

        public static Player Receive(ClientSocket clientSocket) 
        {
            return clientSocket.Receive<Player>().Data;
        }
    }
}
