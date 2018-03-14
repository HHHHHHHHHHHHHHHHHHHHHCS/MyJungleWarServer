using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace TCPClient
{
    class Client
    {
        static void Main(string[] args)
        {
            MySQLConnect.Main main = new MySQLConnect.Main();
            main.SelectUser();
             Console.ReadLine();
            return;

            Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            clientSocket.Connect(new IPEndPoint(IPAddress.Parse("192.168.0.120"), 6666));

            byte[] data = new byte[1024];
            int count = clientSocket.Receive(data);//调用receive 的时候会暂停程序
            string msg = Encoding.UTF8.GetString(data, 0, count);
            Console.WriteLine(msg);

            string str = @"-1却渴望和归还欠款危机恶化矿井全部为科技行情为科技beg虹口区火热空间去我何
-2苦如何为恐怖户籍卡温热包工头环境而不可喝完就可很快进入妻儿哦f9bvu8x就快崩溃her科技部阿萨德
-3却渴望和归还欠款危机恶化矿井全部为科技行情为科技beg虹口区火热空间去我何
-4苦如何为恐怖户籍卡温热包工头环境而不可喝完就可很快进入妻儿哦f9bvu8x就快崩溃her科技部
-5而不可喝阿萨德阿萨德下次vxcvx完就可很快进入妻儿哦f9bvu8x就快崩溃her科技部阿萨德
-6却渴望和归还欠款危机恶化矿井全部为科技行情为科技beg虹口区火热空间去我何
-7苦如何为恐怖青蛙户籍卡温热包工头环境而不可
-8而不可喝完就突然同人可很快进入妻儿哦f9bvu8x就快崩溃her科技部阿萨德
-9却渴望和任天野人归还欠款危机恶化矿井全部为科技行情为科技beg虹口区火热空间去我何
-10苦如何为恐怖户籍卡温热包工头环境而不可
-11苦如何为恐怖户籍卡温热包工头环境而不可喝完就可很快进入妻儿哦f9bvu8x就快崩溃her科技部阿萨德
-12却渴望和归还欠款危机恶化矿井全部为科技行情为科技beg虹口区火热空间去我何
-13苦如何为恐怖户籍卡温热包工头环境而不可喝完就可很快进入妻儿哦f9bvu8x就快崩溃her科技部
-14而不可喝阿萨德阿萨德下次vxcvx完就可很快进入妻儿哦f9bvu8x就快崩溃her科技部阿萨德
-15却渴望和归还欠款危机恶化矿井全部为科技行情为科技beg虹口区火热空间去我何
-16苦如何为恐怖青蛙户籍卡温热包工头环境而不可
-17而不可喝完就突然同人可很快进入妻儿哦f9bvu8x就快崩溃her科技部阿萨德
-18却渴望和任天野人归还欠款危机恶化矿井全部为科技行情为科技beg虹口区火热空间去我何
-19苦如何为恐怖户籍卡温热包工头环境而不可";

            for(int i=0;i<100;i++)
            {
                clientSocket.Send(ClientMessage.GetBytes(i.ToString()));
                Thread.Sleep(1);
            }



            while (true)
            {
                string s = Console.ReadLine();
                if (s == "close")
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
