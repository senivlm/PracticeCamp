using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

namespace Task08
{
    class IPAdressHandler
    {
        readonly List<IPAdress> iPAdresses;
        public IPAdressHandler(List<IPAdress> iPAdresses)
        {
            this.iPAdresses = iPAdresses ?? throw new ArgumentNullException("No objects in list given");
        }
        public IPAdressHandler(string path)
        {
            iPAdresses = new List<IPAdress>();
            try
            {
                string[] adressesFile = File.ReadAllLines(path);
                foreach (string item in adressesFile)
                {
                    try
                    {
                        string[] line = item.Split(new char[] { '\t' }, StringSplitOptions.RemoveEmptyEntries);
                        iPAdresses.Add(new IPAdress(line[0], TimeSpan.Parse(line[1]), line[2].ToLower()));
                    }
                    catch (ArgumentNullException)
                    {
                        Console.WriteLine("Error with timespan parsing occured");
                    }
                    catch (ArgumentException)
                    {
                        Console.WriteLine("Error with splitting objects in file occured");
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Error with timespan format occured");
                    }
                    catch (OverflowException)
                    {
                        Console.WriteLine("Overflow");
                    }
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("File not found");
            }
            catch (IOException)
            {
                Console.WriteLine("Unknown exception with file system");
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine("No objects in list given");
            } 
        }
        //using LINQ for effective looping and grouping 
        //of repeating collections without creating
        //another methods to return collection parts 
        public Dictionary<string, int> GetAmountForIP()
        {
            return iPAdresses
                   .GroupBy(x => x.Adress)
                   .ToDictionary(x => x.Key, x => x.Count());
        }
        public Dictionary<string, string> GetMostPopularTimeForEachIP()
        {
            return iPAdresses
                   .GroupBy(x => x.Adress)
                   .ToDictionary(x => x.Key, x => x
                   .GroupBy(x => x.Time.Hours)
                   .OrderByDescending(x => x.Count())
                   .Select(x => $"{x.Key} - {(x.Key + 1 == 24 ? 00 : x.Key + 1)}")
                   .FirstOrDefault());
        }
        public Dictionary<string, string> GetMostPopularDayForEachIP()
        {
            return iPAdresses
                   .GroupBy(x => x.Adress)
                   .ToDictionary(x => x.Key, x => x
                   .GroupBy(x => x.DayOfWeek)
                   .OrderByDescending(x => x.Count())
                   .Select(x => x.Key)
                   .FirstOrDefault());
        }
        public string GetTheMostPopularDay()
        {
            return iPAdresses
                   .GroupBy(x => x.DayOfWeek)
                   .OrderByDescending(x => x.Count())
                   .Select(x => x.Key)
                   .FirstOrDefault();
        }
        public string GetTheMostPopularTime()
        {
            return iPAdresses
                   .GroupBy(x => x.Time.Hours)
                   .OrderByDescending(x => x.Count())
                   .Select(x => $"{x.Key} - {(x.Key + 1 == 24 ? 00 : x.Key + 1)}")
                   .FirstOrDefault();
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in iPAdresses)
            {
                sb.Append(item);
            }
            return sb.ToString();
        }
    }
}
