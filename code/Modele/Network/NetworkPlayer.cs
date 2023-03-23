using Modele.PlayerPackage;
using ServerCommunication.Server;
using Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserPlayer = Shared.DTO.UserPlayer;

namespace Modele.Network
{
    public static class NetworkPlayer
    {
        public static void SendPlayer(ClientSocket clientSocket, UserPlayer player) 
        { 
            clientSocket.Send<UserPlayer>(new ObjectTransfert<UserPlayer>(new Informations(Shared.DTO.Action.SendPlayer,0,typeof(UserPlayer).ToString()),player));
        }

        public static UserPlayer ReceivePlayer(ClientSocket clientSocket) 
        {
            UserPlayer player = clientSocket.Receive<UserPlayer>().Data;
            while(player is null)
            {
                player = clientSocket.Receive<UserPlayer>().Data;
            }
            return player;
        }
    }
}
