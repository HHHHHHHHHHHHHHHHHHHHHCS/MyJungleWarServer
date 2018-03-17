using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace MyJungleWarServer.Controller
{
    public class ControllerManager
    {
        private Dictionary<RequestCode, BaseController> controllerDic =
            new Dictionary<RequestCode, BaseController>();

        public ControllerManager()
        {
            Init();
        }

        private void Init()
        {
            DefaultController defaultController = new DefaultController();
            controllerDic.Add(defaultController.RequestCode, defaultController);
        }

        public void HandleRequest(RequestCode requestCode, ActionCode actionCode, string data)
        {
            var isGet = controllerDic.TryGetValue(requestCode, out BaseController baseController);
            if (isGet)
            {
                Console.WriteLine("无法得到[" + requestCode + "]所对应的Controller，无法处理请求！");
                return;
            }
            string methodName = Enum.GetName(typeof(ActionCode), actionCode);
            MethodInfo methodInfo = baseController.GetType().GetMethod(methodName);
            if (methodInfo == null)
            {
                Console.WriteLine("[警告]在Controller[" + baseController + "]中没有对应的方法:[" + methodName + "]");
                return;
            }
            object result = methodInfo.Invoke(baseController, new object[] { data });
        }
    }
}
