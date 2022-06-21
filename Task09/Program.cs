using System;
using System.IO;

namespace Task09
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                using StreamWriter streamWriter = new StreamWriter(@"..\..\..\result.txt", false);

                Menu menu = new Menu(@"..\..\..\Menu.txt");
                Console.WriteLine(menu);

                PriceList priceList = new PriceList(@"..\..\..\Prices.txt");
                Console.WriteLine(priceList);

                Course course = new Course(@"..\..\..\Course.txt", 
                    (Currency)Enum.Parse(typeof(Currency), Console.ReadLine()));
                Console.WriteLine(course);

                if (!MenuService.TryGetMenuPrice(menu, priceList, course, out decimal menuPrice))
                    Console.WriteLine("Invalid data!");

                streamWriter.WriteLine($"Total: {course.currency} {menuPrice}");
                streamWriter.WriteLine("\nTotal costs for each dish: ");
                streamWriter.WriteLine(MenuService.GetDishes(menu, priceList, course, out decimal menuPrice1));
                streamWriter.WriteLine("\nTotal costs for each product: ");
                streamWriter.WriteLine(MenuService.PrintProductCosts(menu, priceList, course));

                streamWriter.Close(); // not in finally { }, as creation of streamWriter is in try-block
            }
            catch (FileNotFoundException exp)
            {
                Console.WriteLine(exp.FileName);
            }
            catch (IOException)
            {
                Console.WriteLine("Problem with files");
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Invalid data while parsing");
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("Invalid data in file given");
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp.Message);
            }
        }
    }
}
