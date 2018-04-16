using MyJungleWarServer.DAO;
using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyJungleWarServer.Servers;

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
    }
}
