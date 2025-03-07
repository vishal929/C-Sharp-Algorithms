﻿using Sorting.ComparisonBased;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace Sorting.Tests.ComparisonBased;

public class BubbleSortTest
{

    private ITestOutputHelper output { get; set; }
    public BubbleSortTest(ITestOutputHelper helper)
    {
        output = helper; 
    }

    [Fact]
    public void TestSort()
    {
        int[] test = new int[TestGlobalSize.testArraySize];

        int[] testCopy = new int[TestGlobalSize.testArraySize];

        for (int i = 0; i < test.Length; i++)
        {
            test[i] = Random.Shared.Next(int.MinValue,int.MaxValue);
        }

        // copy the array to testCopy
        Array.Copy(test, testCopy, test.Length);

        // sort both and assert that the result is the same

        // time both operations
        Stopwatch watch = new Stopwatch();
        watch.Start();
        Array.Sort(testCopy);
        watch.Stop();

        long stdSortMs = watch.ElapsedMilliseconds;

        watch.Restart();
        BubbleSort.Sort(test);
        watch.Stop();

        long ourSortMs = watch.ElapsedMilliseconds;

        output.WriteLine($"bubble sort took {ourSortMs} ms while library sort took {stdSortMs} ms");

        Assert.Equal(testCopy, test);
    }
}
