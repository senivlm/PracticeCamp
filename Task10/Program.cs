using System;

namespace Task10
{
    class Program
    {
        static void Main(string[] args)
        {
            Matrix matrix = new Matrix(5, 7);
            foreach (int item in matrix)
            {
                Console.WriteLine(item);
            }
        }
    }
}
