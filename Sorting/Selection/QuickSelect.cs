using Sorting.ComparisonBased;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorting.Selection;
public class QuickSelect
{
    /// <summary>
    /// Selects the k-th smallest element from the list (O(n) with quickselect)
    /// </summary>
    /// <typeparam name="T">returned element, throw exception if (k < 0 or k > list.Length)</typeparam>
    /// <param name="list">list to filter</param>
    /// <param name="k">largest indicator, i.e 1 for smallest, 2 for second smallest, etc.</param>
    /// <returns>element representing the k-th smallest</returns>
    public static T Select<T>(T[] list, int start, int end, int k) where T : IComparable<T>
    {
        // the k-th smallest will be at index k in the sorted array
        // partition until we hit this

        if (k < start || k > end) throw new ArgumentOutOfRangeException($"k:{k} is out of the bounds of the array with size: {list.Length}!");

        // pick a random pivot and partition
        int pivIndex = Random.Shared.Next(start, end+1);
        T tmp = list[pivIndex];
        list[pivIndex] = list[end];
        list[end] = tmp;

        int actualIndex = QuickSort.Partition(list, start, end);

        if (actualIndex == k) return list[actualIndex];

        if (k > actualIndex)
        {
            // recurse on right
            return Select(list, actualIndex + 1, end, k);
        } 
        else
        {
            // recurse on left
            return Select(list, start, actualIndex - 1,k);
        }
    }

}
