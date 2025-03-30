namespace DynamicProgramming
{
    public class MaxSubarray
    {
        public static (int Start,int End,int Sum) FindMaxSubarray(int[] arr)
        {

            // can find max subarray in linear time
            (int bestSum, int bestStart, int bestEnd) bestInfo = (arr[0], 0, 0);
            int currSum = arr[0];
            int start = 0;
            int end = 0;
            for (int i = 1; i < arr.Length; i++)
            {
                if (arr[i] >= currSum)
                {
                    // no use keeping the metrics, reset
                    currSum = arr[i];
                    start = i;
                    end = i;
                } else
                {
                    end = i;
                    currSum += arr[i];
                }

                if (currSum > bestInfo.bestSum)
                {
                    bestInfo = (currSum, start, end);
                }
            }

            return bestInfo;

        } 
    }
}
