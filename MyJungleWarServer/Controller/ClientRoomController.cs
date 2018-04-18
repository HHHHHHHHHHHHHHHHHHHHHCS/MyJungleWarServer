using MyJungleWarServer.DAO;
using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyJungleWarServer.Servers;
using Common.Code;

namespace MyJungleWarServer.Controller
{
    public class ClientRoomController : BaseController
    {
        private ClientRoomDAO userDAO = new ClientRoomDAO();


        public ClientRoomController()
        {
            requestCode = RequestCode.ClientRoom;
        }

        public override string HandleByActionCode(ActionCode code, string data, Client client, Server server)
        {
            string result = null;
            switch (code)
            {
                case ActionCode.ClientRoom_Show:
                    break;
                case ActionCode.ClientRoom_Create:
                    result = CreateRoom(data, client, server);
                    break;
                case ActionCode.ClientRoom_Join:
                    break;
                case ActionCode.ClientRoom_Ready:
                    break;
                case ActionCode.ClientRoom_Leavel:
                    break;
                default:
                    break;
            }
            return result;
        }

        public string CreateRoom(string data, Client client, Server server)
        {
            string result;
            bool isSucceed = server.ClientRoomList.CreateRoom(client);
            if (isSucceed)
            {
                string str = ControllerManager.Instance.GetControllser<UserDataController>(RequestCode.UserData)
                 .UserData_Get(client.GetUsername, client, server);
                result = string.Format("{0},{1}", (int)(isSucceed ? ReturnCode.Success : ReturnCode.Fail), str);
            }
            else
            {
                result = string.Format("{0}", (int)(isSucceed ? ReturnCode.Success : ReturnCode.Fail));
            }
            return result;
        }
    }
}
