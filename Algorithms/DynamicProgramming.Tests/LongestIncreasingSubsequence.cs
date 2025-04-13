namespace DynamicProgramming.Tests;
public class LongestIncreasingSubsequence
{
    [Fact]
    public void TestLIC()
    {
        int testSize = 5000;
        int[] arr = new int[testSize];
        int[] arrGold = new int[testSize];

        for (int i = 0; i < testSize; i++)
        {
            arr[i]=Random.Shared.Next(int.MinValue, int.MaxValue);
        }

        Array.Copy(arr, arrGold, testSize);

        // test both
        IList<int> licOurs = DynamicProgramming.LongestIncreasingSubsequence.GetLIC(arr);
        IList<int> gold = DynamicProgramming.LongestIncreasingSubsequence.GetLICBruteForce(arr);

        Assert.True(IsLic(licOurs));
        Assert.True(IsLic(gold));

        Assert.Equal(licOurs.Count, gold.Count);
    }

    private bool IsLic(IList<int> lic)
    {
        for (int i = 1; i < lic.Count; i++)
        {
            if (lic[i]<= lic[i - 1])
            {
                return false;
            }
        }
        return true;
    }
}
