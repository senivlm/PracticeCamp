using System;

namespace Task05
{
    class Program
    {
        static void Main(string[] args)
        {
            Vector vect = new Vector(10);
            vect.RandomInitialization();
            vect.HeapSort();
            Console.WriteLine(vect);
        }
    }
}
