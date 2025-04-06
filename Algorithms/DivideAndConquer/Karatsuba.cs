using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DivideAndConquer;
public class Karatsuba
{

    public static string KaratsubaMultiply(string x, string y)
    {
        // x and y are n digit binary strings
        // x = x_1*B^m + x_0
        // y = y_1*B^m + y_0
        // can split them and do the multiply based on below

        // i.e 110001111 = x = 1100 * 2^5 + 01111
        // 010110111 = y = 0101 * 2^5 + 10111


        // need to pad x and y if they arent the same length
        int pad = Math.Max(x.Length, y.Length);

        x = x.PadLeft(pad, '0');
        y = y.PadLeft(pad, '0');
        // if the lengths are small enough, default to normal mult
        if (x.Length <= 4)
        {
            return BinaryMultiplication(x, y);
        }
        // note that this is T(n) = 3T(n/2) + O(n) --> this is O(n^{log_2(3)}) ... better than O(n^2)
        // the O(n) part comes from the binary add/difference

        // x and y are n digit strings in base 2  (same length)

        // choose m as n/2 which shifts left half of x and right half of y
        int m = (x.Length / 2);

        string x1 = x.Substring(0, x.Length-m);
        string x0 = x.Substring(x.Length-m);

        string y1 = y.Substring(0, y.Length-m);
        string y0 = y.Substring(y.Length-m);

        // compute z0, z2, z3 
        string z0 = KaratsubaMultiply(x0, y0);
        string z2 = KaratsubaMultiply(x1, y1);
        string z1 = KaratsubaMultiply(BinaryAdd(x1, x0), BinaryAdd(y1, y0));

        // now z1 is z3-z2-z0
        string z3 = BinaryDifference(BinaryDifference(z1, z2),z0);

        // return the solution now
        return BinaryAdd(BinaryAdd(z2.PadRight(z2.Length + (m * 2),'0'), z3.PadRight(z3.Length + m,'0')), z0);
    }

    public static string ToBinary(long x)
    {
        // keep dividing by 2
        StringBuilder s = new();
        while (x > 0)
        {
            (long divisor, long rem) = Math.DivRem(x, 2);
            s.Append(rem);
            x = divisor;
        }

        return new string(s.ToString().Reverse().ToArray());
    }

    public static long FromBinary(string x)
    {
        long num = 0;
        long power = 1;
        for (int i = x.Length-1; i >=0; i--)
        {
            num += (x[i] - '0') * power;
            power *= 2;
        }

        return num;
    }

    public static string BinaryAdd(string x, string y)
    {
        StringBuilder res = new();
        // pad x and y to same length (padded)
        int numDigits = Math.Max(x.Length, y.Length);
        x = x.PadLeft(numDigits,'0');
        y = y.PadLeft(numDigits,'0'); 
        // do binary Addition
        int carry = 0;
        for (int i = x.Length-1; i >=0; i--) 
        {
            int sum = (x[i] - '0') + (y[i] - '0') + carry;
            carry = 0;
            if (sum >= 2)
            {
                sum -= 2;
                carry = 1;
            }
            res.Append(sum);
        }
        if (carry != 0)
        {
            res.Append(carry);
        } 

        // reverse the entire string and return it
        return new string(res.ToString().Reverse().ToArray());
    }

    public static string BinaryDifference(string x, string y)
    {
        int maxDigit = Math.Max(x.Length, y.Length);

        x = x.PadLeft(maxDigit,'0');
        y = y.PadLeft(maxDigit,'0');

        // assume x will always be greater than y, so we dont need to worry about negatives here
        StringBuilder s = new();

        int carry = 0;
        for (int i = x.Length - 1; i >= 0; i--)
        {
            int diff = (x[i] - '0') - (y[i] - '0') - carry;
            carry = 0;
            
            // 100000
            // 000101
            // ------
            // 011011 
            
            if (diff < 0)
            {
                carry = 1;
                diff+=2; 
            } 
            
            
            s.Append(diff); 
        }

        return new string(s.ToString().Reverse().ToArray());

    }

    public static string BinaryMultiplication(string x, string y)
    {

        string currSum = string.Empty;

        // this is O(n^2) since for each digit in y, we compute a sum of two O(n) numbers which is O(n)
        for (int j = y.Length - 1; j >= 0; j--)
        {
            // multiply this element with  all the elements of x
            StringBuilder res = new();
            for (int i = x.Length - 1; i >= 0; i--)
            {
                int mult = ((int)(x[i] - '0')) * ((int)(y[j] - '0'));
                res.Append(mult);
            }
            // reverse, pad right by how far we are from the end and add to a list
            string partialSum = new string(res.ToString().Reverse().ToArray()).PadRight(res.Length+(y.Length-1 - j),'0');

            currSum = currSum == string.Empty ? partialSum : BinaryAdd(currSum, partialSum);
        }

        return currSum;
    }
}
