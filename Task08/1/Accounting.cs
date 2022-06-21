using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Task08
{
    class Accounting
    {
        private List<Account> accountings;   
        public Accounting(in List<Account> accountings)
        {
            this.accountings = accountings;
        }
        public Accounting(string path)
        {
            accountings = new List<Account>();
            string[] accountingsFile = File.ReadAllLines(path);
            int quartal = int.Parse(File.ReadAllLines(path)[0]
                .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Last());
            int amount = int.Parse(File.ReadAllLines(path)[0]
                .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).First());
            foreach (string item in accountingsFile.Skip(1))
            {
                string[] line = item.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                accountings.Add(new Account(int.Parse(line[0]),
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
        public Account GetInfoAboutFlat(int number)
        {
            foreach (Account item in accountings)
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
            foreach (Account item in accountings)
            {
                utilities[item.Number] = (item.OutputMeter - item.InputMeter) * price;
            }
            return utilities;
        }
        private Dictionary<int, int> GetTimeFromMeterLast()
        {
            Dictionary<int, int> periods = new Dictionary<int, int>();
            foreach (Account item in accountings)
            {
                periods[item.Number] = (int)(DateTime.Now - item.DatesMeter[2]).TotalDays;
            }
            return periods;
        }
        public void PrintDataInFile(string fileName)
        {
            StreamWriter streamWriter = new StreamWriter(fileName, false);
            foreach (Account item in accountings)
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
        public static Accounting operator +(in Accounting lhs, in Accounting rhs)
        {//плутаєте порожній і не створений. Показати в групі
            if(lhs == null)
            {
                return rhs;
            }
            if(rhs == null)
            {
                return lhs;
            }
            return new Accounting(lhs.accountings.Union(rhs.accountings).ToList());
        }
        public static Accounting operator -(Accounting lhs, in Accounting rhs)
        {
            if (lhs == null)
            {
                return null;
            }
            if (rhs == null)
            {
                return lhs;
            }
            lhs.accountings = lhs.accountings.Except(rhs.accountings).ToList();
            return lhs;
        }
        public override bool Equals(object obj)
        {
            return obj is Accounting accounting &&
                   EqualityComparer<List<Account>>.Default.Equals(accountings, accounting.accountings);
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(accountings);
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in accountings)
            {
                sb.Append(item);
            }
            return sb.ToString();
        }


    }
}
