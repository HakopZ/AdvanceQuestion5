using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace AdvanceQuestion5
{
    public class Heap<T> where T : IComparable<T>
    {
        private T[] heap;
        private IComparer<T> comparer;
        public int Count { get; private set; }
        public int Capacity => heap.Length;
        public Heap(IComparer<T> compare = null, int capacity = 10)
        {
            compare = compare ?? Comparer<T>.Default;
            heap = new T[capacity];
        }
        private int Left(int i)
        {
            return (i * 2) + 1;
        }
        private int Right(int i)
        {
            return (i * 2) + 2;
        }
        private int Parent(int i)
        {
            return (i - 1) / 2;
        }
        public void Insert(T value)
        {
            if (Count == Capacity)
            {
                Resize(Capacity * 2);
            }
            heap[Count] = value;
            Count++;
            HeapifyUp(Count - 1);
        }
        private void HeapifyUp(int index)
        {
            int parent = Parent(index);

            if (index == 0 || comparer.Compare(heap[index], heap[parent]) <= 0)
            {
                return;
            }
            Swap(index, parent);
            HeapifyUp(parent);
        }

        public T Pop()
        {
            if (Count == 0)
            {
                throw new Exception("Heap is empty");
            }
            var value = heap[0];
            heap[0] = heap[Count - 1];
            Count--;
            if (Count == Capacity / 4)
            {
                Resize(Capacity / 2);
            }
            HeapifyDown(0);
            return value;
        }

        private void HeapifyDown(int index)
        {
            int child = index;
            int left = Left(index);
            int right = Right(index);

            if (left < Count && comparer.Compare(heap[child], heap[left]) <= 0)
            {
                child = left;
            }

            if (right < Count && comparer.Compare(heap[child], heap[right]) <= 0)
            {
                child = right;
            }

            if (child == index)
            {
                return;
            }

            Swap(index, child);
            HeapifyDown(child);
        }
        private void Swap(int index, int parent)
        {
            T temp = heap[index];
            heap[index] = heap[parent];
            heap[parent] = temp;
        }
        private void Resize(int size)
        {
            T[] temp = new T[size];
            for (int i = 0; i < Count; i++)
            {
                temp[i] = heap[i];
            }

            heap = temp;
        }

    }
}
