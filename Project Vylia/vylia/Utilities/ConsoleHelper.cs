using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Vylia.vylia.Utilities
{
    class ConsoleHelper
    {
        public static void Error(Object obj)
        {
            Error(obj.ToString());
        }

        public static void Error(string s)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(s);
            Console.ResetColor();
        }
    }
}
