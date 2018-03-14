using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;

namespace MySQLConnect
{
    public class Main
    {
        private const string connStr = @"Server= 127.0.0.1;Port= 3306;Database=myjunglewarsql;
Uid=root;";

        private MySqlConnection conn;

        public void Connect()
        {
            conn = new MySqlConnection(connStr);
            conn.Open();
        }

        public void Close()
        {
            conn.Close();
        }

        public void SelectUser()
        {
            Connect();

            string sqlStr = "select * from user";
            MySqlCommand cmd = new MySqlCommand(sqlStr, conn);
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                var username = reader.GetString("username");
                var password = reader.GetString("password");
                Console.WriteLine(username + ":" + password);
            }
            reader.Close();
            Close();
        }
    }
}
