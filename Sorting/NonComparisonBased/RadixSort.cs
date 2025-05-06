using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorting.NonComparisonBased;
public class RadixSort
{
    public static int[] Sort(int[] arr)
    {
       int[] sorted = Array.Empty<int>();
       for (int i = 0; i < 32; i++)
       {
           sorted = CountingSort.Sort(arr, 1, x => (x & (1 << i)) > 0 ? 1 : 0);
           arr = sorted;
       }

       // now each number is sorted
       return sorted;
    }
}
