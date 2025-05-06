using Sorting.NonComparisonBased;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace Sorting.Tests.NonComparisonBased;
public class RadixSortTest (ITestOutputHelper helper)
{
    [Fact]
    public void TestSort()
    {
        // need hundreds of millions of integers for counting sort to basically match lib sorts performance
        int[] random = Enumerable.Range(0,30).Select(x => Random.Shared.Next(0,int.MaxValue/2)).ToArray();

        // sort with counting sort
        var watch = new Stopwatch();
        watch.Start();
        int[] randSorted = RadixSort.Sort(random);
        watch.Stop();

        helper.WriteLine($"radix sort took {watch.ElapsedMilliseconds} ms");

        // sort in place with lib sort
        watch.Restart();
        Array.Sort(random);
        watch.Stop();

        helper.WriteLine($"lib sort took {watch.ElapsedMilliseconds} ms");


        // assert equality
        Assert.True(random.SequenceEqual(randSorted));

    }

}
