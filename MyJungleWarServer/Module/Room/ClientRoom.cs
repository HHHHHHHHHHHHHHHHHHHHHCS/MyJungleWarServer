using Common.Code;
using Common.Model;
using MyJungleWarServer.Controller;
using MyJungleWarServer.Servers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
        public HashSet<Client> ClientSet { get; private set; }
        public HashSet<string> ReadyUsernameSet { get; private set; }

        private const int roomMaxCount = 2;
        private const int waitTime = 5 * 1000;
        private Timer timer;

        public static ClientRoom CreateDefaultRoom(Client client)
        {
            ClientRoom room = new ClientRoom()
            {
                HomeClient = client,
                ClientSet = new HashSet<Client>()
                {
                    client
                },
                ReadyUsernameSet = new HashSet<string>(),
                RoomState = ClientRoomState.WaitingJoin
            };
            return room;
        }

        public string JoinRoom(Client client, Server server)
        {
            if (RoomState == ClientRoomState.WaitingJoin)
            {
                ClientSet.Add(client);
                if (ClientSet.Count >= roomMaxCount)
                {
                    RoomState = ClientRoomState.WaitingBattle;
                }
                var awayUserData = ControllerManager.Instance.GetController<UserDataController>(RequestCode.UserData)
                    .UserData_Get(client.GetUsername, client, server);
                BroadcastMessage(server, client, ActionCode.ClientRoom_Come, awayUserData);
                return awayUserData;
            }
            return "";
        }

        public string ReadyBattle(string data, Client client, Server server)
        {
            StringBuilder sb = new StringBuilder();
            if (data == ((int)ReturnCode.True).ToString())
            {
                ReadyUsernameSet.Add(client.GetUsername);
            }
            else
            {
                ReadyUsernameSet.Remove(client.GetUsername);
            }
            foreach (var item in ReadyUsernameSet)
            {
                sb.Append(item).Append(',');
            }
            if (sb.Length > 0)
            {
                sb.Remove(sb.Length - 1, 1);
            }
            BroadcastMessage(server, null, ActionCode.ClientRoom_Ready, sb.ToString());

            if (ReadyUsernameSet.Count >= roomMaxCount)
            {
                BroadcastMessage(server, null, ActionCode.ClientRoom_AllReady, sb.ToString());
                AllReady(server);
            }
            else if (timer != null && ReadyUsernameSet.Count < roomMaxCount)
            {
                timer.Dispose();
                BroadcastMessage(server, null, ActionCode.ClientRoom_CancelReady, "");
            }
            return null;
        }

        public void AllReady(Server server)
        {
            timer = new Timer(StartGame, server, waitTime, 0);
        }

        public void StartGame(object state)
        {
            Server server = state as Server;
            RoomState = ClientRoomState.Battle;
            ReadyUsernameSet.Clear();
            foreach (var client in ClientSet)
            {
                server.SendRespone(client, ActionCode.ClientRoom_StartGame
                    , ((int)(client == HomeClient ? RoleType.Red : RoleType.Blue)).ToString());
            }
            timer.Dispose();
            timer = null;
        }

        public void LeaveRoom(Client client, Server server)
        {//TODO:判断在战斗的时候玩家离开房间
            if (HomeClient == client)
            {
                _CloseRoom(client, server);
            }
            else
            {
                _LeaveRoom(client, server);
            }
        }

        private void _CloseRoom(Client client, Server server)
        {
            RoomState = ClientRoomState.None;
            if (HomeClient != null)
            {
                BroadcastMessage(server, HomeClient, ActionCode.ClientRoom_Close
                , ((int)ReturnCode.Success).ToString());
                HomeClient = null;
            }
            if(timer!=null)
            {
                timer.Dispose();
                timer = null;
            }

            ClientSet = null;
        }

        private void _LeaveRoom(Client client, Server server)
        {
            ClientSet.Remove(client);
            if (ClientSet.Count < roomMaxCount)
            {
                RoomState = ClientRoomState.WaitingJoin;
            }
            BroadcastMessage(server, client, ActionCode.ClientRoom_Quit, ((int)ReturnCode.Success).ToString());
            if (timer != null)
            {
                timer.Dispose();
                timer = null;
            }
        }

        public string EnterScene(string data, Client client, Server server)
        {
            ReadyUsernameSet.Add(client.GetUsername);
            if (ReadyUsernameSet.Count >= roomMaxCount)
            {
                BroadcastMessage(server, null, ActionCode.Battle_CanPlay,"");
                ReadyUsernameSet.Clear();
            }
            return "";
        }

        public void BroadcastMessage(Server server, Client excludeClient, ActionCode actionCode, string data)
        {
            foreach (var item in ClientSet)
            {
                if (item != excludeClient)
                {
                    server.SendRespone(item, actionCode, data);
                }
            }
        }
    }
}
