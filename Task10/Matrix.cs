using System.Text;
using System.Collections.Generic;
using System.Collections;

namespace Task10
{
    public class Matrix : IEnumerable<int>
    {
        private int rows;
        private int columns;
        private readonly int[,] matrix;
        public int Rows
        {
            get { return rows; }
            set
            {
                if (value > 0)
                    rows = value;
            }
        }
        public int Columns
        {
            get { return columns; }
            set
            {
                if (value > 0)
                    columns = value;
            }
        }
        public Matrix(in int rows, in int columns)
        {
            Rows = rows;
            Columns = columns;
            matrix = new int[Rows, Columns];
            int count = 1;
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    matrix[i, j] = count++;
                }
            }
        }
        public int this[int i, int j]
        {
            get
            {
                if (i >= 0 && 
                    i < Rows && 
                    j >= 0 && 
                    j < Columns)
                    return matrix[i, j];
                else
                    return 0;
            }
            set
            {
                matrix[i, j] = value;
            }
        }
        public void GetHorizontalSnake()
        {
            int direction = 0;
            int count = 1;
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (direction == 0)
                        matrix[i, j] = count;
                    else
                        matrix[i, matrix.GetLength(1) - j - 1] = count;
                    count++;
                }
                direction = (direction + 1) % 2;

            }
        }
        public static bool operator ==(in Matrix first, in Matrix second)
        {
            return first.Equals(second);
        }
        public static bool operator !=(in Matrix first, in Matrix second)
        {
            return !first.Equals(second);
        }
        public override bool Equals(object obj)
        {
            if (obj != null && obj is Matrix first)
            {
                if (Rows == first.Rows && Columns == first.Columns)
                {
                    for (int i = 0; i < Rows; i++)
                        for (int j = 0; j < Columns; j++)
                            if (matrix[i, j] != first[i, j])
                                return false;
                    return true;
                }
            }
            return false;
        }
        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    sb.Append(matrix[i, j].ToString().PadRight(3));
                }
                sb.Append("\n");
            }
            return sb.ToString();
        }
        public IEnumerator<int> GetEnumerator()
        {
            int direction = 0;
            int count = 1;
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (direction == 0)
                        yield return matrix[i, j];
                    else
                        yield return matrix[i, matrix.GetLength(1) - j - 1];
                    count++;
                }
                direction = (direction + 1) % 2;

            }
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        ~Matrix()
        { }
    }
}
