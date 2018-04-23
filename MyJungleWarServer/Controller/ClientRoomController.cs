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
                    result = ShowRoomList(data, client, server);
                    break;
                case ActionCode.ClientRoom_Create:
                    result = CreateRoom(data, client, server);
                    break;
                case ActionCode.ClientRoom_Join:
                    result = JoinRoom(data, client, server);
                    break;
                case ActionCode.ClientRoom_Ready:
                    result = ReadyBattle(data, client, server);
                    break;
                case ActionCode.ClientRoom_Leavel:
                    result = LeaveRoom(data, client, server);
                    break;
                default:
                    break;
            }
            return result;
        }


        private string ShowRoomList(string data, Client client, Server server)
        {
            StringBuilder sb = new StringBuilder();
            var clientRoom = server.ClientRoomList.GetWaitJoinClientRoom();
            var userData = ControllerManager.Instance.GetController<UserDataController>(RequestCode.UserData);
            string userDataStr;
            foreach (var item in clientRoom)
            {
                userDataStr = userData.UserData_Get(item.HomeClient.GetUsername, client, server);
                sb.Append(userDataStr).Append('|');
            }
            if (sb.Length > 0)
            {
                sb.Remove(sb.Length - 1, 1);
            }
            return sb.ToString();
        }

        private string CreateRoom(string data, Client client, Server server)
        {
            string result;
            bool isSucceed = server.ClientRoomList.CreateRoom(client);
            if (isSucceed)
            {
                string str = ControllerManager.Instance.GetController<UserDataController>(RequestCode.UserData)
                 .UserData_Get(client.GetUsername, client, server);
                result = string.Format("{0},{1}", (int)(isSucceed ? ReturnCode.Success : ReturnCode.Fail), str);
            }
            else
            {
                result = string.Format("{0}", (int)(isSucceed ? ReturnCode.Success : ReturnCode.Fail));
            }
            return result;
        }

        private string JoinRoom(string data, Client client, Server server)
        {
            string result = "";
            var awayUserData = server.ClientRoomList.JoinRoom(data, client, server);

            if (!string.IsNullOrEmpty(awayUserData))
            {
                string userDataStr = ControllerManager.Instance.GetController<UserDataController>(RequestCode.UserData)
                    .UserData_Get(data, client, server);
                result = string.Format("{0},{1},{2}", (int)ReturnCode.Success, userDataStr, awayUserData);
            }
            else
            {
                result = ((int)ReturnCode.Fail).ToString();
            }
            return result;
        }


        private string ReadyBattle(string data, Client client, Server server)
        {
            return server.ClientRoomList.ReadyBattle(data, client, server);
        }

        private string LeaveRoom(string data, Client client, Server server)
        {
            string result = server.ClientRoomList.LeaveRoom(client, server)
                ? ((int)ReturnCode.Success).ToString() : ((int)ReturnCode.Fail).ToString();
            return result;
        }
    }
}
