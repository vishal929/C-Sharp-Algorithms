using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DivideAndConquer;
public class Exponent
{

    /// <summary>
    /// Computes the exponent of x to the specified power
    /// </summary>
    /// <param name="x">exponand</param>
    /// <param name="power">exponent</param>
    /// <returns>the result of the exponent</returns>
    public static double Pow(double x, long power)
    {
        // T(n) = T(n/2) + O(1) -> Log(n) exponent
        // x^n = x^{n/2} * x^{n/2}
        if (power  == 0) return 1;
        if (power == 1) return x;
        if (power == -1) return 1 / x;

        // if power is not even, subtract 1 from it and then recurse
        if (power % 2 != 0)
        {
            power--;
            long half = power / 2;
            double comp = Pow(x, half);
            return x * comp * comp;
        }
        else
        {
            long half = power / 2;
            double comp = Pow(x, half);
            return comp * comp;
        }
    }
}
