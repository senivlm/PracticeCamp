using System;
using System.IO;
using System.Collections.Generic;

namespace Task06
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Sentence s = new Sentence(@"..\..\..\text.txt");
                Console.WriteLine(s.GetShortest());
                Console.WriteLine(s.GetLongest());
                s.PrintText();
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }
            try
            {
                Accounting meter = new Accounting(@"..\..\..\file.txt");
                Console.WriteLine($"Info about flat:\n{meter.GetInfoAboutFlat(36)}");
                Console.WriteLine($"Debtors: {meter.FindDebtor()}");
                Console.WriteLine($"Numbers of unused flats: {meter.FindUnused()}");

                meter.PrintDataInFile(@"..\..\..\data.csv");
                meter.PrintUtilitiesInFile(@"..\..\..\utilities.csv", 100);
                meter.PrintPeriodsInFile(@"..\..\..\periods.csv");
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}