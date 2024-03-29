using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MegaNzChecker
{
    class Logger
    {
        public enum Type
        {
            WARNING = ConsoleColor.DarkYellow,
            SUCCESS = ConsoleColor.DarkGreen,
            ERROR = ConsoleColor.DarkRed,
            DEBUG = ConsoleColor.DarkBlue

        }

        public static void Printf(string text, Type? type = null)
        {
            if (type != null)
            {
                Console.ForegroundColor = (ConsoleColor)type;
            }
            
            Console.WriteLine($"[{DateTime.Now.ToString("HH:mm:ss")}]" + text);
            Console.ResetColor();
        }
    }
}
