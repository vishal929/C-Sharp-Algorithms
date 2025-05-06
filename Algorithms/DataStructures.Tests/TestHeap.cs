using DataStructures.Heaps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.Tests;
public class TestHeap
{
    [Fact] 
    public void TestHeapify()
    {
        int[] test = new int[350];
        for (int i = 0; i < test.Length; i++)
        {
            test[i] = Random.Shared.Next(-2000,2000);
        }
        int[] testCopy = new int[test.Length];
        Array.Copy(test, testCopy, test.Length);

        var heap = new BinaryHeap<int>(true, test.ToList());

        // sort the copy
        Array.Sort(testCopy);

        for (int i = testCopy.Length - 1; i >= 0; i--)
        {
            Assert.Equal(heap.Extract(), testCopy[i]);
        }

        // now make it a min heap
        heap = new BinaryHeap<int>(false, test.ToList());

        for (int i = 0; i < testCopy.Length; i++)
        {
            Assert.Equal(heap.Extract(), testCopy[i]);
        }

    }

}
