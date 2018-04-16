using MyJungleWarServer.Servers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyJungleWarServer.Module
{
    public class ClientRoom
    {
        private enum ClientRoomState
        {
            None,
            WaitingJoin,
            WaitingBattle,
            Battle,
            End
        }


        private Dictionary<Client, HashSet<Client>> clientRoom = new Dictionary<Client, HashSet<Client>>();

        
        public void CreateRoom(Client client,Server server)
        {
            if(!clientRoom.ContainsKey(client))
            {
                HashSet<Client> clientSet = new HashSet<Client>
                {
                    client
                };
                clientRoom.Add(client, clientSet);
            }
        }

    }
}
