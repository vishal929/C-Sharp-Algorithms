using Sorting.ComparisonBased;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace Sorting.Tests.ComparisonBased;

public class MergeSortTest (ITestOutputHelper helper)
{
    [Fact]
    public void TestSort()
    {
        // create an array of size 1000
        int[] test = new int[TestGlobalSize.testArraySize];

        int[] testCopy = new int[TestGlobalSize.testArraySize];

        for (int i = 0; i < test.Length; i++)
        {
            test[i] = Random.Shared.Next();
        }

        // copy the array to testCopy
        Array.Copy(test, testCopy, test.Length);

        // sort both and assert that the result is the same
        var watch = new Stopwatch();
        watch.Start();
        Array.Sort(testCopy);
        watch.Stop();

        long libMs = watch.ElapsedMilliseconds;

        watch.Restart();
        int[] sorted = MergeSort.SortRecursive(test);
        watch.Stop();

        helper.WriteLine($"Merge sort took {watch.ElapsedMilliseconds} ms while lib sort took {libMs}");

        Assert.Equal(testCopy, sorted);
    }
}
