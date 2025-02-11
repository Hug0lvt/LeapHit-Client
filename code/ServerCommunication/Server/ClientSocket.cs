﻿using System;
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
        public bool _isHost { get; set; }

        public ClientSocket()
        {
            _serverEndPoint = new IPEndPoint(Dns.GetHostAddresses("hulivet.fr").FirstOrDefault(), 3131);

            //_serverEndPoint = new IPEndPoint(IPAddress.Parse("192.168.135.58"), 3131);
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
            Tuple<int, bool> dataReceive = JsonSerializer.Deserialize<Tuple<int, bool>>(_client.Receive(ref remoteEndPoint));

            _serverEndPoint.Port = dataReceive.Item1;
            _isHost = dataReceive.Item2;
            _stateConnexion = true;
        }

        public void Host(Player player)
        {
            ObjectTransfert<Player> obj = new ObjectTransfert<Player>()
            {
                Informations = new Informations(Shared.DTO.Action.Host, 0, typeof(Player).ToString()),
                Data = player
            };
            _client.Send(Encoding.ASCII.GetBytes(JsonSerializer.Serialize(obj)), _serverEndPoint);

            IPEndPoint remoteEndPoint = new IPEndPoint(_serverEndPoint.Address, 0);
            Tuple<int, bool> dataReceive = JsonSerializer.Deserialize<Tuple<int, bool>>(_client.Receive(ref remoteEndPoint));

            _serverEndPoint.Port = dataReceive.Item1;
            _isHost = dataReceive.Item2;
            _stateConnexion = true;
        }

        public void Join(Player player, string idRoom)
        {
            ObjectTransfert<Player> obj = new ObjectTransfert<Player>()
            {
                Informations = new Informations(Shared.DTO.Action.Join, 0, typeof(Player).ToString(), idRoom),
                Data = player
            };
            _client.Send(Encoding.ASCII.GetBytes(JsonSerializer.Serialize(obj)), _serverEndPoint);

            IPEndPoint remoteEndPoint = new IPEndPoint(_serverEndPoint.Address, 0);
            Tuple<int, bool> dataReceive = JsonSerializer.Deserialize<Tuple<int, bool>>(_client.Receive(ref remoteEndPoint));

            _serverEndPoint.Port = dataReceive.Item1;
            _isHost = dataReceive.Item2;
            _stateConnexion = true;
        }

        public void Send<T>(ObjectTransfert<T> datas)
        {
            if (_stateConnexion == false) throw new SocketException();
            var json = JsonSerializer.Serialize<ObjectTransfert<T>>(datas);
            _client.Send(Encoding.ASCII.GetBytes(json), _serverEndPoint);
        }

        public ObjectTransfert<T>? Receive<T>() 
        {
            if (_stateConnexion == false) throw new SocketException();
            IPEndPoint remoteEndPoint = _serverEndPoint;
            ObjectTransfert<T> data = null;
            try
            {
                data = JsonSerializer.Deserialize<ObjectTransfert<T>>(_client.Receive(ref remoteEndPoint));
            } catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
            return data;
        }

        public void Disconnect()
        {
            //string connectionMessage = Shared.DTO.Action.End.ToString();
            //_client.Send(Encoding.ASCII.GetBytes(connectionMessage), _serverEndPoint);
            _client.Close();
            _stateConnexion = false;

        }

    }
}
