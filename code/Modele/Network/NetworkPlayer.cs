using Modele.PlayerPackage;
using ServerCommunication.Server;
using Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Player = Modele.PlayerPackage.Player;

namespace Modele.Network
{
    public static class NetworkPlayer
    {
        public static void SendPlayer(ClientSocket clientSocket, Shared.DTO.UserPlayer player) 
        { 
            clientSocket.Send(new ObjectTransfert<Shared.DTO.UserPlayer>(new Informations(Shared.DTO.Action.SendPlayer,0,typeof(Shared.DTO.UserPlayer).ToString()), player));
        }

        public static Shared.DTO.UserPlayer ReceivePlayer(ClientSocket clientSocket) 
        {
            Shared.DTO.UserPlayer player = clientSocket.Receive<Shared.DTO.UserPlayer>().Data;
            while(player is null)
            {
                player = clientSocket.Receive<Shared.DTO.UserPlayer>().Data;
            }
            return player;
        }
    }
}
