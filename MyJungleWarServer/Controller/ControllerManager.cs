using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using MyJungleWarServer.Servers;
using Common.Code;

namespace MyJungleWarServer.Controller
{
    public class ControllerManager
    {
        internal static ControllerManager Instance;

        private Dictionary<RequestCode, BaseController> controllerDic =
            new Dictionary<RequestCode, BaseController>();
        private Server server;

        public ControllerManager(Server _server)
        {
            Instance = this;
            server = _server;
            InitController();
        }

        private void InitController()
        {
            DefaultController defaultController = new DefaultController();
            controllerDic.Add(defaultController.RequestCode, defaultController);
            UserController userController = new UserController();
            controllerDic.Add(userController.RequestCode, userController);
            UserDataController userDataController = new UserDataController();
            controllerDic.Add(userDataController.RequestCode, userDataController);
            ClientRoomController clientRoomController = new ClientRoomController();
            controllerDic.Add(clientRoomController.RequestCode, clientRoomController);
        }

        public T GetControllser<T>(RequestCode requestCode) where T: BaseController
        {
            controllerDic.TryGetValue(requestCode, out BaseController baseController);
            if (baseController==null)
            {
                Console.WriteLine("无法得到[" + requestCode + "]所对应的Controller，无法处理请求！");
                return null;
            }
            return (T)baseController;
        }

        public void HandleRequest(RequestCode requestCode, ActionCode actionCode
            , string data,Client client)
        {
            var baseController = GetControllser<BaseController>(requestCode);
            if(baseController!=null)
            {
                string result = baseController.HandleByActionCode(actionCode, data, client, server);
                server.SendRespone(client, actionCode, result);
            }
        }
    }
}
