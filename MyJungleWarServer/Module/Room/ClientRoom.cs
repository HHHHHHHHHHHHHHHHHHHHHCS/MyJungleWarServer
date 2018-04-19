using Common.Code;
using MyJungleWarServer.Controller;
using MyJungleWarServer.Servers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyJungleWarServer.Module.Room
{
    public class ClientRoom
    {
        public enum ClientRoomState
        {
            None,
            WaitingJoin,
            WaitingBattle,
            Battle,
            End
        }

        public ClientRoomState RoomState { get; private set; }
        public Client HomeClient { get; private set; }
        //public HashSet<Client> ClientSet { get; private set; }//这里是一个1v1的用不到
        public Client AwayClient { get; private set; }

        public static ClientRoom CreateDefaultRoom(Client client)
        {
            ClientRoom room = new ClientRoom()
            {
                HomeClient = client
                ,
                RoomState = ClientRoomState.WaitingJoin
            };
            return room;
        }

        public string JoinRoom(Client client, Server server)
        {
            RoomState = ClientRoomState.WaitingBattle;
            if (AwayClient == null)
            {
                var awayUserData = ControllerManager.Instance.GetControllser<UserDataController>(RequestCode.UserData)
                    .UserData_Get(client.GetUsername, client, server);
                server.SendRespone(HomeClient, ActionCode.ClientRoom_Come, awayUserData);
                AwayClient = client;
                return awayUserData;
            }
            return "";
        }

        public void CloseRoom()
        {
            //通知Home房间关闭
            //通知Away用户房间关闭
            RoomState = ClientRoomState.None;
            if (HomeClient != null)
            {
                HomeClient = null;
            }
            if (AwayClient != null)
            {
                AwayClient = null;
            }
        }

        public void LeaveRoom()
        {
            //通知Home房间  Away离开了
            //通知清空Away
            RoomState = ClientRoomState.WaitingJoin;
            if (AwayClient != null)
            {
                AwayClient = null;
            }
        }
    }
}
