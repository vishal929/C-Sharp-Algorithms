using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace DynamicProgramming.Tests;
public class TestPrimes(ITestOutputHelper helper)
{
    [Fact]
    public void TestGeneratePrimes()
    {
        int n = 50000;

        Stopwatch watch = new();
        watch.Start();
        int[] sievePrimes = SieveOfEratosthenes.GeneratePrimes(n);
        watch.Stop();

        helper.WriteLine($"Sieve of Eratosthenes took {watch.ElapsedMilliseconds} milliseconds");

        watch.Restart();
        int[] brutePrimes = PrimesBrute(n);
        watch.Stop();

        helper.WriteLine($"brute force took {watch.ElapsedMilliseconds} milliseconds");


        Assert.Equal(brutePrimes, sievePrimes);
    }



    /// <summary>
    /// Generate primes by brute force trial division
    /// </summary>
    /// <param name="n"></param>
    /// <returns></returns>
    public int[] PrimesBrute(int n)
    {
        List<int> primes = new List<int>();
        primes.Add(2);

        for (int i = 3; i <= n; i++)
        {
            bool isPrime = true;
            for (int j = 0; j < primes.Count(); j++)
            {
                if (i % primes[j] == 0)
                {
                    // this isnt prime
                    isPrime = false;
                    break;
                }
            }
            if (isPrime) primes.Add(i);
        }
        return primes.ToArray();
    }
}
