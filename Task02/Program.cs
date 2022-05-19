using System;

namespace Task02
{
    class Program
    {
        static void Main(string[] args)
        {
            Matrix matrix = new Matrix(5, 7);
            Matrix squareMatrix = new Matrix(5, 5);

            squareMatrix.GetDiagonalSnake();
            Console.WriteLine(squareMatrix);
            squareMatrix.GetSpiralSnake();
            Console.WriteLine(squareMatrix);
            matrix.GetVerticalSnake();
            Console.WriteLine(matrix);
        }
    }
}
