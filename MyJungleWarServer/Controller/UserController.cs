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

        public string Login(string data,Client client,Server server)
        {
            string[] strs = data.Split(',');
            User user =  userDAO.VerifyUser(client.SQLConn, strs[0], strs[1]);
            if(user==null)
            {
                return ((int)ReturnCode.Fail).ToString();
            }
            return ((int)ReturnCode.Success).ToString();
        }
    }
}
