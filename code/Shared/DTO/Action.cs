using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO
{
    public enum Action
    {
        Start,
        Connect,
        End,


        SendPlayer,
        RecivePlayer,


        // Client
        Create,
        WaitClient,
        SendReady,
        SendLocationPaddle,
        Recieve,
        JoinRoom,
        WaitCLient1ToBeReady,
        WaitClient2ToBeReady,

        // Serveur
        RoomCreated,
        Wait,
        Ready,
        SendLocationPaddleAgainst,
        SendWinner,
    }
}
