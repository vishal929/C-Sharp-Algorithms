using Sorting.ComparisonBased;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace Sorting.Tests.ComparisonBased;

public class InsertionSortTest
(ITestOutputHelper helper){


    [Fact]
    public void TestSort()
    {
        // create an array of size 10000
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

        long libSortMs = watch.ElapsedMilliseconds;

        watch.Restart();
        InsertionSort.Sort(test);
        watch.Stop();

        helper.WriteLine($"insertion sort took {watch.ElapsedMilliseconds} ms while library sort took {libSortMs}");

        Assert.Equal(testCopy, test);
    }
}
