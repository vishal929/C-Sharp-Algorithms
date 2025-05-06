using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorting.NonComparisonBased;
public class CountingSort
{
    // if we have nonnegative integers with known maximum, we can use an array to just count them and return an arranged version
    // (if we have negative ints, we could possibly shift everything here by the negative lowest num, but that may introduce overflow)
    public static int[] Sort(int[] arr, int max, Func<int,int> transform)
    {
        int[] counts = new int[max+1];
        for (int i = 0; i < arr.Length; i++)
        {
            counts[transform(arr[i])]++;
        }

        // set counts to hold elements less than or equal to i
        for (int i = 1; i <= max; i++)
        {
            counts[i] = counts[i] + counts[i - 1];
        }

        int[] ret = new int[arr.Length];
        
        for (int i = arr.Length - 1; i >= 0; i--)
        {
            ret[counts[transform(arr[i])]-1] = arr[i];
            counts[transform(arr[i])] -= 1;
        }
                

        return ret;
    }
    
}

