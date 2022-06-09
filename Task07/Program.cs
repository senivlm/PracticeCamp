using System;

namespace Task07
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
        //unuseful data from Task02 removed
        //Answer to theoretical question: The block 'finally' may not be executed
        //in case the program has got into an infinite loop 
        //or the program has ended with RunTimException 
        //or the program has ended unexpectedly

        static void Main(string[] args)
        {
            Storage storage = new Storage("log.txt");
           
            storage.ShowInfo();
            Console.WriteLine(storage.ShowLogs());
            Console.WriteLine(storage);
            storage.CorrectLine("2021-03-03");
            Console.WriteLine(storage.ShowLogs());

        }
    }
}
