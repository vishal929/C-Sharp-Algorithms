using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorting.ComparisonBased;
public class MergeSort
{
    /// <summary>
    /// Sorts using merge sort algorithm (not in-place)
    /// </summary>
    /// <param name="list">list of comparables to sort</param>
    public static T[] SortRecursive<T>(T[] list) where T:IComparable<T>
    {
        if (list.Length <= 1)
        {
            // sorted
            return list;
        }
        // split the list in half, sort both sides    
        T[] left = list.Take(list.Length / 2).ToArray();
        T[] right = list.Skip(left.Length).Take(list.Length - left.Length).ToArray();

        T[] leftSorted = SortRecursive(left);
        T[] rightSorted = SortRecursive(right);

        // merge them
        return Merge(leftSorted, rightSorted);
    }

    private static T[] Merge<T>(T[] left, T[] right) where T:IComparable<T>
    {
        int i = 0;
        int j = 0;
         
        T[] result = new T[left.Length + right.Length];

        while (i < left.Length && j <  right.Length)
        {
            if (left[i].CompareTo(right[j])<0)
            {
                result[i + j] = left[i];
                i++;
            }  
            else
            {
                result[i + j] = right[j];
                j++;
            }
        }

        while (i < left.Length)
        {
            result[i + j] = left[i];
            i++;
        }
        while (j < right.Length)
        {
            result[i + j] = right[j];
            j++;
        }

        return result;
    }

    
    

}
