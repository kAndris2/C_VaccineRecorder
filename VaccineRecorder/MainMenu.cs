using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VaccineRecorder
{
    class MainMenu : Menu
    {
        ConsoleLogger _logger;
        DataManager _data;

        public MainMenu()
        {
            _logger = new ConsoleLogger();
            _data = new DataManager(_logger);
        }

        protected override void ShowMenu()
        {
            Console.WriteLine("[FŐMENÜ]:\n");
            List<string> options = new List<string>
            {
                "Új oltási bejegyzés készítés",
                "Összes oltási bejegyzés lekérése",
                "Oltási bejegyzés módosítás",
                "Oltási bejegyzés törlés"
            };

            for (int i = 0; i < options.Count; i++)
                Console.WriteLine($"({i + 1}). - {options[i]}");
            Console.WriteLine("\n(0). - Kilépés.");
        }

        protected override bool MenuFunctions()
        {
            Console.WriteLine("\nA kiválasztáshoz írd be az egyik menüpont sorszámát:");
            string input = Console.ReadLine();

            if (input == "0")
            {
                Environment.Exit(-1);
                return false;
            }
            else if (input == "1")
            {
                Dictionary<string, string> inputs = GetInputs();
                Vaccine vaccine = _data.AddNewVaccine(inputs);

                Console.WriteLine();
                _logger.Success("Sikeresen létehoztál egy új oltási bejegyzést!");
                Console.WriteLine(vaccine.ToString());

                WaitToKey();
                return true;
            }
            else if (input == "2")
            {
                if (_data.GetVaccineCount() != 0)
                {
                    Console.Clear();

                    PrintVaccines();
                    _logger.Info($"Összesen {_data.GetVaccineCount()} oltási bejegyzést találtam.");

                    WaitToKey();
                    return true;
                }
                else
                    throw new ArgumentException("Az adatbázis jelenleg üres!");
            }
            else if (input == "3")
            {
                if (_data.GetVaccineCount() != 0)
                {
                    Console.Clear();
                    int index = GetVaccineIndex("módosítani");

                    Console.Clear();
                    _logger.Info("Ha egy részt nem töltesz ki akkor a régi érték marad meg!");
                    WaitToKey();

                    Dictionary<string, string> inputs = GetInputs();
                    Vaccine vaccine = _data.UpdateVaccine(index, inputs);

                    Console.Clear();
                    _logger.Success("Sikeresen módosítottál egy bejegyzést!");
                    Console.WriteLine(vaccine.ToString());

                    WaitToKey();
                    return true;
                }
                else
                    throw new ArgumentException("Az adatbázis jelenleg üres!");
            }
            else if (input == "4")
            {
                if (_data.GetVaccineCount() != 0)
                {
                    Console.Clear();

                    int index = GetVaccineIndex("törölni");
                    Vaccine vaccine = _data.GetVaccine(index);

                    Console.Clear();
                    _data.RemoveVaccine(index);
                    _logger.Success("Sikeresen töröltél egy oltási bejegyzést!\n");
                    Console.WriteLine(vaccine.ToString());

                    WaitToKey();
                    return true;
                }
                else
                    throw new ArgumentException("Az adatbázis jelenleg üres!");
            }
            else
                throw new KeyNotFoundException($"Ismeretlen sorszám - ('{input}')");
        }

        private int GetVaccineIndex(string messagePart)
        {
            PrintVaccines();
            Console.WriteLine($"Írd be annak a bejegyzésnek a sorszámát amelyiket {messagePart} szeretnéd:");
            string str = Console.ReadLine();

            if (int.TryParse(str, out int index))
            {
                if (index <= 0 || index > _data.GetVaccineCount())
                    throw new ArgumentException($"Érvénytelen sorszám! - ('{index}')");
                else
                    return index - 1;
            }
            else
                throw new ArgumentException($"A megadott érték nem szám! - ('{str}')");
        }

        private void PrintVaccines()
        {
            List<Vaccine> vaccines = _data.GetVaccines();

            for (int i = 0; i < vaccines.Count; i++)
            {
                Console.WriteLine($"({i + 1}):\n{vaccines[i]}\n");
            }
        }

        private Dictionary<string, string> GetInputs()
        {
            Dictionary<string, string> inputs = new Dictionary<string, string>()
            {
                {"GazdaNév", null}, {"Cím", null}, {"KutyaNév", null},
                {"ChipSzám", null}, {"ElsőOltás", null}
            };

            foreach(KeyValuePair<string, string> item in inputs)
            {
                Console.Clear();
                Console.WriteLine(
                    $"Kérlek add meg a következő adatot: {item.Key} " +
                    $"{(item.Key.Equals("ElsőOltás") ? "(Igen/Nem)" : "")}"
                );
                inputs[item.Key] = Console.ReadLine();
            }
            return inputs;
        }
    }
}
