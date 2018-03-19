using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace MyJungleWarServer.Tool
{
    public class ConnHelper
    {
        private const string connStr = @"Server= 127.0.0.1;Port= 3306;Database=myjunglewarsql;Uid=root;";

        public static MySqlConnection Connect()
        {
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                conn.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine("连接数据库的时候出现异常：" + e);
            }
            return conn;
        }

        public static void Close(MySqlConnection conn)
        {
            if(conn!=null)
            {
                conn.Close();
            }
            else
            {
                Console.WriteLine("MySqlConnection 关闭时，不能为空");
            }
        }
    }
}
