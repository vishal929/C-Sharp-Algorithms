using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorting.ComparisonBased;
public class QuickSort
{

    public static void SortIterative<T>(T[] list) where T:IComparable<T>
    {
        Stack<(int start, int end)> s = new();

        s.Push((0, list.Length-1));

        while (s.Count > 0)
        {
            (int start, int end) = s.Pop();
            if (start >= end) continue;

            int pivotIdx = Random.Shared.Next(start, end+1);
            // swap pivot with end
            T tmp = list[pivotIdx];
            list[pivotIdx] = list[end];
            list[end] = tmp;

            int pivotTruePos = Partition(list, start, end);

            // recurse on the smaller subarray first
            if ((pivotTruePos-1-start) < (end - pivotTruePos - 1))
            {
                s.Push((start, pivotTruePos - 1));
                s.Push((pivotTruePos + 1, end));
            } else
            {
                s.Push((pivotTruePos + 1, end));
                s.Push((start, pivotTruePos - 1));
            }
        }

    }

    public static void SortRecursive<T>(T[] list) where T: IComparable<T>
    {
        SortRecursiveDriver<T>(list, 0, list.Length-1);   
    }

    private static void SortRecursiveDriver<T>(T[] list, int start, int end) where T: IComparable<T>
    {
        if (start >= end) return;

        int pivotIdx = Random.Shared.Next(start, end+1);
        // swap pivot with end
        T tmp = list[pivotIdx];
        list[pivotIdx] = list[end];
        list[end] = tmp;

        int pivotTrueIndex = Partition<T>(list, start, end);

        // recurse on the smaller subarray first
        if ((pivotTrueIndex-1 - start) < (end - pivotTrueIndex - 1))
        {
            SortRecursiveDriver<T>(list, start, pivotTrueIndex-1);
            SortRecursiveDriver<T>(list, pivotTrueIndex + 1, end);
        } 
        else
        {
            SortRecursiveDriver<T>(list, pivotTrueIndex + 1, end);
            SortRecursiveDriver<T>(list, start, pivotTrueIndex-1);
        }
    }

    /// <summary>
    /// Partitions an array in place based on a pivot
    /// </summary>
    /// <typeparam name="T">element of icomparable</typeparam>
    /// <param name="list">list which is to be partitioned around the pivot</param>
    /// <param name="start">starting index to use for partitioning subarray list</param>
    /// <param name="end">ending index (inclusive) to use for partitioning subarray list</param>
    /// <returns>integer representing index of sorted element (pivots correct position in sorted array)</returns>
    public static int Partition<T>(T[] list, int start, int end) where T : IComparable<T>
    {

        if (start == end) return start;
        //todo partition is pretty slow for some reason
        T pivotVal = list[end];

        // partition elements so that left are all elements less than the pivot  
        int low=start;
        int high = end-1;

        while (low <= high)
        {
            while (low < end && list[low].CompareTo(pivotVal) < 0) low++;
            while (high>=start && list[high].CompareTo(pivotVal) >= 0 ) high--;

            if (low <= high)
            {
                //swap high with low and advance
                T tmpVal = list[low];
                list[low] = list[high];
                list[high] = tmpVal;
                high--;
                low++;
            }
        }

        // swap pivot with low
        T tmp = list[low];
        list[low] = list[end];
        list[end] = tmp;

        return low;
    } 
}
