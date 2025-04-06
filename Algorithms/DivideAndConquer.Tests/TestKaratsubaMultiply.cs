using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace DivideAndConquer.Tests;
public class TestKaratsubaMultiply (ITestOutputHelper helper)
{
    [Fact]
    public void TestKMultiply()
    {

        // pick numbers large enough to give karatsuba an edge (big integers)
        var xBuilder = new StringBuilder();
        var yBuilder = new StringBuilder();

        for (int i = 0; i < 500; i++)
        {
            xBuilder.Append(Random.Shared.Next(1, 9));
            yBuilder.Append(Random.Shared.Next(1, 9));
        }
        BigInteger x = BigInteger.Parse(xBuilder.ToString());
        BigInteger y = BigInteger.Parse(yBuilder.ToString());

        string xBin = BigIntegerToString(x);
        string yBin = BigIntegerToString(y);

        var watch = new Stopwatch();
        watch.Start();
        string mult = Karatsuba.BinaryMultiplication(xBin, yBin);
        watch.Stop();

        helper.WriteLine($"regular binary multiplication took {watch.ElapsedMilliseconds} ms");

        watch.Restart();
        string karatMult = Karatsuba.KaratsubaMultiply(xBin, yBin);
        watch.Stop();

        helper.WriteLine($"karatsuba binary multiplication took {watch.ElapsedMilliseconds} ms");

        BigInteger multVal = StringToBigInteger(mult);
        BigInteger karatVal = StringToBigInteger(karatMult);

        Assert.Equal(multVal, karatVal);
    }



    [Fact]
    public void TestMultiply()
    {
        for (int i = 0; i < 10; i++)
        {
            int x = Random.Shared.Next(100,5000);
            int y = Random.Shared.Next(100,5000);
            
            string xBin = Karatsuba.ToBinary(x);
            string yBin = Karatsuba.ToBinary(y);

            long outMult = Karatsuba.FromBinary(Karatsuba.BinaryMultiplication(xBin, yBin));
            long mult = x * y;
            Assert.Equal(outMult, mult);
        }
    }

    [Fact]
    public void TestSubtract()
    {
        // x > y subtraction
        for (int i = 0; i < 10; i++)
        {
            int y = Random.Shared.Next(100,5000);
            int x = Random.Shared.Next(y,y+5000);

            string xBin = Karatsuba.ToBinary(x);
            string yBin = Karatsuba.ToBinary(y);

            string diffBin = Karatsuba.BinaryDifference(xBin, yBin);

            long outDiff = Karatsuba.FromBinary(diffBin);

            Assert.Equal(outDiff , x - y);
        }
        
        
    }

    [Fact]
    public void TestAdd()
    {
        for (int i = 0; i < 10; i++)
        {
            int x = Random.Shared.Next(100,5000);
            int y = Random.Shared.Next(100,5000);

            string xBin = Karatsuba.ToBinary(x);
            string yBin = Karatsuba.ToBinary(y);

            long outAdd = Karatsuba.FromBinary(Karatsuba.BinaryAdd(xBin, yBin));

            Assert.Equal(outAdd, x + y);
        }
        
    }

    private string BigIntegerToString(BigInteger num)
    {
        var builder = new StringBuilder();
        while(num > 0)
        {
            (BigInteger divisor, BigInteger rem) = BigInteger.DivRem(num, 2);
            num = divisor;
            builder.Append(rem);
        }
        return new string(builder.ToString().Reverse().ToArray());
    }

    private BigInteger StringToBigInteger(string str)
    {
        BigInteger sum = 0;
        BigInteger power = 1;
        for (int i = str.Length - 1; i >= 0; i--)
        {
            sum += (str[i] - '0') * power;
            power *= 2;
        }
        return sum;
    }

}
