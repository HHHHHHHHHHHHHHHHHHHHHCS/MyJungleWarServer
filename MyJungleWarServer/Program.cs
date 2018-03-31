using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyJungleWarServer
{
    public class Program
    {
        public static void Main(String[] args)
        {
            Servers.Server server = new Servers.Server("127.0.0.1", 2333);
            server.Start();
            while(Console.ReadLine()!="-q")
            {
                server.Close();
            }
        }
    }
}
