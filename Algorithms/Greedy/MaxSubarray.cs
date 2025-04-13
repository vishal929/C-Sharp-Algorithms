namespace Greedy
{
    public class MaxSubarray
    {
        public static (int Start,int End,int Sum) FindMaxSubarray(int[] arr)
        {

            // can find max subarray in linear time
            (int bestStart, int bestEnd, int bestSum) bestInfo = (0,0,arr[0]);
            int currSum = arr[0];
            int start = 0;
            int end=0;
            for (int i = 1; i < arr.Length; i++)
            {
                if (currSum <= 0)
                {
                    // restart since currSum + arr[i] <= arr[i], we would be better by just restarting the subarray
                    currSum = 0;
                    start = i;
                }
                end = i;
                currSum += arr[i];

                if (currSum >= bestInfo.bestSum)
                {
                    bestInfo = (start, end, currSum);
                }
            }

            return bestInfo;

        } 
    }
}
