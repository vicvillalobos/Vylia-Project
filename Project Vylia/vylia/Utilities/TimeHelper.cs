using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Vylia.vylia.Utilities
{
    public class TimeHelper
    {
        public static long getActualMiliseconds()
        {
            return DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
        }
    }
}
