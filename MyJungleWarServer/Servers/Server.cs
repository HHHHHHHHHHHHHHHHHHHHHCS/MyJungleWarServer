using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using MyJungleWarServer.Controller;
using Common;
using MyJungleWarServer.Tool;
using MySql.Data.MySqlClient;

namespace MyJungleWarServer.Servers
{
    public class Server
    {
        private IPEndPoint ipEndPoint;
        private Socket serverSocket;
        private HashSet<Client> clientHashSet = new HashSet<Client>();
        private ControllerManager controllerManager;
        private MySqlConnection sqlConn;

        public Server()
        {
            
        }

        public Server(string ipStr, int port)
        {
            controllerManager = new ControllerManager(this);
            SetIPAndPort(ipStr, port);
        }

        public void SetIPAndPort(string ipStr, int port)
        {
            ipEndPoint = new IPEndPoint(IPAddress.Parse(ipStr), port);
        }

        public void Start()
        {
            sqlConn= ConnHelper.Connect(); 
            serverSocket = new Socket(AddressFamily.InterNetwork
                , SocketType.Stream, ProtocolType.Tcp);
            serverSocket.Bind(ipEndPoint);
            serverSocket.Listen(0);
            serverSocket.BeginAccept(AcceptCallBack, null);
        }

        private void AcceptCallBack(IAsyncResult ar)
        {
            Socket clientSocket = serverSocket.EndAccept(ar);
            Client client = new Client(clientSocket, this, sqlConn);
            client.Start();
            clientHashSet.Add(client);
            serverSocket.BeginAccept(AcceptCallBack, null);
        }

        public void RemoveClient(Client client)
        {
            lock (clientHashSet)
            {
                clientHashSet.Remove(client);
            }
        }

        public void SendRespone(Client client, ActionCode actionCode, string data)
        {
            client.Send(actionCode, data);
        }

        public void HandleRequest(RequestCode requestCode, ActionCode actionCode
            , string data, Client client)
        {
            controllerManager.HandleRequest(requestCode, actionCode, data, client);
        }

        public void Close()
        {
            sqlConn.Close();
            serverSocket.Close();
        }
    }
}
