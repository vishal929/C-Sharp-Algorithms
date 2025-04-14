using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace DivideAndConquer.Tests;

public class TestExponent(ITestOutputHelper helper)
{

    [Fact]
    public void TestPow()
    {
        double x = Random.Shared.NextDouble() * 30;
        int pow = Random.Shared.Next(-10, 10);

        double comp = Exponent.Pow(x, pow);
        double lib = Math.Pow(x, pow);

        // assert they are equal within a certain epsilon value
        double epsilon = 0.05;

        if (Math.Abs(lib - comp) > epsilon)
        {
            helper.WriteLine($"x: {x} power: {pow}");
            helper.WriteLine("Our computed value " + comp);
            helper.WriteLine("Library computed value " + lib);
        }
        

        Assert.True(Math.Abs(lib-comp)<=epsilon);
    }
}
