using System;

namespace Task03
{
    class Vector
    {
        public int[] Arr { get; set; }
        public int this[int index]
        {
            get
            {
                if (index >= 0 && index < Arr.Length)
                {
                    return Arr[index];
                }
                else
                {
                    throw new Exception("Index out of range array");
                }
            }
            set
            {
                Arr[index] = value;
            }
        }
        public Vector() 
        {   }
        public Vector(int[] arr)
        {
            Arr = arr;
        }
        public Vector(int n)
        {
            Arr = new int[n];
        }
        public void RandomInitialization(int a, int b)
        {
            Random random = new Random();
            for (int i = 0; i < Arr.Length; i++)
            {
                Arr[i] = random.Next(a, b);
            }
        }
        public void RandomInitialization()
        {
            Random random = new Random();
            int x;
            for (int i = 0; i < Arr.Length; i++)
            {
                while (Arr[i] == 0)
                {
                    x = random.Next(1, Arr.Length + 1);
                    bool isExist = false;
                    for (int j = 0; j < i; j++)
                    {
                        if (x == Arr[j])
                        {
                            isExist = true;
                            break;
                        }
                    }
                    if (!isExist)
                    {
                        Arr[i] = x;
                        break;
                    }
                }
            }
        }
        public Pair[] CalculateFreq()
        {

            Pair[] pairs = new Pair[Arr.Length];

            for (int i = 0; i < Arr.Length; i++)
            {
                pairs[i] = new Pair(0, 0);

            }
            int countDifference = 0;

            for (int i = 0; i < Arr.Length; i++)
            {
                bool isElement = false;
                for (int j = 0; j < countDifference; j++)
                {
                    if (Arr[i] == pairs[j].Number)
                    {
                        pairs[j].Freq++;
                        isElement = true;
                        break;
                    }
                }
                if (!isElement)
                {
                    pairs[countDifference].Freq++;
                    pairs[countDifference].Number = Arr[i];
                    countDifference++;
                }
            }
            Pair[] result = new Pair[countDifference];
            for (int i = 0; i < countDifference; i++)
            {
                result[i] = pairs[i];
            }
            return result;
        }
        public bool IsPalindrome()
        {
            for (int i = 0; i < Arr.Length / 2; i++)
            {
                if (Arr[i] != Arr[Arr.Length - 1 - i])
                    return false;
            }
            Array.Reverse(Arr);
            return true;
        }
        public void Reverse()
        {
            int temp;
            for (int i = 0; i < Arr.Length / 2; i++)
            {
                temp = Arr[i];
                Arr[i] = Arr[Arr.Length - 1 - i];
                Arr[Arr.Length - 1 - i] = temp;
            }
        }
        public void ReverseStandart()
        {
            Array.Reverse(Arr);
        }
        public Vector GetMaxSubString()
        {
            int begin = 0;
            int end = 1;
            int maxBegin = 0;
            int maxEnd = 1;
            while(end < Arr.Length)
            {
                if(Arr[end] != Arr[end - 1])
                {
                    if (maxEnd - maxBegin < end - begin)
                    {
                        maxBegin = begin;
                        maxEnd = end;
                    }
                    begin = end;
                }
                end++;
            }
            if (maxEnd - maxBegin < end - begin)
            {
                maxBegin = begin;
                maxEnd = end;
            }
            int resultSize = 0;
            if (Arr.Length != 0)
            {
                resultSize = maxEnd - maxBegin;
            }
            Vector result = new Vector(resultSize);

            for (int i = 0; i < result.Arr.Length; i++)
            {
                result[i] = Arr[maxBegin + i];
            }
            return result;
        }
        public override string ToString()
        {
            string str = "";
            for (int i = 0; i < Arr.Length; i++)
            {
                str += Arr[i] + " ";
            }
            return str;
        }
    }
}
