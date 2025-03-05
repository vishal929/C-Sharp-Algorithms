using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorting.ComparisonBased;

/// <summary>
/// Repeatedly choose the smallest/largest element from the array and put it in its position
/// </summary>
/// <remarks>n + n-1 + n-2 + .... -> (n)(n+1)/2 which is O(n^2)</remarks>
public class SelectionSort
{

    /// <summary>
    /// Sorts in place using selection sort algorithm
    /// </summary>
    /// <param name="list">list of comparables to sort</param>
    public static void Sort<T>(T[] list) where T:IComparable<T>
    {
        for (int i = 0; i < list.Length - 1; i++)
        {
            int smallestIndex = i;
            for (int j = i+1; j < list.Length; j++)
            {
                if (list[j].CompareTo(list[smallestIndex])<0)
                {
                    smallestIndex = j;
                }
            }
            //swap
            T tmp = list[i];
            list[i] = list[smallestIndex];
            list[smallestIndex] = tmp;
        }
    }


}
