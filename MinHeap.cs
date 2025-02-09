using System;
using System.Collections.Generic;

namespace Municipality_App
{
    public class MinHeap<T> where T : IComparable<T>
    {
        private readonly List<T> heap = new List<T>();

        public void Insert(T item)
        {
            heap.Add(item);
            HeapifyUp(heap.Count - 1);
        }

        private void HeapifyUp(int index)
        {
            while (index > 0)
            {
                int parent = (index - 1) / 2;
                if (heap[index].CompareTo(heap[parent]) >= 0)
                    break;

                (heap[index], heap[parent]) = (heap[parent], heap[index]);
                index = parent;
            }
        }
    }
}
