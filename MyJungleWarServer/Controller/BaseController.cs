using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace MyJungleWarServer.Controller
{
    public abstract class BaseController
    {
        RequestCode requestCode = RequestCode.None;

        public virtual void DefaultHandle() { }
    }
}
