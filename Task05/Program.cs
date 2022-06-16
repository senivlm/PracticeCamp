using System;
using System.IO;
using System.Diagnostics;

namespace Task05
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //I also added version of merge sort from the internet changed a bit for comparison 
                //Method ToString() is used withoud StringBuilder here, as it's inefficient for small arrays
                //For universal tasks I would use StringBuilder
                Vector vect = new Vector(10);
                vect.RandomInitialization();
                vect.HeapSort();
                Console.WriteLine(vect);
                Vector vector = new Vector("../../../file.txt", "../../../part1.txt", "../../../part2.txt");
                vector.MergeSort("../../../part1.txt", "../../../part2.txt", "../../../result.txt");

                Stopwatch timer = new Stopwatch();

                Vector vectMergeCheck = new Vector(100);
                vectMergeCheck.RandomInitialization();
                timer.Start();
                vectMergeCheck.MergeSort();
                timer.Stop();
                Console.WriteLine(vectMergeCheck);
                Console.WriteLine($"Time taken for merge sort: {timer.Elapsed.TotalMilliseconds}");

                timer.Reset();
                vectMergeCheck.RandomInitialization();
                timer.Start();
                vectMergeCheck.MergeSortOptimal();
                timer.Stop();
                Console.WriteLine(vectMergeCheck);
                Console.WriteLine($"Time taken for optimal merge sort: {timer.Elapsed.TotalMilliseconds}");
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine($"File {e.FileName} not found");
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }
    }
}
