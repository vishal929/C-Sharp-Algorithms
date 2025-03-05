using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorting.ComparisonBased;

/// <summary>
/// Bubble sort just repeatedly swaps until no more swaps are needed
/// the worst case is that element is in reverse order, will take at least n-1 swaps to bring the largest to the front, n-2 for the smallest and so on ...
/// which is O(n^2)
/// </summary>
public class BubbleSort
{
    /// <summary>
    /// Sorts in place using bubble sort algorithm
    /// </summary>
    /// <param name="list">list of comparables to sort</param>
    public static void Sort<T>(T[] list) where T:IComparable<T>
    {
        int numSwaps;
        do
        {
            numSwaps = 0;
            for (int i = 0; i < list.Length - 1; i++)
            {
                if (list[i].CompareTo(list[i + 1]) > 0)
                {
                    // need to swap
                    T tmp = list[i + 1];
                    list[i + 1] = list[i];
                    list[i] = tmp;
                    numSwaps++;
                }
            }
        } while (numSwaps > 0);
    }
}
