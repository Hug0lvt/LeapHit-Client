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
        public static void Send(ClientSocket clientSocket, GameEntities entities, long frame)
        {
            ObjectTransfert<GameEntities> obj = new ObjectTransfert<GameEntities>()
            {
                Informations = new Informations(Shared.DTO.Action.SendEntities, frame, typeof(GameEntities).ToString()),
                Data = entities
            };
            clientSocket.Send<GameEntities>(obj);
        }

        public static GameEntities Receive(ClientSocket clientSocket)
        {
            var entities = clientSocket.Receive<GameEntities>();
            while (entities == null)
            {
                entities = clientSocket.Receive<GameEntities>();
            }

            return entities.Data;
        }


    }
}
