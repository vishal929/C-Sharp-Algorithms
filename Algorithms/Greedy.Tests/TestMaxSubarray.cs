namespace Greedy.Tests
{
    public class TestMaxSubarray
    {
        [Fact]
        public void TestFindMaxSubarray()
        {
            // create some random array
            int[] arr = new int[1000];
            for (int i = 0; i < arr.Length; i++)
            {
                // forget about overflow for now
                arr[i] = Random.Shared.Next(int.MinValue/arr.Length, int.MaxValue/ arr.Length);
            }

            // find the best subarray by brute force
            (int bruteStart, int bruteEnd, int bruteSum) = FindMaxSubArrayBrute(arr);

            // find the best subarray by greedy method in linear time
            (int greedyStart, int greedyEnd, int greedySum) = Greedy.MaxSubarray.FindMaxSubarray(arr);

            // assert that the values are the same
            Assert.Equal(greedySum, bruteSum);

            // assert that the sum is actually true
            int sum = arr.Skip(greedyStart).Take(greedyEnd - greedyStart + 1).Sum();
            Assert.Equal(sum, greedySum);
        }

        (int Start, int End, int Sum) FindMaxSubArrayBrute(int[] arr)
        {
            (int start, int end, int sum) bestInfo = (-1, -1, int.MinValue);
            for (int i = 0; i < arr.Length; i++)
            {
                int sum = 0;
                for (int j = i; j < arr.Length; j++)
                {
                    sum += arr[j]; 
                    if (sum >= bestInfo.sum)
                    {
                        bestInfo = (i, j, sum);
                    }
                }
            }
            return bestInfo;
        }
    }
}
