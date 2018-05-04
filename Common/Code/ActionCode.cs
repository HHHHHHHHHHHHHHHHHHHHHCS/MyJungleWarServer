using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Code
{
    public enum ActionCode
    {
        None,//null
        Login,//登录
        Register,//注册

        UserData_Create,//创建玩家信息
        UserData_Get,//获取玩家信息

        ClientRoom_Show,//房间显示列表
        ClientRoom_Create,//创建房间
        ClientRoom_Join,//客机加入
        ClientRoom_Come,//主机知道客机进入
        ClientRoom_Ready,//玩家准备
        ClientRoom_AllReady,//全部玩家准备
        ClientRoom_CancelReady,//开始游戏的时候 有玩家取消了准备
        ClientRoom_StartGame,//全部玩家准备
        ClientRoom_Close,//主机关闭房间
        ClientRoom_Leavel,//玩家自己离开
        ClientRoom_Quit,//告诉别人 玩家退出

        Game_Enter,//告诉别人自己加载完成了
        Game_CanDo,//告诉别人可以开始战斗了
    }
}
