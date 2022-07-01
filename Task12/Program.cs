using System;

namespace Task12
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Storage storage = new Storage(@"..\..\..\1\logger.txt", @"..\..\..\1\file.txt");
                Console.WriteLine(storage);
                Console.WriteLine(storage.FindProduct(x => x.Product.Name == "Eggs"));
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine("No data in file given");
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
