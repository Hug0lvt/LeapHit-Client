using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Shared.DTO;


namespace ServerCommunication.Server
{
    public class ClientSocket
    {
        public IPEndPoint _serverEndPoint { get; set; }
        public bool _stateConnexion { get; set; }
        public UdpClient _client { get; set; }

        public ClientSocket(string host, int port)
        {
            //IPEndPoint _serverEndPoint = new IPEndPoint(Dns.GetHostAddresses(host).FirstOrDefault(), port);

            _serverEndPoint = new IPEndPoint(IPAddress.Parse("192.168.43.36"), 3131);
            _client = new UdpClient();
        }

        public void Connect(Player player)
        {
            ObjectTransfert<Player> obj = new ObjectTransfert<Player>()
            {
                Informations = new Informations(Shared.DTO.Action.Connect, 0, typeof(Player).ToString()),
                Data = player
            };
            _client.Send(Encoding.ASCII.GetBytes(JsonSerializer.Serialize(obj)), _serverEndPoint);

            IPEndPoint remoteEndPoint = new IPEndPoint(_serverEndPoint.Address, 0);
            _serverEndPoint.Port = Int32.Parse(Encoding.ASCII.GetString(_client.Receive(ref remoteEndPoint)));
            _stateConnexion = true;
        }

        public void Send<T>(ObjectTransfert<T> datas)
        {
            if (_stateConnexion == false) throw new SocketException();
            Debug.WriteLine(datas.Data);
            var json = JsonSerializer.Serialize<ObjectTransfert<T>>(datas);
            _client.Send(Encoding.ASCII.GetBytes(json), _serverEndPoint);
        }

        public ObjectTransfert<T> Receive<T>() 
        {
            if (_stateConnexion == false) throw new SocketException();
            IPEndPoint remoteEndPoint = _serverEndPoint;
            return JsonSerializer.Deserialize<ObjectTransfert<T>>(_client.Receive(ref remoteEndPoint));
        }

        public void Disconnect()
        {
            string connectionMessage = Shared.DTO.Action.End.ToString();
            _client.Send(Encoding.ASCII.GetBytes(connectionMessage), _serverEndPoint);
            _client.Close();
            _stateConnexion = false;
        }

    }
}
