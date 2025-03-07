using Sorting.ComparisonBased;
using System.Diagnostics;
using Xunit.Abstractions;

namespace Sorting.Tests.ComparisonBased
{
    public class SelectionSortTest (ITestOutputHelper helper)
    {
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

            long libMs = watch.ElapsedMilliseconds;

            watch.Restart();
            SelectionSort.Sort(test);
            watch.Stop();

            helper.WriteLine($"Selection sort took {watch.ElapsedMilliseconds} while lib sort took {libMs}");

            Assert.Equal(testCopy, test);
        }
    }
}
