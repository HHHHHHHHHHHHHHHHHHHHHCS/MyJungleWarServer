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
        private DefaultController defaultController;
        private UserController userController;
        private UserDataController userDataController;
        private ClientRoomController clientRoomController;

        public ControllerManager(Server _server)
        {
            Instance = this;
            server = _server;
            InitController();
        }

        private void InitController()
        {
            defaultController = new DefaultController();
            controllerDic.Add(defaultController.RequestCode, defaultController);
            userController = new UserController();
            controllerDic.Add(userController.RequestCode, userController);
            userDataController = new UserDataController();
            controllerDic.Add(userDataController.RequestCode, userDataController);
            clientRoomController = new ClientRoomController();
            controllerDic.Add(clientRoomController.RequestCode, clientRoomController);
        }

        public T GetController<T>(RequestCode requestCode) where T : BaseController
        {
            BaseController baseController = null;
            switch (requestCode)
            {
                case RequestCode.None:
                    baseController = defaultController;
                    break;
                case RequestCode.User:
                    baseController = userController;
                    break;
                case RequestCode.UserData:
                    baseController = userDataController;
                    break;
                case RequestCode.ClientRoom:
                    baseController = clientRoomController;
                    break;
                default:
                    break;
            }
            if (baseController == null)
            {
                controllerDic.TryGetValue(requestCode, out baseController);
                if (baseController == null)
                {
                    Console.WriteLine("无法得到[" + requestCode + "]所对应的Controller，无法处理请求！");
                }
            }
            return (T)baseController;
        }

        public void HandleRequest(RequestCode requestCode, ActionCode actionCode
            , string data, Client client)
        {
            var baseController = GetController<BaseController>(requestCode);
            if (baseController != null)
            {
                Console.WriteLine(actionCode+"////"+data);
                string result = baseController.HandleByActionCode(actionCode, data, client, server);
                server.SendRespone(client, actionCode, result);
            }
        }
    }
}
