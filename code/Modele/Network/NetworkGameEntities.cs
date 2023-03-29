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
        public static void Send(ClientSocket clientSocket, Tuple<GameEntities, Tuple<int, int>> entities, long frame)
        {
            ObjectTransfert<Tuple<GameEntities, Tuple<int, int>>> obj = new ObjectTransfert<Tuple<GameEntities, Tuple<int, int>>>()
            {
                Informations = new Informations(Shared.DTO.Action.SendEntities, frame, typeof(Tuple<GameEntities, Tuple<int, int>>).ToString()),
                Data = entities
            };
            clientSocket.Send<Tuple<GameEntities, Tuple<int, int>>>(obj);
        }

        public static Tuple<GameEntities, Tuple<int, int>> Receive(ClientSocket clientSocket)
        {
            var entities = clientSocket.Receive<Tuple<GameEntities, Tuple<int, int>>>();
            while (entities == null)
            {
                entities = clientSocket.Receive<Tuple<GameEntities, Tuple<int, int>>>();
            }

            return entities.Data;
        }


    }
}
