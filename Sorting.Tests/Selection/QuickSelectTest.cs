using Sorting.ComparisonBased;
using Sorting.Selection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace Sorting.Tests.ComparisonBased;

public class QuickSelectTest (ITestOutputHelper helper)
{
    [Fact]
    public void TestSelectRecursive()
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

        // choose k randomly (k-th smallest)
        int k = Random.Shared.Next(0, test.Length); 


        // sort both and assert that the result is the same
        var watch = new Stopwatch();
        watch.Start();
        int kVal = QuickSelect.Select(test, 0, test.Length - 1, k);
        watch.Stop();

        long selectMs = watch.ElapsedMilliseconds;

        watch.Restart();
        // sort the other array and assert the position values are the same 
        Array.Sort(testCopy);
        int kValTrue = testCopy[k];
        watch.Stop();

        Assert.Equal(kVal, kValTrue);

        helper.WriteLine($"quick select recursive took {selectMs} ms while sorting with library and choosing k took: {watch.ElapsedMilliseconds} ms");

    }
}
