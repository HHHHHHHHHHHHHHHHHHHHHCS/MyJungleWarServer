using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using MyJungleWarServer.Tool;

namespace MyJungleWarServer.Servers
{
    public class Client
    {
        private Socket clientSocket;
        private Server server;
        private Message msg;

        public MySqlConnection SQLConn { get; private set; }

        public Client()
        {

        }

        public Client(Socket _clientSocket, Server _server, MySqlConnection _conn)
        {
            clientSocket = _clientSocket;
            server = _server;
            msg = new Message();
            SQLConn = _conn;
        }

        public void Start()
        {
            clientSocket.BeginReceive(msg.Data, msg.StartIndex, msg.RemainIndex, SocketFlags.None, ReceiveCallback, null);
        }

        private void ReceiveCallback(IAsyncResult ar)
        {
            try
            {
                int count = clientSocket.EndReceive(ar);
                if (count <= 0)
                {
                    Close();
                }
                else
                {
                    //处理接收到的数据
                    msg.GetOneContent(count, OnProcessMessage);
                    Start();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Close();
            }
        }

        private void OnProcessMessage(RequestCode requestCode, ActionCode actionCode, string data)
        {
            server.HandleRequest(requestCode, actionCode, data, this);
        }

        public void Send(ActionCode actionCode,string data)
        {
            byte[] bytes = Message.PackData(actionCode, data);
            clientSocket.Send(bytes);
        }

        public void Close()
        {
            ConnHelper.Close(SQLConn);
            if (clientSocket != null)
            {
                clientSocket.Close();
            }
            server.RemoveClient(this);
        }
    }
}
