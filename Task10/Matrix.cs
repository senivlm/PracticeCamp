using System.Text;
using System.Collections.Generic;
using System.Collections;

namespace Task10
{

    public enum Direction
    {
        Right,
        Down
    }
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
            if (rows > 0 && columns > 0)
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
        public IEnumerator<int> GetEnumerator()
        {
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    yield return matrix[i, j];
                }
            }
        }
        // " є проблема. Подивимось на занятті
        public IEnumerable<int> GetHorizontalSnakeEnumerator()
        {
            int direction = 0;
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (direction == 0)
                        yield return matrix[i, j];
                    else
                        yield return matrix[i, matrix.GetLength(1) - j - 1];
                }
                direction = (direction + 1) % 2;
            }
        }
        private IEnumerable<int> GetDiagonalRightOrderEnumerator()
        {
            for (int i = 1 - Rows; i <= Rows - 1; i++)
            {
                for (int j = 0; j < Rows; j++)
                {
                    if (j - i < 0 || j - i >= Rows)
                        continue;
                    else
                    {
                        if ((i % 2) != 0)
                            yield return matrix[j, Rows - 1 - j + i];
                        else
                            yield return matrix[Rows - 1 - j + i, j];
                    }
                }
            }
        }
        private IEnumerable<int> GetDiagonalDownOrderEnumerator()
        {
            for (int i = 1 - Rows; i <= Rows - 1; i++)
            {
                for (int j = 0; j < Rows; j++)
                {
                    if (j - i < 0 || j - i >= Rows)
                        continue;
                    else
                    {
                        if ((i % 2) == 0)
                            yield return matrix[j, Rows - 1 - j + i];
                        else
                            yield return matrix[Rows - 1 - j + i, j];
                    }
                }
            }
        }
        public IEnumerable<int> GetDiagonalSnakeEnumerator(in Direction direction)
        {
            if (Rows == Columns)
            {
                if (direction == Direction.Right)
                {
                    return GetDiagonalRightOrderEnumerator();
                }
                else if (direction == Direction.Down)
                {
                    return GetDiagonalDownOrderEnumerator();
                }
            }
            return GetHorizontalSnakeEnumerator(); 
            // because I don't want to use System to throw an exception or return null to save memory, so it's a funny option
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
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
        ~Matrix()
        { }
    }
}
