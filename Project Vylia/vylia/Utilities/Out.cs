using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Vylia.vylia.Utilities
{
    class Out
    {
        public static void Info(string s)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("[INFO]:\t\t");
            ScreenManager.Instance.addError(new Utilities.GameError(s));
            Console.ResetColor();
            Console.WriteLine(s);
        }

        public static void Error(string s)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("[ERROR]:\t\t");
            ScreenManager.Instance.addError(new Utilities.GameError(s));
            Console.ResetColor();
            Console.WriteLine(s);
        }

        public static void Warning(string s)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("[WARNING]:\t");
            ScreenManager.Instance.addError(new Utilities.GameError(s));
            Console.WriteLine(s);
            Console.ResetColor();
        }

        public static void Success(string s)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("[SUCCESS]:\t");
            ScreenManager.Instance.addError(new Utilities.GameError(s));
            Console.ResetColor();
            Console.WriteLine(s);
        }

        public static void Waiting(string s)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("[WAITING]:\t");
            ScreenManager.Instance.addError(new Utilities.GameError(s));
            Console.ResetColor();
            Console.WriteLine(s);
        }
    }
}
