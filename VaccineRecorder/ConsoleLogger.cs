using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VaccineRecorder
{
    class ConsoleLogger
    {
        public void Success(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("[SIKER]: ");
            Console.ResetColor();
            Console.Write(message);
            Console.WriteLine();
        }

        public void Error(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("[HIBA]: ");
            Console.ResetColor();
            Console.Write(message + "\n");
            Console.WriteLine();
        }

        public void Info(string message)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("[INFO]: ");
            Console.ResetColor();
            Console.Write(message + "\n");
            Console.WriteLine();
        }
    }
}
