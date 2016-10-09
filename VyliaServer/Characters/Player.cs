using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VyliaServer.Utilities;

namespace VyliaServer.Characters
{
    class Player
    {
        private string ip;
        private string name;
        private int id;

        private FloatPair position;
        private string mapName;

        private int HP;
        private int MaxHP;

        public Player()
        {
            this.position = new FloatPair(10,10);
        }

    }
}
