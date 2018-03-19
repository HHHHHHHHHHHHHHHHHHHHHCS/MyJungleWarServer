using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MyJungleWarServer.Servers
{
    public class Client
    {
        private Socket clientSocket;
        private Server server;
        private Message msg = new Message();

        public Client()
        {

        }

        public Client(Socket _clientSocket, Server _server)
        {
            clientSocket = _clientSocket;
            server = _server;
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
                    msg.GetOneContent(count);
                    Start();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Close();
            }
        }

        public void Close()
        {
            if (clientSocket != null)
            {
                clientSocket.Close();
            }
        }
    }
}
