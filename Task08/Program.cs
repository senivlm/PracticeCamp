using System;
using System.Collections.Generic;

namespace Task08
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Accounting meter = new Accounting(@"..\..\..\1\file.txt");
                Console.WriteLine($"Info about flat:\n{meter.GetInfoAboutFlat(36)}");
                Console.WriteLine($"Debtors: {meter.FindDebtor()}");
                Console.WriteLine($"Numbers of unused flats: {meter.FindUnused()}");

                meter.PrintDataInFile(@"..\..\..\1\data.csv");
                meter.PrintUtilitiesInFile(@"..\..\..\1\utilities.csv", 100);
                meter.PrintPeriodsInFile(@"..\..\..\1\periods.csv");

                Accounting accounting1 = new Accounting(new List<Account>()
                {
                    new Account(21,"Q", 100, 150, new DateTime(2021,12,12)),
                    new Account(22,"Q", 100, 150, new DateTime(2021,12,12)),
                    new Account(23,"Q", 100, 150, new DateTime(2021,12,12))
                });
                Accounting accounting2 = new Accounting(new List<Account>()
                {
                    new Account(23,"Q", 100, 150, new DateTime(2021,12,12)),
                    new Account(24,"Q", 100, 150, new DateTime(2021,12,12))
                });

                Accounting sum = accounting1 + accounting2;
                Accounting substraction = accounting1 - accounting2;

                Console.WriteLine(sum);
                Console.WriteLine(substraction);

                IPAdressHandler iPHandler = new IPAdressHandler(@"..\..\..\2\data.txt");

                Console.WriteLine("Amount of visits for each IP:");
                foreach (KeyValuePair<string, int> item in iPHandler.GetAmountForIP())
                {
                    Console.WriteLine($"{item.Key}\t{item.Value} {(item.Value == 1 ? "time" : "times")}");
                }
                Console.WriteLine("The most popular one hour period for each IP:");
                foreach (KeyValuePair<string, string> item in iPHandler.GetMostPopularTimeForEachIP())
                {
                    Console.WriteLine($"{item.Key}\t{item.Value} hours");
                }
                Console.WriteLine("The most popular day for each IP:");
                foreach (KeyValuePair<string, string> item in iPHandler.GetMostPopularDayForEachIP())
                {
                    Console.WriteLine($"{item.Key}\t{item.Value}");
                }
                Console.WriteLine("The most popular one hour period:");
                Console.WriteLine(iPHandler.GetTheMostPopularTime());
                Console.WriteLine("The most popular day:");
                Console.WriteLine(iPHandler.GetTheMostPopularDay());

                Storage storage1 = new Storage(new List<Buy>()
                {
                new Buy(new DairyProducts(new DateTime(2023,1,1), "Joghurt", 16, 1), 3),
                new Buy(new Product("Cheese", 50, 1)),
                new Buy(new Product("Sausage", 25, 1), 10)
                });

                Storage storage2 = new Storage(new List<Buy>()
                {
                new Buy(new Product("Milk", 25  , 3), 3),
                new Buy(new Meat(Category.HighSort, Species.chicken, "Eggs", 1.5m, 3), 5),
                new Buy(new Product("Cheese", 50, 1)),
                new Buy(new Product("Sausage", 25, 1), 11)
                });

                Console.WriteLine(storage1.Intersect(storage2));
                Console.WriteLine(storage1.Union(storage2));
                Console.WriteLine(storage1.Except(storage2));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

           

        }
    }
}
