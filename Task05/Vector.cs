using System;
using System.IO;
using System.Linq;

namespace Task05
{
    enum Type
    {
        First,
        Last,
        Middle
    }
    class Vector
    {
        readonly int[] arr;
        public int this[int index]
        {
            get
            {
                if (index >= 0 && index < arr.Length)
                {
                    return arr[index];
                }
                else
                {
                    throw new Exception("Index out of range array");
                }
            }
            set
            {
                arr[index] = value;
            }
        }
        public Vector()
        { }
        public Vector(int[] arr)
        {
            this.arr = arr ?? throw new ArgumentNullException("No data in array given");
        }
        public Vector(int n)
        {
            try
            {
                arr = new int[n];
            }
            catch (OverflowException)
            {
                throw new ArgumentException("Value for size of array is incorrect!");
            }
        }
        public Vector(in string fileName)
        {
            arr = GetParsed(fileName);
        }
        public Vector(in string fileGiven, in string lhs, in string rhs)
        {
            StreamWriter writer = new StreamWriter(lhs);
            WriteToFile(writer, GetParsed(fileGiven), 0, GetParsed(fileGiven).Length / 2);
            writer.Close();
            writer = new StreamWriter(rhs);
            WriteToFile(writer, 
                GetParsed(fileGiven), 
                GetParsed(fileGiven).Length / 2, 
                GetParsed(fileGiven).Length);
            writer.Close();
            arr = null;
        }
        public void RandomInitialization(int a, int b)
        {
            Random random = new Random();
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = random.Next(a, b);
            }
        }
        public void RandomInitialization()
        {
            Random random = new Random();
            int x;
            for (int i = 0; i < arr.Length; i++)
            {
                while (arr[i] == 0)
                {
                    x = random.Next(1, arr.Length + 1);
                    bool isExist = false;
                    for (int j = 0; j < i; j++)
                    {
                        if (x == arr[j])
                        {
                            isExist = true;
                            break;
                        }
                    }
                    if (!isExist)
                    {
                        arr[i] = x;
                        break;
                    }
                }
            }
        }
        public void QuickSort(Type type = Type.Middle)
        {
            GetSortedArray(0, arr.Length - 1, type);
        }
        public void HeapSort()
        {
            for (int i = arr.Length / 2 - 1; i >= 0; i--)
                GetSubTree(arr.Length, i);

            for (int i = arr.Length - 1; i > 0; i--)
            {
                Swap(ref arr[0], ref arr[i]);
                GetSubTree(i, 0);
            }
        }
        public void MergeSort(in string lhs, in string rhs, in string sortedFile)
        {
            SaveSorted(lhs);
            SaveSorted(rhs);

            string[] file1 = File.ReadAllLines(lhs);
            string[] file2 = File.ReadAllLines(rhs);
            string[] result = file1.Concat(file2).ToArray();
            int[] res = new int[result.Length];
            for (int j = 0; j < result.Length; j++)
            {
                res[j] = int.Parse(result[j]);
            }
            Vector newVect = new Vector(res);
            newVect.MergeSort();
            newVect.Save(sortedFile);
        }
        public void MergeSort()
        {
            MergeSortRange(0, GetLength() - 1);
        }
        public void MergeSortOptimal()
        {
            MergeSortOptimal(0, GetLength() - 1);
        }
        private void GetSubTree(in int n, in int subRoot)
        {
            int rootNode = subRoot; 
            int left = 2 * subRoot + 1; 
            int right = 2 * subRoot + 2; 

            if (left < n && arr[left] > arr[rootNode])
                rootNode = left;

            if (right < n && arr[right] > arr[rootNode])
                rootNode = right;

            if (rootNode != subRoot)
            {
                Swap(ref arr[subRoot], ref arr[rootNode]);

                GetSubTree(n, rootNode);
            }
        }
        private int GetIndexOfPivot(in int left, in int right, in Type sortType)
        {
            return sortType switch
            {
                Type.First => left,
                Type.Last => right,
                Type.Middle => left + ((right - left + 1) / 2),
                _ => 0,
            };
        }
        private void Swap<T>(ref T left, ref T right)
        {
            T tmp = left;
            left = right;
            right = tmp;
        }
        private void GetSortedArray(in int left, in int right, in Type sortType)
        {
            if (left >= right)
                return;
            int i = left;
            int j = right;
            int pivot = arr[GetIndexOfPivot(left, right, sortType)];

            while (i <= j)
            {
                while (arr[i] < pivot)
                {
                    i++;
                }
                while (arr[j] > pivot)
                {
                    j--;
                }
                if (i <= j)
                {
                    Swap(ref arr[i++], ref arr[j--]);
                }
            }
            GetSortedArray(left, j, sortType);
            GetSortedArray(i, right, sortType);
        }
        private int GetLength()
        {
            return arr.Length;
        }
        private int[] GetParsed(in string fileGiven)
        {
            string[] lines = File.ReadAllLines(fileGiven);
            if(lines.Length == 0)
            {
                throw new ArgumentNullException("No objects in file given");
            }
            int[] arrParsed = new int[lines.Length];

            for (int i = 0; i < lines.Length; i++)
            {
                if (!int.TryParse(lines[i], out arrParsed[i]))
                {
                    throw new ArgumentException("Data in array cannot be parsed");
                }
                else if (lines[i] == null)
                {
                    throw new ArgumentNullException("Problems in file");
                }
            }
            return arrParsed;
        }
        private void WriteToFile(in StreamWriter writer, 
            in int[] arrTo, in int start, in int end)
        {
            for (int i = start; i < end; i++)
            {
                writer.WriteLine(arrTo[i]);
            }
        }
        private void SaveSorted(in string fileName)
        {
            Vector tmp = new Vector(fileName);
            tmp.MergeSort();
            tmp.Save(fileName);
        }       
        private void Save(in string file) 
        {
            StreamWriter writer = new StreamWriter(file);
            WriteToFile(writer, arr, 0, GetLength());
            writer.Close();
        }
        private void Merge(in int lhs, in int middle, in int rhs) 
        {
            int i = lhs;
            int j = middle + 1;
            int[] temp = new int[rhs - lhs + 1];
            int k = 0;
            while (i <= middle && j <= rhs)
            {
                if (arr[i] < arr[j])
                {
                    temp[k++] = arr[i++];
                }
                else
                {
                    temp[k++] = arr[j++];
                }
            }
            if (i > middle)
            {
                while (j <= rhs)
                {
                    temp[k++] = arr[j++];
                }
            }
            else
            {
                while (i <= middle)
                {
                    temp[k++] = arr[i++];
                }
            }
            for (int n = 0; n < temp.Length; n++)
            {
                arr[n + lhs] = temp[n];
            }
        }
        private void MergeSortRange(in int start, in int end) 
        {
            if (end - start <= 1) 
            {
                if (end != start && arr[end] < arr[start]) 
                {
                    Swap(ref arr[end], ref arr[start]);
                }
                return;
            }
            int middle = (end + start) / 2;
            MergeSortRange(start, middle);
            MergeSortRange(middle + 1, end);
            Merge(start, middle, end);
        }
        private void MergeSortOptimal(in int left, in int right)
        {
            if (left < right)
            {
                int middle = left + (right - left) / 2;
                MergeSortOptimal(left, middle);
                MergeSortOptimal(middle + 1, right);
                MergeOptimal(left, middle, right);
            }
        }
        private void MergeOptimal(in int left, in int middle, in int right)
        {
            int[] leftTempArray = new int[middle - left + 1];
            int[] rightTempArray = new int[right - middle];
            int i, j;
            for (i = 0; i < middle - left + 1; ++i)
                leftTempArray[i] = arr[left + i];
            for (j = 0; j < right - middle; ++j)
                rightTempArray[j] = arr[middle + 1 + j];
            i = 0;
            j = 0;
            int k = left;
            while (i < middle - left + 1 && j < right - middle)
            {
                if (leftTempArray[i] <= rightTempArray[j])
                {
                    arr[k++] = leftTempArray[i++];
                }
                else
                {
                    arr[k++] = rightTempArray[j++];
                }
            }
            while (i < middle - left + 1)
            {
                arr[k++] = leftTempArray[i++];
            }
            while (j < right - middle)
            {
                arr[k++] = rightTempArray[j++];
            }
        }
        public override string ToString()
        {
            string str = "";
            for (int i = 0; i < arr.Length; i++)
            {
                str += arr[i] + " ";
            }
            return str;
        }
        public override bool Equals(object obj)
        {
            return obj is Vector vector &&
                   arr.SequenceEqual(vector.arr);
        }
        public override int GetHashCode()
        {
            return arr.GetHashCode();
        }
        public static bool operator ==(Vector left, Vector right)
        {
            return left.Equals(right);
        }
        public static bool operator !=(Vector left, Vector right)
        {
            return !(left == right);
        }
    }
}
