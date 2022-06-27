using System;

namespace Task11
{
    class Program
    {
        static void Main(string[] args)
        {
            MyList<object> list = new MyList<object>();

            list.Add(1);
            list.Add("string");
            list.Add('a');
            list.Add(1.57);
            list.Add(true);

            Console.WriteLine(list);
        }
    }
}
