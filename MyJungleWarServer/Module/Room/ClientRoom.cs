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

        public ClientRoomState RoomState{ get; private set; }

        private HashSet<Client> clientSet;

        public static ClientRoom CreateDefaultRoom(Client client)
        {
            ClientRoom room = new ClientRoom()
            {
                clientSet = new HashSet<Client>()
                {
                    client
                }
                , RoomState = ClientRoomState.None
            };
            return room;
        }
    }
}
