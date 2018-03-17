﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using MyJungleWarServer.Controller;

namespace MyJungleWarServer.Server
{
    public class Server
    {
        private IPEndPoint ipEndPoint;
        private Socket serverSocket;
        private HashSet<Client> clientHashSet;
        private ControllerManager controllerManager = new ControllerManager();

        public Server()
        {

        }

        public Server(string ipStr, int port)
        {
            SetIPAndPort(ipStr, port);
        }

        public void SetIPAndPort(string ipStr, int port)
        {
            ipEndPoint = new IPEndPoint(IPAddress.Parse(ipStr), port);
        }

        public void Start()
        {
            serverSocket = new Socket(AddressFamily.InterNetwork
                , SocketType.Stream, ProtocolType.Tcp);
            serverSocket.Bind(ipEndPoint);
            serverSocket.Listen(0);
            serverSocket.BeginAccept(AcceptCallBack, null);
        }

        private void AcceptCallBack(IAsyncResult ar)
        {
            Socket clientSocket = serverSocket.EndAccept(ar);
            Client client = new Client(clientSocket, this);
            client.Start();
            clientHashSet.Add(client);
        }

        public void RemoveClient(Client client)
        {
            lock(clientHashSet)
            {
                clientHashSet.Remove(client);
            }

        }
    }
}
