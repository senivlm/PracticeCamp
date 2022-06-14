using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using System.Globalization;

namespace Task06
{
    class Accounting
    {
        public int Number{ get; set; }
        public string Surname { get; set; }
        public int InputMeter { get; set; }
        public int OutputMeter { get; set; }
        public DateTime[] DatesMeter;
        //Поспілкуватись про проєктування!!!
        private readonly List<Accounting> accountings;
        public Accounting(int number, string surname, int inputMeter, int outputMeter, 
            params DateTime[] datesMeter)
        {
            
            Number = number;
            Surname = surname;
            InputMeter = inputMeter;
            OutputMeter = outputMeter;
            DatesMeter = datesMeter;
        }
        public Accounting(string path)
        {
            accountings = new List<Accounting>();
            string[] accountingsFile = File.ReadAllLines(path);
            int quartal = int.Parse(File.ReadAllLines(path)[0]
                .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Last());
            int amount = int.Parse(File.ReadAllLines(path)[0]
                .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).First());
            foreach (string item in accountingsFile.Skip(1))
            {
                string[] line = item.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                accountings.Add(new Accounting(int.Parse(line[0]),
                    line[1], int.Parse(line[2]), int.Parse(line[3]),
                    new DateTime(2022, (quartal - 1) * 3, int.Parse(line[4])),
                    new DateTime(2022, (quartal - 1) * 3 + 1, int.Parse(line[5])),
                    new DateTime(2022, (quartal - 1) * 3 + 2, int.Parse(line[6]))));
            }
            if (accountings.Count() != amount)
            {
                throw new ArgumentException("Wrong data in file!");
            }
        }
        private string ParseDateTime(DateTime dt)
        {
            return dt.ToString("MMM, dd yyyy", new CultureInfo("en-GB"));
        }
        public Accounting GetInfoAboutFlat(int number)
        {
            foreach (Accounting item in accountings)
            {
                if (item.Number == number)
                {
                    return item;
                }
            }
            return null;
        }
        public string FindDebtor()
        {
            int maxValue = accountings.Max(x => x.OutputMeter - x.InputMeter);
            return string.Join(", ", accountings
                         .Where(x => x.OutputMeter - x.InputMeter == maxValue)
                         .Select(x => x.Surname)
                         .ToArray());
        }
        public string FindUnused()
        {
            return string.Join(", ", accountings
                         .Where(x => x.OutputMeter - x.InputMeter == 0)
                         .Select(x => x.Number)
                         .ToList());
        }
        private Dictionary<int, int> GetUtilities(int price)
        {
            Dictionary<int, int> utilities = new Dictionary<int, int>();
            foreach (Accounting item in accountings)
            {
                utilities[item.Number] = (item.OutputMeter - item.InputMeter) * price;
            }
            return utilities;
        }
        private Dictionary<int, int> GetTimeFromMeterLast()
        {
            Dictionary<int, int> periods = new Dictionary<int, int>();
            foreach (Accounting item in accountings)
            {
                periods[item.Number] = (int)(DateTime.Now - item.DatesMeter[2]).TotalDays;
            }
            return periods;
        }
        public void PrintDataInFile(string fileName)
        {
            StreamWriter streamWriter = new StreamWriter(fileName, false);
            foreach (Accounting item in accountings)
            {
                streamWriter.WriteLine(item);
            }
            streamWriter.Close();
        }
        public void PrintUtilitiesInFile(string fileName, int price)
        {
            StreamWriter streamWriter = new StreamWriter(fileName, false);
            foreach (KeyValuePair<int, int> item in GetUtilities(price))
            {
                streamWriter.WriteLine($"Flat #{item.Key} : UA {item.Value}");
            }
            streamWriter.Close();
        }
        public void PrintPeriodsInFile(string fileName)
        {
            StreamWriter streamWriter = new StreamWriter(fileName, false);
            foreach (KeyValuePair<int, int> item in GetTimeFromMeterLast())
            {
                streamWriter.WriteLine($"Flat #{item.Key} : {item.Value} days");
            }
            streamWriter.Close();
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(ParseDateTime(DatesMeter[0]));
            foreach (DateTime item in DatesMeter.Skip(1))
            {
                sb.Append("\n");
                sb.Append(ParseDateTime(item).PadLeft(25));
            }
            return $"Number: {Number}" +
                $"\nSurname: {Surname}" +
                $"\nInput meter: {InputMeter}" +
                $"\nOutput meter: {OutputMeter}" +
                $"\nDates meter: {sb}\n\n";
        }
    }
}
