using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicProgramming;
public class MatrixChainMultiplication
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="matrixDims">array of matrix dimensions where matrixDims[i][0] x matrix[i][1] is the dimension of the i-th matrix</param>
    /// <returns>matrix multiplication order and the # of multiplications required</returns>
    public (int[], int) MinimizeMultiplications(int[][] matrixDims)
    {
        // have matrix multiplication like mxp pxn to make an mxn matrix
        return (new int[3], 3);
    }
}
