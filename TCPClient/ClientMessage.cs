using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace TCPClient
{
    public class ClientMessage
    {
        public static byte[] GetBytes(string data)
        {
            byte[] dataBytes = Encoding.UTF8.GetBytes(data);
            byte[] dataLength = BitConverter.GetBytes(dataBytes.Length);
            var a = dataLength.Concat(dataBytes).ToArray();
            return a;
        }

        //短串用，  长串效率和空间不是很好
        //public static byte[] Merge(byte[] length, byte[] data)
        //{
        //    byte[] by = new byte[length.Length + data.Length];
        //    length.CopyTo(by, 0);
        //    data.CopyTo(by, length.Length);
        //    return by;
        //}
    }
}
