using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Task11
{
    class MyList<T>
    {
        private readonly List<T> list;

        public List<T> List => list;

        public MyList()
        {
            list = new List<T>();
        }
        public MyList(IEnumerable<T> collection) : this()
        {
            list = (List<T>)collection ?? throw new IndexOutOfRangeException();
        }
        public MyList(int capacity) : this()
        {
            if (capacity < 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            list.Capacity = capacity;
        }
        public T this[int index]
        {
            get
            {
                if (index >= 0 && index < Count())
                    return list[index];
                throw new IndexOutOfRangeException();
            }
            set
            {
                if (index >= 0 && index < Count())
                    list[index] = value;
                else throw new IndexOutOfRangeException();
            }
        }
        public int Count()
        {
            return list.Count();
        }
        public int Capacity()
        {
            return list.Capacity;
        }
        public void Add(T item)
        {
            list.Add(item);
        }
        public void AddRange(IEnumerable<T> collection)
        {
            list.AddRange(collection ?? throw new IndexOutOfRangeException());
        }
        public ReadOnlyCollection<T> AsReadOnly()
        {
            return list.AsReadOnly();
        }
        public int BinarySearch(int index, int count, T item, IComparer<T> comparer)
        {
            if (comparer is null)
            {
                throw new InvalidOperationException();
            }
            return list.BinarySearch(index, count, item, comparer);
        }
        public int BinarySearch(T item)
        {
            return list.BinarySearch(item);
        }
        public int BinarySearch(T item, IComparer<T> comparer)
        {
            if(comparer is null)
            {
                throw new InvalidOperationException();
            }
            return list.BinarySearch(item, comparer);
        }
        public void Clear()
        {
            list.Clear();
        }
        public bool Contains(T item)
        {
            return list.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            if(array is null)
            {
                throw new ArgumentNullException();
            }
            else if(arrayIndex < 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            else if(list.Count() > array.Length - arrayIndex)
            {
                throw new ArgumentException();
            }
            list.CopyTo(array, arrayIndex);
        }
        public void CopyTo(T[] array)
        {
            if (array is null)
            {
                throw new ArgumentNullException();
            }
            else if (list.Count() > array.Length)
            {
                throw new ArgumentException();
            }
            list.CopyTo(array);
        }
        public void CopyTo(int index, T[] array, int arrayIndex, int count)
        {
            if (array is null)
            {
                throw new ArgumentNullException();
            }
            else if (arrayIndex < 0 || index < 0 || count < 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            list.CopyTo(index, array, arrayIndex, count);
        }
        private IEnumerator<T> GetEnumerator()
        {
            return list.GetEnumerator();
        }
        public void Sort()
        {
            list.Sort();
        }
        public int IndexOf(T item)
        {
            return list.IndexOf(item);
        }
        public void Insert(int index, T item)
        {
            list.Insert(index, item);
        }
        public bool Remove(T item)
        {
            return list.Remove(item);
        }
        public void RemoveAt(int index)
        {
            list.RemoveAt(index);
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (T item in list)
            {
                sb.Append(item + " ");
            }
            return sb.ToString();
        }
    }
}


