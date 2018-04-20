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

        private string Login(string data, Client client, Server server)
        {
            string[] strs = data.Split(',');
            User user = userDAO.VerifyUser(client.SQLConn, strs[0], strs[1]);
            string result;
            if (user == null)
            {
                result=((int)ReturnCode.Fail).ToString();
            }
            else
            {
                var userdata = ControllerManager.Instance.GetController<UserDataController>(RequestCode.UserData)
                    .UserData_Get(strs[0], client, server);
                result = string.Format("{0},{1}", (int)ReturnCode.Success, userdata);
                client.SetUsername(strs[0]);
            }
            return result;
        }

        private string Register(string data, Client client, Server server)
        {
            bool isSucceed = false;
            string[] strs = data.Split(',');
            bool haveUser = userDAO.GetUserByUsername(client.SQLConn, strs[0]);
            if (!haveUser)
            {
                isSucceed = userDAO.AddUser(client.SQLConn, strs[0], strs[1]);
                if (isSucceed)
                {
                    ControllerManager.Instance.GetController<UserDataController>(RequestCode.UserData)
                        .UserData_Create( strs[0],client,server);
                }
            }
            string result = string.Format("{0},{1},{2}"
                , (int)(isSucceed ? ReturnCode.Success : ReturnCode.Fail)
                , strs[0], strs[1]);
            return result;
        }
    }
}
