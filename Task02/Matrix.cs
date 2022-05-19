using System;
using System.Text;

namespace Task02
{
    public class Matrix
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
            this.rows = rows;
            this.columns = columns;
            matrix = new int[this.rows, this.columns];
        }
        public int this[int i, int j]
        {
            get
            {
                if (i >= 0 && j >= 0)
                    return matrix[i, j];
                else
                    return 0;
            }
            set
            {
                matrix[i, j] = value;
            }
        }
        public void GetVerticalSnake()
        {
            int direction = 0;
            int count = 1;
            for (int i = 0; i < matrix.GetLength(1); i++)
            {
                for (int j = 0; j < matrix.GetLength(0); j++)
                {
                    if(direction == 0)
                        matrix[j, i] = count;
                    else
                        matrix[matrix.GetLength(0) - j - 1, i] = count;
                    count++;


                }
                direction = (direction + 1) % 2;

            }
        }
        public void GetDiagonalSnake()
        {
            if(rows == columns)
            {
                int count = 1;
                for (int i = 1 - rows; i <= rows - 1; i++)
                {
                    for (int j = 0; j < rows; j++)
                    {
                        if (j - i < 0 || j - i >= rows)
                            continue;
                        else
                        {
                            if ((i % 2) != 0)
                                matrix[j, rows - 1 - j + i] = count++;
                            else
                                matrix[rows - 1 - j + i, j] = count++;
                        }
                    }
                }
                }
        }
        public void GetSpiralSnake()
        {
            int count = 1;
            for (int i = 0; i < rows / 2 + 1; i++)
            {
                for (int j = i; j < rows - i; j++) matrix[j, i] = count++;
                for (int k = i + 1; k < rows - i; k++) matrix[rows - 1 - i, k] = count++;
                for (int j = rows - i - 2; j >= i; j--) matrix[j, rows - 1 - i] = count++;
                for (int k = rows - 2 - i; k > i; k--) matrix[i, k] = count++;
            }
        }
        public static Matrix operator +(in Matrix first, in Matrix second)
        {
            if (first.rows == second.rows && first.columns == second.columns)
            {
                Matrix result = new Matrix(first.rows, second.columns);
                for (int i = 0; i < first.rows; i++)
                    for (int j = 0; j < second.columns; j++)
                        result[i, j] = first[i, j] + second[i, j];
                return result;
            }
            else
                throw new Exception("Wrong data");
        }
        public static Matrix operator -(in Matrix first, in Matrix second)
        {
            if (first.rows == second.rows && first.columns == second.columns)
            {
                Matrix result = new Matrix(first.rows, second.columns);
                for (int i = 0; i < first.rows; i++)
                    for (int j = 0; j < second.columns; j++)
                        result[i, j] = first[i, j] - second[i, j];
                return result;
            }
            else
                throw new Exception("Wrong data");
        }
        public static Matrix operator *(in Matrix first, in int number)
        {
            Matrix result = new Matrix(first.rows, first.columns);
            for (int i = 0; i < first.rows; i++)
                for (int j = 0; j < first.columns; j++)
                    result[i, j] = first[i, j] * number;
            return result;
        }
        public static Matrix operator *(in int number, in Matrix first)
        {
            Matrix result = new Matrix(first.rows, first.columns);
            for (int i = 0; i < first.rows; i++)
                for (int j = 0; j < first.columns; j++)
                    result[i, j] = first[i, j] * number;
            return result;
        }
        public static Matrix operator *(in Matrix first, in Matrix second)
        {
            if (first.columns == second.rows)
            {
                Matrix result = new Matrix(first.rows, second.columns);
                for (int i = 0; i < first.rows; i++)
                    for (int j = 0; j < second.columns; j++)
                        for (int k = 0; k < second.rows; k++)
                            result.matrix[i, j] += first.matrix[i, k] * second.matrix[k, j];
                return result;
            }
            else
                throw new Exception("Wrong data");
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
                if (rows == first.rows && columns == first.columns)
                {
                    for (int i = 0; i < rows; i++)
                        for (int j = 0; j < columns; j++)
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
        ~Matrix()
        { }
    }
}
