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
        private const string verifyUserSQL = @"select * from user where username= @username and passowrod = @password";

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
            catch(Exception e)
            {
                Console.WriteLine("在VerifyUser时候出现异常"+e);
            }
            return user;
        }
    }
}
