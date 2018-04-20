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
        private Dictionary<Client, ClientRoom> clientRoomDic = new Dictionary<Client, ClientRoom>();

        public List<ClientRoom> GetWaitJoinClientRoom()
        {
            return clientRoomDic.Values.ToList().FindAll(room => room.RoomState == ClientRoom.ClientRoomState.WaitingJoin);
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

        public bool LeaveRoom(Client client)
        {
            clientRoomDic.TryGetValue(client, out ClientRoom room);
            if (room!=null)
            {
                room.LeaveRoom(client);
                clientRoomDic.Remove(client);
                return true;
            }
            else
            {
                var clientRoom = clientRoomDic.Values.ToList().Find(p => p.AwayClient == client);
                if (clientRoom != null)
                {
                    clientRoom.LeaveRoom(client);
                    return true;
                }
            }
            return false;
        }

        public string JoinRoom(string username,Client client,Server server)
        {
            var clientRoom = clientRoomDic.Values.ToList().Find(p => p.HomeClient.GetUsername == username);
            if (clientRoom != null)
            {
                return clientRoom.JoinRoom(client,server);
            }
            return "";
        }

        public void CloseAllRoom(HashSet<Client> clientList)
        {
            while (clientRoomDic.Count > 0)
            {
                LeaveRoom(clientRoomDic.ElementAt(0).Key);
            }
        }
    }
}
