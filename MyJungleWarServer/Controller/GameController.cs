using Common.Code;
using MyJungleWarServer.Servers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyJungleWarServer.Controller
{
    public class GameController : BaseController
    {
        public GameController()
        {
            requestCode = RequestCode.Game;
        }

        public override string HandleByActionCode(ActionCode code, string data, Client client, Server server)
        {
            string result = null;
            switch (code)
            {
                case ActionCode.Game_Enter:
                    result = PlayerEnterScene(data, client, server);
                    break;
                default:
                    break;
            }
            return result;
        }

        private string PlayerEnterScene(string data, Client client, Server server)
        {
            return "";
        }
    }
}
