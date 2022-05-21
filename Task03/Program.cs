using System;

namespace Task03
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Vector arr = new Vector(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 });
                Vector checkPalindrome = new Vector(new int[] { 1, 2, 3, 4, 3, 2, 1 });
                Vector checkPalindromeEven = new Vector(new int[] { 1, 2, 3, 4, 4, 3, 2, 1 });
                Vector checkSubString = new Vector(new int[] { 1, 1, 1, 1, 1, 5, 6, 7, 8, 2, 2, 2, 5, 5, 5, 5 });
                Vector checkSubStringEmpty = new Vector(new int[] { });
                arr.Reverse();
                Console.WriteLine(arr);
                arr.ReverseStandart();
                Console.WriteLine(arr);
                Console.WriteLine(checkSubString.GetMaxSubString());
                Console.WriteLine(checkSubStringEmpty.GetMaxSubString());
                Console.WriteLine(checkPalindrome.IsPalindrome());
                Console.WriteLine(checkPalindromeEven.IsPalindrome());
                Matrix matrix = new Matrix(5, 5);
                Console.WriteLine("Input your direcation (Right/Down):");
                Enum.TryParse(Console.ReadLine().Trim(), out Direction direction);
                matrix.GetDiagonalSnake(direction);
                Console.WriteLine(matrix);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
           
        }
    }
}
