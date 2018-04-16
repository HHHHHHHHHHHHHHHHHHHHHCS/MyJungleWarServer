using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyJungleWarServer.DAO
{
    public class UserDataDAO
    {
        private const string createDefaultUserData = @"insert into userdata(username) values (@username)";
        private const string getUserData = @"select * from userdata where username= @username";


        public bool CreateDefaultUserData(MySqlConnection conn, string _username)
        {
            bool isSucceed = false;
            try
            {
                MySqlCommand cmd = new MySqlCommand(createDefaultUserData, conn);
                cmd.Parameters.AddWithValue("@username", _username);
                if (cmd.ExecuteNonQuery() > 0)
                {
                    isSucceed = true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(GetType()+"在CreateDefaultUserData时候出现异常" + e);
            }
            return isSucceed;
        }

        public Model.UserData GetUserData(MySqlConnection conn, string _username)
        {
            Model.UserData userdata = null;
            try
            {
                MySqlCommand cmd = new MySqlCommand(getUserData, conn);
                cmd.Parameters.AddWithValue("@username", _username);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
                        string username =  reader.GetString("username");
                        Int32 totalCount = reader.GetInt16("totalcount");
                        Int32 winCount = reader.GetInt16("wincount");
                         userdata = new Model.UserData(username, totalCount, winCount);
                    }
                    reader.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(GetType()+ "在GetUserData时候出现异常" + e);
            }
            return userdata;
        }

    }
}
