using System;
using System.Diagnostics;
using System.Collections.Generic;

namespace Task04
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Vector> vect = new List<Vector>();
            Stopwatch timer = new Stopwatch();
            int i = 0;
            try
            {
                foreach (var item in Enum.GetValues(typeof(Type)))
                {
                    vect.Add(new Vector(10));
                    vect[i].RandomInitialization();
                    Console.WriteLine(vect[i]);
                    timer.Start();
                    vect[i].QuickSort((Type)item);
                    timer.Stop();
                    Console.WriteLine($"Time taken for {item} pivot: {timer.Elapsed.TotalMilliseconds}");
                    timer.Reset();
                    Console.WriteLine(vect[i++]);
                }
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp.Message);
            }

        }
    }
}
