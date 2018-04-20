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
        public HashSet<Client> ClientSet { get; private set; }

        private const int roomMaxCount = 2;

        public static ClientRoom CreateDefaultRoom(Client client)
        {
            ClientRoom room = new ClientRoom()
            {
                HomeClient = client,
                ClientSet = new HashSet<Client>()
                {
                    client
                },
                RoomState = ClientRoomState.WaitingJoin
            };
            return room;
        }

        public string JoinRoom(Client client, Server server)
        {
            if (RoomState == ClientRoomState.WaitingJoin)
            {
                if (!ClientSet.Contains(client))
                {
                    ClientSet.Add(client);
                    if (ClientSet.Count >= roomMaxCount)
                    {
                        RoomState = ClientRoomState.WaitingBattle;
                    }
                    var awayUserData = ControllerManager.Instance.GetControllser<UserDataController>(RequestCode.UserData)
                        .UserData_Get(client.GetUsername, client, server);
                    BroadcastMessage(server, client, ActionCode.ClientRoom_Come, awayUserData);
                    return awayUserData;
                }
            }
            return "";
        }

        public void LeaveRoom(Client client, Server server)
        {
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
