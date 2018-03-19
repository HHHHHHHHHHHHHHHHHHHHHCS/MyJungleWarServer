using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyJungleWarServer.Servers
{
    public class Message
    {
        private byte[] data = new byte[1024];
        private int startIndex = 0;//我们存取了多少个字节的数据在数组里面

        public byte[] Data
        {
            get
            {
                return data;
            }
        }

        public int StartIndex
        {
            get
            {
                return startIndex;
            }
        }

        public int RemainIndex
        {
            get
            {
                return data.Length - startIndex;
            }
        }

        public byte[] Length
        {
            get; set;
        }


        private void AddIndex(int count)
        {
            startIndex += count;
        }


        public string GetOneContent(int newDataAmount)
        {
            AddIndex(newDataAmount);
            if (startIndex <= 4)
            {
                return null;
            }
            int count = BitConverter.ToInt32(data, 0);
            if ((startIndex - 4) >= count)
            {
                string str = Encoding.UTF8.GetString(data, 4, count);
                Array.Copy(data, count + 4, data, 0, startIndex - 4 - count);
                startIndex -= count + 4;
                return str;
            }
            return null;
        }


        public List<string> GetAllContent(int newDataAmount)
        {
            List<string> strList = new List<string>();
            while (true)
            {
                var str = GetOneContent(newDataAmount);
                if (str != null)
                {
                    strList.Add(str);
                }
                else
                {
                    break;
                }
            }
            return strList;
        }
    }
}
