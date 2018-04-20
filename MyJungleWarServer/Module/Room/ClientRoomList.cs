using MyJungleWarServer.Servers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyJungleWarServer.Module.Room
{
    public class ClientRoomList
    {
        private Server server;
        private Dictionary<Client, ClientRoom> clientRoomDic;
        private HashSet<Client> waitClientSet;

        public ClientRoomList(Server server)
        {
            this.server = server;
            clientRoomDic = new Dictionary<Client, ClientRoom>();
            waitClientSet = new HashSet<Client>();
        }

        public List<ClientRoom> GetWaitJoinClientRoom()
        {
            return clientRoomDic.Values
                .Where(room => room.RoomState == ClientRoom.ClientRoomState.WaitingJoin).ToList();
        }

        public ClientRoom[] GetAllClientRoom()
        {
            return clientRoomDic.Values.ToArray();
        }

        public bool CreateRoom(Client client)
        {
            if (!clientRoomDic.ContainsKey(client))
            {
                clientRoomDic.Add(client, ClientRoom.CreateDefaultRoom(client));
                return true;
            }
            return false;
        }

        public bool LeaveRoom(Client client, Server server)
        {
            foreach (var item in clientRoomDic.Values)
            {
                if (item.HomeClient == client)
                {
                    item.LeaveRoom(client, server);
                    clientRoomDic.Remove(client);
                    return true;
                }
                if (item.ClientSet.Contains(client))
                {
                    item.LeaveRoom(client, server);
                    return true;
                }
            }
            return false;
        }

        public string JoinRoom(string username, Client client, Server server)
        {
            foreach (var item in clientRoomDic.Values)
            {
                if (item.HomeClient.GetUsername == username)
                {
                    return item.JoinRoom(client, server);
                }
            }
            return "";
        }

        public void CloseAllRoom(HashSet<Client> clientList)
        {
            foreach (var key in new List<Client>(clientRoomDic.Keys))
            {
                LeaveRoom(key, server);
            }
        }
    }
}
