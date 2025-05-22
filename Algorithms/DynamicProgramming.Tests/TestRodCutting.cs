using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicProgramming.Tests;
public class TestRodCutting
{

    [Fact]
    public void TestMaxPrice()
    {
        // generate a random array of a random number of prices
        int numPrices = 30;

        int[] prices = Enumerable.Range(0,numPrices).Select(x => Random.Shared.Next()).ToArray();

        Assert.Equal(RodCutting.DetermineRevenue(prices), GetMaxPriceBruteForce(prices, prices.Length));
    }


    private int GetMaxPriceBruteForce(int[] prices, int length)
    {
        // recursively generate the solution
        if (length == 1) return prices[0];

        // if we dont cut
        int max = prices[length - 1];

        for (int i = 1; i < length; i++)
        {
            // cut in the middle somewhere
            max = Math.Max(max, prices[i - 1] + GetMaxPriceBruteForce(prices, length - i));
        }

        return max;
    }


}
