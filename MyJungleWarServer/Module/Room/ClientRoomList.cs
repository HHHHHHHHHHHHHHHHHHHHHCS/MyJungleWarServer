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

        
        public bool CreateRoom(Client client)
        {
            if(!clientRoomDic.ContainsKey(client))
            {
                clientRoomDic.Add(client, ClientRoom.CreateDefaultRoom(client));
                return true;
            }
            return false;
        }

        public void CloseRoom(Client client)
        {
            if (clientRoomDic.ContainsKey(client))
            {
                clientRoomDic.Remove(client);
            }
        }

        public void CloseAllRoom(HashSet<Client> clientList)
        {
            while(clientRoomDic.Count>0)
            {
                CloseRoom(clientRoomDic.ElementAt(0).Key);
            }
        }
    }
}
