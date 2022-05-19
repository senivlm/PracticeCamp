using System;
using System.Collections.Generic;

namespace hw_entry
{
    public enum Category
    {
        HighSort,
        Sort1,
        Sort2
    }

    public enum Species
    {
        mutton, 
        veal, 
        pork, 
        chicken
    }
    class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random();
            List<Buy> buy = new List<Buy>();

            try
            {
                buy = new List<Buy>()
            {
                new Buy(new Product("Milk", 25  , random.NextDouble(1, 5)), 3),
                new Buy(new Meat(Category.HighSort, Species.chicken, "Eggs", 1.5m, random.NextDouble(1.0, 5.0)), 5),
                new Buy(new DairyProducts(new DateTime(2023,1,1), "Joghurt", 16, random.NextDouble(1.0, 5.0))),
                new Buy(new Product("Cheese", 50, random.NextDouble(1.0, 5.0))),
                new Buy(new Product("Sausage", 25, random.NextDouble(1.0, 5.0)), 10)
            };
                Check.ShowInfo(buy);
            }
            catch(ProductException exp)
            {
                Console.WriteLine(exp.Message);
            }
            catch(Exception exp)
            {
                Console.WriteLine(exp.Message);
            }

            Storage storage = new Storage(buy);
            //storage.CreateDialogue();
            storage.InitializeData("Tea", 15, 100, 1);
           
            foreach (var item in storage.FindMeat())
            {
                Console.WriteLine(item);
            }
            Console.WriteLine(storage);
        }
    }
}
