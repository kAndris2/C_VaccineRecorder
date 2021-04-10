using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VaccineRecorder
{
    abstract class Menu
    {
        protected ConsoleLogger _logger = new ConsoleLogger();

        public void Start()
        {
            while (true)
            {
                ShowMenu();
                try
                {
                    if (!MenuFunctions())
                        break;
                    else
                        Console.Clear();
                }
                catch (Exception e)
                {
                    Console.Clear();
                    _logger.Error($"{e.Message}");
                }
            }
        }

        protected abstract void ShowMenu();
        protected abstract bool MenuFunctions();

        protected void WaitToKey()
        {
            Console.WriteLine("\n--->[A folytatáshoz nyomj entert.]");
            Console.ReadKey();
        }
    }
}
