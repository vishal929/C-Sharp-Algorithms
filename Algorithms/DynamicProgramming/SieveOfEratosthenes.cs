using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicProgramming;
public class SieveOfEratosthenes
{
    /// <summary>
    /// Generates primes up to n
    /// </summary>
    /// <param name="n">upper bound (inclusive) on primes to generate</param>
    /// <returns>list of primes</returns>
    public static int[] GeneratePrimes(int n)
    {
        // just use n+1 so we can use 1 indexing
        bool[] possible = new bool[n+1];
        for (int i = 2; i <= (int)Math.Sqrt(n); i++)
        {
            // if this is marked, continue (its composite)
            if (possible[i])
            {
                // composite continue
                continue;
            }
            else
            {
                // this is prime, mark the composites
                // O(n/i) time
                // the partial prime harmonic series is bounded by O(loglog(n))
                for (int j=i*i;j<=n; j += i)
                {
                    possible[j] = true;
                }
            }
        }

        // in the above block ,we do n/2 + n/3 + n/5 + ..... + n/lastPrime work.
        // so this total work will be O(nloglog(n))

        // skip 0 and 1 and return the primes (O(n))
        int numPrime = 0;
        for (int i = 2; i <= n; i++)
        {
            numPrime += (possible[i] ? 0 : 1);
        }

        int[] primes = new int[numPrime];
        int c = 0;
        for (int i = 2; i <= n; i++)
        {
            if (!possible[i])
            {
                primes[c] = i;
                c++;
            }
        }
        return primes;

        // we have total complexity O(n loglog(n))


    }
}
