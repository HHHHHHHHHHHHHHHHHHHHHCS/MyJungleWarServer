using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    public enum ActionCode
    {
        None,
        Login,
        Register,

        UserData_Create,
        UserData_Get,

        ClientRoom_Show,
        ClientRoom_Create,
        ClientRoom_Join,
        ClientRoom_Ready,
        ClientRoom_Leavel,
    }
}
