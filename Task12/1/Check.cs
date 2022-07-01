using System;
using System.Collections.Generic;

namespace Task12
{
    public static class Check
    {
        public static void ShowInfo(in List<Buy> buy)
        {
            decimal sum = 0;
            foreach (Buy item in buy)
            {
                Console.WriteLine($"Product:\n{item.Product}\nAmount: {item.Amount}\nSum: {item.Sum}");
                sum += item.Sum;
            }
            Console.WriteLine($"\nTotal: {sum}");
        }
    }
}
