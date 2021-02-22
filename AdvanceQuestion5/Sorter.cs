using System;
using System.Collections.Generic;
using System.Text;

namespace AdvanceQuestion5
{
    public static class Sorter
    {

        public static T[] Merge<T>(T[] left, T[] right)
            where T : IComparable
        {
            T[] sum = new T[left.Length + right.Length];
            int RIndexer = 0;
            int LIndexer = 0;
            int sumIndex = 0;

            while (LIndexer < left.Length)
            {
                T input = left[LIndexer];
                for (int x = RIndexer; x < right.Length; x++)
                {
                    if (input.CompareTo(right[x]) > 0)
                    {
                        input = right[RIndexer];
                        x = right.Length;
                    }
                }

                if (input.CompareTo(left[LIndexer]) == 0)
                {
                    LIndexer++;
                }
                else
                {
                    RIndexer++;
                }

                sum[sumIndex] = input;
                sumIndex++;

            }
            for (int i = RIndexer; i < right.Length; i++)
            {
                sum[sumIndex] = right[i];
                sumIndex++;
            }
            return sum;
        }

        public static T[] MergeSort<T>(T[] nums)
            where T : IComparable
        {
            if (nums.Length < 2)
            {
                return nums;
            }
            int size = nums.Length / 2;
            T[] left = new T[size];
            T[] right = new T[size];

            if (nums.Length % 2 == 1)
            {
                left = new T[size + 1];
            }
            int indexer = 0;
            for (int i = 0; i < left.Length; i++)
            {
                left[i] = nums[indexer];
                indexer++;
            }
            for (int i = 0; i < right.Length; i++)
            {
                right[i] = nums[indexer];
                indexer++;
            }

            return Merge(MergeSort(left), MergeSort(right));
        }
    }
}
