using Common;
using Common.Code;
using MyJungleWarServer.DAO;
using MyJungleWarServer.Model;
using MyJungleWarServer.Servers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyJungleWarServer.Controller
{
    public class UserDataController : BaseController
    {
        private UserDataDAO userDataDAO = new UserDataDAO();

        public UserDataController()
        {
            requestCode = RequestCode.UserData;
        }

        public override string HandleByActionCode(ActionCode code, string data, Client client, Server server)
        {
            string result = null;
            switch (code)
            {
                case ActionCode.UserData_Create:
                    result = UserData_Create(data, client, server);
                    break;
                case ActionCode.UserData_Get:
                    result = UserData_Get(data, client, server);
                    break;
                default:
                    break;
            }
            return result;
        }


        public string UserData_Create(string data, Client client, Server server)
        {
            string[] strs = data.Split(',');
            bool isSucceed = userDataDAO.CreateDefaultUserData(client.SQLConn, strs[0]);
            if (!isSucceed)
            {
                Console.WriteLine(GetType() + "[" + strs[0] + "]在自动添加CreateDefaultUserData出现问题！");
            }
            return ((int)(isSucceed ? ReturnCode.Success : ReturnCode.Fail)).ToString();
        }

        public string UserData_Get(string data, Client client, Server server)
        {
            string result;
            string username = data;
            UserData userdata = userDataDAO.GetUserData(client.SQLConn, username);
            if (userdata == null)
            {
                UserData_Create(data, client, server);
                userdata = userDataDAO.GetUserData(client.SQLConn, username);
            }
            if (userdata == null)
            {
                result = string.Format("{0},{1},{2}", username, 0, 0);
                Console.WriteLine(GetType() + "[" + username + "]在获取UserData_Get出现问题！");
            }
            else
            {
                result = string.Format("{0},{1},{2}", userdata.Username, userdata.TotalCount, userdata.WinCount);
            }
            return result;
        }
    }
}
