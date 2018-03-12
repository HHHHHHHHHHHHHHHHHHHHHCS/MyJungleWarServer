using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace MyJungleWarServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Program p = new Program();
            p.StartServerAsync();
        }

        private void StartServerSync()
        {
            Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //本机IP：192.168.0.120     电脑内部IP：127.0.0.1
            //IPAddress xxx.xxx.xxx.xxx IPEndPoint xxx.xxx.xxx.xxx:port
            //IPAddress ipAddress = new IPAddress(new byte[] { 127, 0, 0, 1 });//不推荐
            IPAddress ipAddress = IPAddress.Parse("192.168.0.120");
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddress, 6666);
            serverSocket.Bind(ipEndPoint);
            serverSocket.Listen(0);//开始监听端口号   设置为0 数量没有限制
            var clientSocket = serverSocket.Accept();//开始接受一个客户端链接  期间会阻塞线程

            //向客户端发送消息
            string msg = "Hello World!雷侯啊，啊油OK！";
            var data = System.Text.Encoding.UTF8.GetBytes(msg);
            clientSocket.Send(data);

            //接受客户端的消息
            byte[] dataBuffer = new byte[1024];
            int receiveDataLength = clientSocket.Receive(dataBuffer);
            string receiveMsg = System.Text.Encoding.UTF8.GetString(dataBuffer, 0, receiveDataLength);
            Console.WriteLine(receiveMsg);

            clientSocket.Close();
            serverSocket.Close();

            while (true)
            {
                Console.ReadLine();
            }
        }


        private byte[] asyncDataBuffer = new byte[1024];
        private void StartServerAsync()
        {
            Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //本机IP：192.168.0.120     电脑内部IP：127.0.0.1
            //IPAddress xxx.xxx.xxx.xxx IPEndPoint xxx.xxx.xxx.xxx:port
            //IPAddress ipAddress = new IPAddress(new byte[] { 127, 0, 0, 1 });//不推荐
            IPAddress ipAddress = IPAddress.Parse("192.168.0.120");
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddress, 6666);
            serverSocket.Bind(ipEndPoint);
            serverSocket.Listen(0);//开始监听端口号   设置为0 数量没有限制

            serverSocket.BeginAccept(AcceptCallBack, serverSocket);

            while (true)
            {
                Console.ReadLine();
            }
        }

        private void AcceptCallBack(IAsyncResult ar)
        {
            Socket serverSocket = ar.AsyncState as Socket;
            Socket clientSocket = serverSocket.EndAccept(ar);

            //向客户端发送消息
            string msg = "Hello World!雷侯啊，啊油OK！";
            var data = Encoding.UTF8.GetBytes(msg);
            clientSocket.Send(data);

            asyncDataBuffer = new byte[1024];
            clientSocket.BeginReceive(asyncDataBuffer, 0, 1024, SocketFlags.None, ReceiveCallBack, clientSocket);

            serverSocket.BeginAccept(AcceptCallBack, serverSocket);
        }

        private void ReceiveCallBack(IAsyncResult ar)
        {
            Socket clientSocket = null;
            try
            {
                clientSocket = (Socket)ar.AsyncState;
                int count = clientSocket.EndReceive(ar);
                if (count <= 0)
                {
                    clientSocket.Close();
                }
                else
                {
                    string msg = Encoding.UTF8.GetString(asyncDataBuffer, 0, count);
                    Console.WriteLine(msg);
                    clientSocket.BeginReceive(asyncDataBuffer, 0, 1024, SocketFlags.None, ReceiveCallBack, clientSocket);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                if (clientSocket != null)
                {
                    clientSocket.Close();
                }
            }
        }
    }
}
