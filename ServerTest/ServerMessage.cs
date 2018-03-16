using System;
using System.Collections.Generic;
using System.Text;

namespace ServerTest
{
    public class ServerMessage
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


        public void AddIndex(int count)
        {
            startIndex += count;
        }


        public string GetOneContent()
        {
            if (startIndex <= 4)
            {
                return null;
            }
            int count = BitConverter.ToInt32(data, 0);
            if ((startIndex - 4) >= count)
            {
                string str = Encoding.UTF8.GetString(data, 4, count);
                Array.Copy(data, count + 4, data, 0, startIndex - 4 - count);
                startIndex -= count +4;
                return str;
            }
            return null;
        }


        public List<string> GetAllContent()
        {
            List<string> strList = new List<string>();
            while(true)
            {
                var str = GetOneContent();
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
