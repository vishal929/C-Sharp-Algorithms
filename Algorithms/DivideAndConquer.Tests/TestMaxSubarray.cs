namespace DivideAndConquer.Tests
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
            (int divideStart, int divideEnd, int divideSum) = DivideAndConquer.MaxSubarray.FindMaxSubarray(arr,0,arr.Length-1);

            // assert that the values are the same
            Assert.Equal(divideSum, bruteSum);

            // assert that the sum is actually true
            int sum = arr.Skip(divideStart).Take(divideEnd - divideStart + 1).Sum();
            Assert.Equal(sum, divideSum);
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
