using System;

namespace Task10
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Matrix matrix = new Matrix(5, 5);

                Console.WriteLine(matrix);

                foreach (int item in matrix)
                {
                    Console.WriteLine(item);
                }

                foreach (int item in matrix.GetHorizontalSnakeEnumerator())
                {
                    Console.WriteLine(item);
                }

                foreach (int item in matrix.GetDiagonalSnakeEnumerator(Direction.Right))
                {
                    Console.WriteLine(item);
                }

                Translator translator = new Translator(@"..\..\..\vocabulary.txt", @"..\..\..\text.txt");
                Console.WriteLine(translator.Translate());
                translator.WriteTranslationToFile(@"..\..\..\result.txt");
            }
            catch (NullReferenceException)
            {
                Console.WriteLine("Rows or columns are invalid!");
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("Empty word given");
            }
            catch(Exception exp)
            {
                Console.WriteLine(exp.Message);
            }

           
        }
    }
}
