using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace VaccineRecorder
{
    class DataManager
    {
        private const String FILENAME = "oltások.xml";
        private List<Vaccine> Vaccines = new List<Vaccine>();

        public DataManager(ConsoleLogger logger)
        {
            try
            {
                Vaccines.AddRange(ReadVaccines());
            }
            catch (FileNotFoundException)
            {
                logger.Info("Az adatbázis jelenleg üres!");
            }
        }

        public List<Vaccine> GetVaccines() { return Vaccines; }
        public int GetVaccineCount() { return Vaccines.Count; }
        public Vaccine GetVaccine(int index) { return Vaccines[index]; }

        public Vaccine AddNewVaccine(Dictionary<string, string> data)
        {
            Vaccine vaccine = CreateNewVaccine(data);
            Vaccines.Add(vaccine);
            SaveVaccines();
            return vaccine;
        }

        public void RemoveVaccine(int index)
        {
            Vaccines.RemoveAt(index);
            SaveVaccines();

            if (GetVaccineCount() == 0)
                File.Delete(FILENAME);
        }

        public Vaccine UpdateVaccine(int index, Dictionary<string, string> data)
        {
            Vaccine oldVaccine = Vaccines[index],
                    newVaccine = new Vaccine(
                        data["GazdaNév"] == "" ? oldVaccine.Hostname : data["GazdaNév"],
                        data["Cím"] == "" ? oldVaccine.Address : data["Cím"],
                        data["KutyaNév"] == "" ? oldVaccine.DogName : data["KutyaNév"],
                        data["ChipSzám"] == "" ? oldVaccine.ChipID : Int32.Parse(data["ChipSzám"]),
                        data["ElsőOltás"] == "" ? oldVaccine.First : data["Elsőoltás"].ToLower() == "igen"
                    );
            Vaccines[index] = newVaccine;
            SaveVaccines();
            return newVaccine;
        }

        public void SaveVaccines()
        {
            XmlSerializer writer = new XmlSerializer(typeof(List<Vaccine>));

            using (TextWriter writerfinal = new StreamWriter(FILENAME))
            {
                writer.Serialize(writerfinal, Vaccines);
            }
        }

        private Vaccine CreateNewVaccine(Dictionary<string, string> data)
        {
            return new Vaccine(
                    data["GazdaNév"],
                    data["Cím"],
                    data["KutyaNév"],
                    Int32.Parse(data["ChipSzám"]),
                    data["ElsőOltás"].ToLower() == "igen"
            );
        }

        private List<Vaccine> ReadVaccines()
        {
            XmlSerializer reader = new XmlSerializer(typeof(List<Vaccine>));
            List<Vaccine> vaccines;

            using (FileStream readfile = File.OpenRead(FILENAME))
            {
                vaccines = (List<Vaccine>)reader.Deserialize(readfile);
            }
            return vaccines;
        }
    }
}
