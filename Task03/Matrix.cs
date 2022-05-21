using System.Text;

namespace Task03
{
    public enum Direction
    {
        Right,
        Down
    };
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
                if (i >= 0 && j >= 0 && i < Rows && j < Columns)
                    return matrix[i, j];
                else
                    return 0;
            }
            set
            {
                matrix[i, j] = value;
            }
        }
        private void GetDiagonalRightOrder()
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
        private void GetDiagonalDownOrder()
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
                        if ((i % 2) == 0)
                            matrix[j, rows - 1 - j + i] = count++;
                        else
                            matrix[rows - 1 - j + i, j] = count++;
                    }
                }
            }
        }
        public void GetDiagonalSnake(in Direction direction)
        {
            if (rows == columns)
            {
                if (direction == Direction.Right)
                {
                    GetDiagonalRightOrder();
                }
                else if(direction == Direction.Down)
                {
                    GetDiagonalDownOrder();
                }
            }
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
