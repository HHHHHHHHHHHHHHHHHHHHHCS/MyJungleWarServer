using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        }
    }
}
