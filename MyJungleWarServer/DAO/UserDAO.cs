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


        public Model.User VerifyUser(MySqlConnection conn, string _username
            , string _password)
        {
            Model.User user = null;
            try
            {
                MySqlCommand cmd = new MySqlCommand(verifyUserSQL, conn);
                cmd.Parameters.AddWithValue("@username", _username);
                cmd.Parameters.AddWithValue("@password", _password);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        int id = reader.GetInt32("id");
                        string username = reader.GetString("username");
                        string password = reader.GetString("password");
                        user = new Model.User(id, _username, _password);
                    }
                    reader.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(GetType() + "在VerifyUser时候出现异常" + e);
            }
            return user;
        }

        public bool GetUserByUsername(MySqlConnection conn, string _username)
        {
            bool haveUser = false;
            try
            {

                MySqlCommand cmd = new MySqlCommand(getUserByUsernameSQL, conn);
                cmd.Parameters.AddWithValue("@username", _username);
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
                Console.WriteLine(GetType() + "在GetUserByUsername时候出现异常" + e);
            }
            return haveUser;
        }

        public bool AddUser(MySqlConnection conn, string _username, string _password)
        {
            bool isSucceed = false;
            try
            {
                MySqlCommand cmd = new MySqlCommand(addUserSQL, conn);
                cmd.Parameters.AddWithValue("@username", _username);
                cmd.Parameters.AddWithValue("@password", _password);
                if (cmd.ExecuteNonQuery() > 0)
                {
                    isSucceed = true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(GetType() + "在AddUser时候出现异常" + e);
            }
            return isSucceed;
        }


    }
}
