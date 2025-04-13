namespace DynamicProgramming;
public class LongestIncreasingSubsequence
{

    // get the longest strictly increasing subsequence
    public static IList<int> GetLIC(int[] arr)
    {
        // first sort the array in nlogn, so that we only have to scan to the left
        Array.Sort(arr);

        // let dp[i] represent the length of the longest increasing subsequence including arr[i]
        // need to keep an array prev[i] to build the sequence from when generating a solution

        int[] dp = new int[arr.Length];
        dp[0] = 1;

        // prev stores indices of decisions for generating the solution (-1 means we are at the end of the chain)
        int[] prev = new int[arr.Length];
        prev[0] = -1;

        for (int i = 1; i < arr.Length; i++)
        {
            // find the largest up till now, default is just a sequence of itself
            dp[i] = 1;
            prev[i] = -1;
            for (int j = 0; j < i; j++)
            {
                if (arr[j] < arr[i] && dp[j] + 1 > dp[i])
                {
                    // candidate
                    dp[i] = dp[j] + 1;
                    prev[i] = j; 
                }
            }
        }

        // get the max dp
        int largest = dp[0];
        int largestIdx = 0;

        for (int i = 1; i < dp.Length; i++)
        {
            if (dp[i] > largest)
            {
                largest = dp[i];
                largestIdx = i;
            }
        }

        IList<int> seq = new List<int>();

        seq.Add(arr[largestIdx]);

        int prevVal = prev[largestIdx];

        while (prevVal != -1)
        {
            seq.Add(arr[prevVal]);
            prevVal = prev[prevVal];
        }

        return seq.Reverse().ToList();
    }

    public static IList<int> GetLICBruteForce(int[] arr)
    {
        // sort the array first 
        Array.Sort(arr);

        IList<int> bestSoFar = new List<int>();

        for (int i = 0; i < arr.Length; i++)
        {
            IList<int> found = GetLICRecursive(new List<int>(), i, arr);

            if (found.Count > bestSoFar.Count)
            {
                bestSoFar = found;
            }
        }

        return bestSoFar;

    }

    private static IList<int> GetLICRecursive(IList<int> setSoFar, int idx, int[] arr)
    {
        if (idx >= arr.Length) return setSoFar;

        if (setSoFar.Count == 0 || arr[idx] > setSoFar.Last())
        {
            // add to the set
            setSoFar.Add(arr[idx]);
        }

        // go to the next idx
        return GetLICRecursive(setSoFar, idx + 1, arr);
    }

}
