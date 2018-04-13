using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyJungleWarServer.DAO
{
    public class UserDAO
    {
        private const string verifyUserSQL = @"select * from user where username= @username and password = @password";
        private const string getUserByUsernameSQL = @"select * from user where username= @username";
        private const string addUserSQL = @"insert into user(username, password) values (@username,@password)";


        public Model.User VerifyUser(MySqlConnection conn, string username
            , string password)
        {
            Model.User user = null;
            try
            {
                MySqlCommand cmd = new MySqlCommand(verifyUserSQL, conn);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        int _id = reader.GetInt32("id");
                        string _username = reader.GetString("username");
                        string _password = reader.GetString("password");
                        user = new Model.User(_id, _username, _password);
                    }
                    reader.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("在VerifyUser时候出现异常" + e);
            }
            return user;
        }

        public bool GetUserByUsername(MySqlConnection conn, string username)
        {
            bool haveUser = false;
            try
            {

                MySqlCommand cmd = new MySqlCommand(getUserByUsernameSQL, conn);
                cmd.Parameters.AddWithValue("@username", username);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        haveUser = true;
                    }
                    reader.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("在GetUserByUsername时候出现异常" + e);
            }
            return haveUser;
        }

        public bool AddUser(MySqlConnection conn, string username, string password)
        {
            bool isSucceed = false;
            try
            {
                MySqlCommand cmd = new MySqlCommand(addUserSQL, conn);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);
                if (cmd.ExecuteNonQuery() > 0)
                {
                    isSucceed = true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("在AddUser时候出现异常" + e);
            }
            return isSucceed;
        }

    }
}
