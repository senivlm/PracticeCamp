using System;

namespace Task11
{
    class Program
    {// Де інша частина завдання?
        static void Main(string[] args)
        {// не показовий  1 тест.
            MyList<object> list = new MyList<object>();

            list.Add(1);
            list.Add(1.57);
            list.Add("string");
            list.Add('c');
            list.Add(true);

            Console.WriteLine(list);
        }
    }
}
