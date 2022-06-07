using System;

namespace Task04
{// Ваш номер 30.
    Швидке сортування вдало реалізували. Молодець.
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
            this.arr = arr;
        }
        public Vector(int n)
        {
            arr = new int[n];
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
        private int GetIndexOfPivot(in int left, in int right, Type sortType)
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
        public void QuickSort(Type type = Type.Middle)
        {
            GetSortedArray(0, arr.Length - 1, type);
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
    }
}
