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
        // create some 2 matrices which are powers of 2, assert their multiplications are correct        
        int[,] first = new int[512, 512];
        int[,] second = new int[512, 512];

        // fill them with random numbers
        for (int i = 0; i < first.GetLength(0); i++)
        {
            for (int j = 0; j < first.GetLength(1); j++)
            {
                first[i, j] = Random.Shared.Next(-5000, 5000);
                second[i, j] = Random.Shared.Next(-5000, 5000);
            }
        }

        var watch = new Stopwatch();
        watch.Start();
        int[,] CStrassen = DivideAndConquer.StrassenMatrixMultiplication.StrassenMatrixMultiply(first,(0,first.GetLength(0)-1,0,first.GetLength(0)-1), second , (0,first.GetLength(0) - 1,0,first.GetLength(0) - 1));
        watch.Stop();
        helper.WriteLine($"Strassen took {watch.ElapsedMilliseconds} ms");

        watch.Restart();
        int[,] CNormal = MatrixMultiply(first, second);
        watch.Stop();
        helper.WriteLine($"Normal Matrix Multiply took {watch.ElapsedMilliseconds} ms");

        for (int i = 0; i < first.GetLength(0); i++)
        {
            for (int j = 0; j < first.GetLength(1); j++)
            {
                Assert.Equal(CStrassen[i, j], CNormal[i, j]);
            }
        }
    }


    private int[,] MatrixMultiply(int[,] matrix, int[,] otherMatrix)
    {
        // both are nxn matrices
        int[,] res = new int[matrix.GetLength(0), matrix.GetLength(1)];
        
        for (int i = 0; i < res.GetLength(0); i++)
        {
            for (int j=0;j< matrix.GetLength(1); j++)
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


