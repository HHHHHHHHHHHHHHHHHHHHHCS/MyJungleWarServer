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

        public MySqlConnection Connect()
        {
            MySqlConnection conn = new MySqlConnection(connStr);
            conn.Open();
            return conn;
        }

        /// <summary>
        /// 查询User
        /// </summary>
        public void SelectUser()
        {
            using (var conn = Connect())
            {
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
            }
        }

        /// <summary>
        /// 插入User
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        public void InsertUser(string username, string password)
        {
            using (var conn = Connect())
            {

                string sqlStr = "insert into user set username=@username,password=@password";
                MySqlCommand cmd = new MySqlCommand(sqlStr, conn);

                cmd.Parameters.AddWithValue("username", username);
                cmd.Parameters.AddWithValue("password", password);
                //上下方法二选一
                //cmd.Parameters.AddRange(new MySqlParameter[] {
                //    new MySqlParameter("username", username)
                //    , new MySqlParameter("password", username)});

                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        public void DeleteUser(string username)
        {
            using (var conn = Connect())
            {

                string sqlStr = "delete from user where username=@username";
                MySqlCommand cmd = new MySqlCommand(sqlStr, conn);
                cmd.Parameters.AddWithValue("username", username);
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// 更新用户密码
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        public void UpdateUser(string username,string password)
        {
            using (var conn = Connect())
            {
                string sqlStr = "update user set password = @password where username = @username";
                MySqlCommand cmd = new MySqlCommand(sqlStr, conn);
                cmd.Parameters.AddWithValue("username", username);
                cmd.Parameters.AddWithValue("password", password);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
