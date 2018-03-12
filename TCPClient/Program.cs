using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace TCPClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            clientSocket.Connect(new IPEndPoint(IPAddress.Parse("192.168.0.120"), 6666));

            byte[] data = new byte[1024];
            int count = clientSocket.Receive(data);//调用receive 的时候会暂停程序
            string msg = Encoding.UTF8.GetString(data, 0, count);
            Console.WriteLine(msg);

            while(true)
            {
                string s = Console.ReadLine();
                if(s=="close")
                {
                    clientSocket.Close();
                }
                else
                {
                    clientSocket.Send(Encoding.UTF8.GetBytes(s));
                }

            }


        }
    }
}
