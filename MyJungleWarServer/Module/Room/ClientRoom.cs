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
                AwayClient = client;
                var awayUserData = ControllerManager.Instance.GetControllser<UserDataController>(RequestCode.UserData)
                    .UserData_Get(client.GetUsername, client, server);
                server.SendRespone(HomeClient, ActionCode.ClientRoom_Come, awayUserData);
                return awayUserData;
            }
            return "";
        }

        public void LeaveRoom(Client client, Server server)
        {
            if (HomeClient == client)
            {
                CloseRoom(server);
            }
            else if (AwayClient == client)
            {
                LeaveRoom(server);
            }
        }

        private void CloseRoom(Server server)
        {
            RoomState = ClientRoomState.None;
            if (HomeClient != null)
            {
                HomeClient = null;
            }
            if (AwayClient != null)
            {
                server.SendRespone(AwayClient, ActionCode.ClientRoom_Close
                    , ((int)ReturnCode.Success).ToString());
                AwayClient = null;
            }
        }

        private void LeaveRoom(Server server)
        {
            RoomState = ClientRoomState.WaitingJoin;
            if (HomeClient != null && AwayClient != null)
            {
                server.SendRespone(HomeClient, ActionCode.ClientRoom_Quit
                    , ((int)ReturnCode.Success).ToString());
                AwayClient = null;
            }
        }
    }
}
