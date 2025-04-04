using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace DivideAndConquer.Tests;
public class TestStrassenMatrixMultiplication
(ITestOutputHelper helper){

    [Fact]
    public void TestStrassenMatrixMultiply()
    {
        // create some 2 matrices of arbitrary length, assert their multiplications are correct        
        int[,] first = new int[923, 1024];
        int[,] second = new int[1024, 856];

        // fill them with random numbers
        for (int i = 0; i < first.GetLength(0); i++)
        {
            for (int j = 0; j < first.GetLength(1); j++)
            {
                first[i, j] = Random.Shared.Next(-5000, 5000);
            }
        }

        for (int i = 0; i < second.GetLength(0); i++)
        {
            for (int j = 0; j < second.GetLength(1); j++)
            {
                second[i, j] = Random.Shared.Next(-5000, 5000);
            }
        }

        int expectedResultRows = first.GetLength(0);
        int expectedResultCols = second.GetLength(1);

        var watch = new Stopwatch();
        watch.Start();
        int[,] CStrassen = DivideAndConquer.StrassenMatrixMultiplication.StrassenMatrixMultiply(first, second);
        watch.Stop();
        helper.WriteLine($"Strassen took {watch.ElapsedMilliseconds} ms");

        watch.Restart();
        int[,] CNormal = MatrixMultiply(first, second);
        watch.Stop();
        helper.WriteLine($"Normal Matrix Multiply took {watch.ElapsedMilliseconds} ms");

        Assert.Equal(expectedResultRows, CStrassen.GetLength(0));
        Assert.Equal(expectedResultCols, CStrassen.GetLength(1));

        for (int i = 0; i < expectedResultRows; i++)
        {
            for (int j = 0; j < expectedResultCols; j++)
            {
                Assert.Equal(CStrassen[i, j], CNormal[i, j]);
            }
        }
    }


    private int[,] MatrixMultiply(int[,] matrix, int[,] otherMatrix)
    {
        int[,] res = new int[matrix.GetLength(0), otherMatrix.GetLength(1)];
        
        for (int i = 0; i < res.GetLength(0); i++)
        {
            for (int j=0;j< res.GetLength(1); j++)
            {
                for (int k = 0; k < matrix.GetLength(1); k++)
                {
                    res[i, j] += matrix[i, k] * otherMatrix[k, j];
                } 
            }
        }
        return res;
    }
}


