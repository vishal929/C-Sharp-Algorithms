using Sorting.ComparisonBased;

namespace Sorting.Tests.ComparisonBased
{
    public class SelectionSortTest
    {
        [Fact]
        public void TestSort()
        {
            // create an array of size 1000
            int[] test = new int[1000];

            int[] testCopy = new int[1000];

            for (int i = 0; i < test.Length; i++)
            {
                testCopy[i] = Random.Shared.Next();
            }

            // copy the array to testCopy
            Array.Copy(test, testCopy, test.Length);

            // sort both and assert that the result is the same
            Array.Sort(testCopy);

            SelectionSort.Sort(test);

            Assert.Equal(testCopy, test);
        }
    }
}
