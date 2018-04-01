using Common;
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
    public class UserController : BaseController
    {
        private UserDAO userDAO = new UserDAO();
        public UserController()
        {
            requestCode = RequestCode.User;
        }

        public override string HandleByActionCode(ActionCode code, string data, Client client, Server server)
        {
            string result = null;
            switch (code)
            {
                case ActionCode.Login:
                    result = Login(data, client, server);
                    break;
                case ActionCode.Register:
                    result = Register(data, client, server);
                    break;
                default:
                    break;
            }
            return result;
        }

        public string Login(string data, Client client, Server server)
        {
            string[] strs = data.Split(',');
            User user = userDAO.VerifyUser(client.SQLConn, strs[0], strs[1]);
            if (user == null)
            {
                return ((int)ReturnCode.Fail).ToString();
            }
            return ((int)ReturnCode.Success).ToString();
        }

        public string Register(string data, Client client, Server server)
        {
            bool isSucceed = false;
            string[] strs = data.Split(',');
            bool haveUser = userDAO.GetUserByUsername(client.SQLConn, strs[0]);
            if (!haveUser)
            {
                isSucceed = userDAO.AddUser(client.SQLConn, strs[0], strs[1]);
            }
            return ((int)(isSucceed ? ReturnCode.Success : ReturnCode.Fail)).ToString();
        }
    }
}
