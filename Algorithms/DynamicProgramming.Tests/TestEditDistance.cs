using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicProgramming.Tests;
public class TestEditDistance
{

    [Fact]
    public void TestCalculate()
    {
        Assert.Equal(3,EditDistance.Determine("kitten", "sitting"));  
        Assert.Equal(0,EditDistance.Determine("theyarethesame", "theyarethesame"));  
        Assert.Equal(3,EditDistance.Determine("theyarethesame", "they are the same"));  
        Assert.Equal(2,EditDistance.Determine("book", "back"));  
        Assert.Equal(29,EditDistance.Determine("cgbyaaxfobfiffwfcwbgoevqtkizak", "hznjsrkjqxyiecnbjyuclrihhltkqt"));  




    }
}
