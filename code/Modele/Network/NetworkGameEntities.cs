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
    public static class NetworkGameEntities
    {
        public static void Send(ClientSocket clientSocket, GameEnities entities, long frame)
        {
            clientSocket.Send<GameEnities>(
                new ObjectTransfert<GameEnities>(
                    new Informations(
                        Shared.DTO.Action.SendEntities, 
                        frame, 
                        typeof(GameEnities).ToString()
                        ), 
                    entities
                    )
                );
        }

        public static GameEnities Receive(ClientSocket clientSocket)
        {
            GameEnities entities = clientSocket.Receive<GameEnities>().Data;
            while (entities is null)
            {
                entities = clientSocket.Receive<GameEnities>().Data;
            }
            return entities;
        }


    }
}
