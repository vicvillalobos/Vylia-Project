using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Vylia.vylia.Utilities
{
    class GameError
    {
        public int Code;
        public string Message;

        public GameError(string s)
        {
            this.Message = s;
        }
    }
}
