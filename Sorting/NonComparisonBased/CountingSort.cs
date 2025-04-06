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
    public static int[] Sort(int[] arr)
    {
        int max = arr.Max();
        int[] counts = new int[max];
        int numZero = 0;
        for (int i = 0; i < arr.Length; i++)
        {
            if (arr[i] == 0)
            {
                numZero++;
            } else
            {
                counts[arr[i] - 1]++;
            }
        }

        int[] ret = new int[arr.Length];
        int num = 0;

        for (int j = 0; j < numZero; j++)
        {
            ret[num] = 0;
            num++;
        }

        for (int j = 0; j < counts.Length; j++)
        {
            for (int k = 0; k < counts[j]; k++)
            {
                ret[num] = j+1;
                num++;
            }
        }
        return ret;
    }

}
