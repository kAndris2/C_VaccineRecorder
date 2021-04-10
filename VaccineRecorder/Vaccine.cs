using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VaccineRecorder
{
    public class Vaccine
    {
        public String Hostname { get; set; }
        public String Address { get; set; }
        public String DogName { get; set; }
        public int ChipID { get; set; }
        public DateTime Date { get; set; }
        public DateTime Expire { get; set; }
        public Boolean First { get; set; }

        public Vaccine() { }

        public Vaccine(string hostanme, string address, string dogname, int chipid, bool first)
        {
            Hostname = hostanme;
            Address = address;
            DogName = dogname;
            ChipID = chipid;
            Date = DateTime.Now;
            Expire = Date.AddMonths(6);
            First = first;
        }

        public override String ToString()
        {
            return $"Gazda: {Hostname}\n" +
                   $"Cím: {Address}\n" +
                   $"Kutya: {DogName}\n" +
                   $"Chip: {ChipID}\n" +
                   $"Dátum: {Date.ToString("yyyy.MMM.dd.")}\n" +
                   $"Lejár: {Expire.ToString("yyyy.MMM.dd.")}\n" +
                   $"Első oltás: {(First ? "Igen" : "Nem")}";
        }
    }
}
