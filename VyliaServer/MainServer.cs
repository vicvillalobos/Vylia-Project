using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VyliaServer.Characters;
using VyliaServer.Utilities;

namespace VyliaServer
{
    class MainServer
    {
        private List<Player> playerList;

        private static MainServer instance;
        
        public static MainServer Instance {
            get {
                if(instance == null)
                {
                    instance = new MainServer();
                }
                return instance;
            }
        }

        public MainServer()
        {
            playerList = new List<Player>();
        }

        public int playerConnect()
        {
            playerList.Add(new Player());
            Out.Success("Player Connected.");
            return playerList.Count - 1;
        }
    }
}
