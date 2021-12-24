using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MelonManager.Utils
{
    public class ThreadSafeList<T> : IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable, IReadOnlyList<T>, IReadOnlyCollection<T>
    {
        private readonly List<T> internalList = new List<T>();

        public T this[int index]
        {
            get
            {
                lock (internalList)
                    return internalList[index];
            }
            set
            {
                lock (internalList)
                    internalList[index] = value;
            }
        }

        public int Count 
        {
            get
            {
                lock (internalList)
                    return internalList.Count;
            }
        }

        public bool IsReadOnly => false;

        public void Add(T item)
        {
            lock (internalList)
                internalList.Add(item);
        }

        public void Clear()
        {
            lock (internalList)
                internalList.Clear();
        }

        public bool Contains(T item)
        {
            lock (internalList)
                return internalList.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            lock (internalList)
                internalList.CopyTo(array, arrayIndex);
        }

        public IEnumerator<T> GetEnumerator()
        {
            lock (internalList)
                return internalList.GetEnumerator();
        }

        public int IndexOf(T item)
        {
            lock (internalList)
                return internalList.IndexOf(item);
        }

        public void Insert(int index, T item)
        {
            lock (internalList)
                internalList.Insert(index, item);
        }

        public bool Remove(T item)
        {
            lock (internalList)
                return internalList.Remove(item);
        }

        public void RemoveAt(int index)
        {
            lock (internalList)
                internalList.RemoveAt(index);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            lock (internalList)
                return internalList.GetEnumerator();
        }
    }
}
