using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Task07
{
    class Log
    {
        readonly Dictionary<DateTime, string> Logs;
        readonly string file;
        public Log(string file)
        {
            Logs = new Dictionary<DateTime, string>();
            this.file = file;
        }
        private void Append(string errorMessage)
        {
            using StreamWriter streamWriter = File.AppendText($"..\\..\\..\\{file}");
            streamWriter.WriteLine($"{DateTime.Now} : {errorMessage}");
        }
        public void Add(string errorMessage)
        {
            Append(errorMessage);
            Logs.Add(DateTime.Now, errorMessage);
        }
        private DateTime ParseDate(string date)
        {
            if (DateTime.TryParse(date, out DateTime dt))
            {
                return dt;
            }
            throw new ArgumentException("Invalid date");
        }
        private List<string> ShowByDate(DateTime dt)
        {
            List<string> logs = new List<string>();
            foreach (KeyValuePair<DateTime, string> item in Logs)
            {
                if (item.Key >= dt)
                {
                    logs.Add(item.Value);
                }
            }
            return logs;
        }
        public void CorrectLine(string dateAfter)
        {
            List<string> logs = ShowByDate(ParseDate(dateAfter));
            int counter = 0;
            foreach (string item in logs)
            {
                Console.WriteLine($"{counter++}. {item}");
            }
            Console.WriteLine("Choose index of error to change: ");
            int.TryParse(Console.ReadLine(), out int index);
            Console.WriteLine("Input the change");
            string change = Console.ReadLine();
            DateTime dt = DateTime.Now;
            foreach (string item in Logs.Values.ToList())
            {
                if (item == logs[index])
                {
                    DateTime key = Logs.KeyByValue(item);
                    Logs.RenameKey(key, dt);
                    Logs[dt] = change;
                }
            }
        }
        public void Close()
        {
            Logs.Clear();
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (KeyValuePair<DateTime, string> item in Logs)
                sb.Append($"{item.Key} : {item.Value}");
            return sb.ToString();
        }
    }
}