using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicProgramming;
public class RodCutting
{
    /// <summary>
    /// Given a rod with n inches and prices an array from i=1...n representing the selling price of a rod of size i
    /// determine the maximum revenue from possible cuts of the rod
    /// </summary>
    /// <param name="prices">array of prices from 1 to n</param>
    /// <returns>maximum revenue that can be achieved</returns>
    public static int DetermineRevenue(int[] prices)
    {
        // let dp[i] be the maximum revenue achieved for cutting a rod with i inches
        // we either dont cut, or take the max revenue over some previous cut
        // then dp[i] = Max_j(p[j]+dp[i-j])

        // we will just use dp[0] = 0 for a rod of length 0

        // note that we have O(2^n-1) different ways to cut up the rod, but in this formulation, our complexity is O(n^2)

        int[] dp = new int[prices.Length+1];

        for (int i = 1; i <= prices.Length; i++)
        {
            for (int k = 1; k <= i; k++)
            {
                dp[i]= Math.Max(dp[i], prices[k-1] + dp[i - k]);
            }
        }

        return dp[prices.Length];
    }
}
