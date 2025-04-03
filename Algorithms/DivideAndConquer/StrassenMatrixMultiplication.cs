using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DivideAndConquer
{
    public class StrassenMatrixMultiplication
    {
        // divide and conquer algo to multiply nxn matrices
        // suppose n is a power of 2. if not, we can just pad the matrix with zeros

        public static int[,] StrassenMatrixMultiply(int[,] matrix, (int rowStart,int rowEnd,int colStart,int colEnd) bounds, int[,] otherMatrix, (int rowStart,int rowEnd,int colStart,int colEnd) otherBounds)
        {
            // if the bounds are 2x2 just go to regular multiply
            int leafBound = 64;
            if (bounds.rowEnd-bounds.rowStart+1 <= leafBound)
            {
                var size = bounds.rowEnd - bounds.rowStart + 1;
                int[,] first = new int[size, size];
                int[,] second = new int[size, size];
                for (int i=0;i<size;i++)
                {
                    for (int j = 0; j < size; j++)
                    {
                        first[i, j] = matrix[bounds.rowStart + i, bounds.colStart+ j];
                        second[i, j] = otherMatrix[otherBounds.rowStart + i, otherBounds.colStart + j];
                    }
                }
                return MatrixMultiply(first, second);
            }
            //TODO: redo with proper indexing of bounds!!!
            // create a matrix C to hold the output
            int[,] C = new int[bounds.rowEnd-bounds.rowStart+1, bounds.colEnd-bounds.colStart+1];

            

            // partition matrix and otherMatrix into 4 matrices of size n/2 (since they are the same dim, we just need to create 4 idx partititions)
            int mRowLength = (bounds.rowEnd - bounds.rowStart + 1);
            int mColLength = (bounds.colEnd - bounds.colStart + 1);
            var m_1_1 = (bounds.rowStart, bounds.rowStart + (mRowLength / 2)-1, bounds.colStart, bounds.colStart + (mColLength/ 2)-1);
            var m_1_2 = (bounds.rowStart, bounds.rowStart + (mRowLength / 2)-1, bounds.colStart+(mColLength / 2), bounds.colStart+mColLength - 1);
            var m_2_1 = (bounds.rowStart + (mRowLength / 2), bounds.rowStart + mRowLength - 1, bounds.colStart, bounds.colStart + (mColLength / 2)-1);
            var m_2_2 = (bounds.rowStart + (mRowLength / 2), bounds.rowStart + mRowLength - 1, bounds.colStart + (mColLength / 2), bounds.colStart + mColLength - 1);

            mRowLength = otherBounds.rowEnd - otherBounds.rowStart + 1;
            mColLength = otherBounds.colEnd - otherBounds.colStart + 1;
            var otherM_1_1 = (otherBounds.rowStart, otherBounds.rowStart + (mRowLength / 2)-1, otherBounds.colStart, otherBounds.colStart + (mColLength/ 2)-1);
            var otherM_1_2 = (otherBounds.rowStart, otherBounds.rowStart + (mRowLength / 2)-1, otherBounds.colStart+(mColLength / 2), otherBounds.colStart+mColLength - 1);
            var otherM_2_1 = (otherBounds.rowStart + (mRowLength / 2), otherBounds.rowStart + mRowLength - 1, otherBounds.colStart, otherBounds.colStart + (mColLength / 2)-1);
            var otherM_2_2 = (otherBounds.rowStart + (mRowLength / 2), otherBounds.rowStart + mRowLength - 1, otherBounds.colStart + (mColLength / 2), otherBounds.colStart + mColLength - 1);


            // compute matrix sums we need
            var S1 = MatrixDifference(otherMatrix, otherM_1_2, otherMatrix, otherM_2_2);
            var S2 = MatrixSum(matrix, m_1_1, matrix, m_1_2);
            var S3 = MatrixSum(matrix, m_2_1, matrix, m_2_2);
            var S4 = MatrixDifference(otherMatrix, otherM_2_1, otherMatrix, otherM_1_1);
            var S5 = MatrixSum(matrix, m_1_1, matrix, m_2_2);
            var S6 = MatrixSum(otherMatrix, otherM_1_1, otherMatrix, otherM_2_2);
            var S7 = MatrixDifference(matrix, m_1_2, matrix, m_2_2);
            var S8 = MatrixSum(otherMatrix, otherM_2_1, otherMatrix, otherM_2_2);
            var S9 = MatrixDifference(matrix, m_1_1, matrix, m_2_1);
            var S10 = MatrixSum(otherMatrix, otherM_1_1, otherMatrix, otherM_1_2);

            // recursively compute multiplications that we need (note that this is T(n) = 7T(n/2) + O(n^2) ---> log7 by the master theorem
            var newMatrixBounds = (0, (mRowLength/2) - 1, 0, (mColLength/2) - 1);
            var P1 = StrassenMatrixMultiply(matrix, m_1_1, S1, newMatrixBounds);
            var P2 = StrassenMatrixMultiply(S2, newMatrixBounds, otherMatrix, otherM_2_2);
            var P3 = StrassenMatrixMultiply(S3, newMatrixBounds, otherMatrix, otherM_1_1);
            var P4 = StrassenMatrixMultiply(matrix, m_2_2, S4, newMatrixBounds);
            var P5 = StrassenMatrixMultiply(S5, newMatrixBounds, S6, newMatrixBounds);
            var P6 = StrassenMatrixMultiply(S7, newMatrixBounds, S8, newMatrixBounds);
            var P7 = StrassenMatrixMultiply(S9, newMatrixBounds, S10, newMatrixBounds);

            var C_1_1 = MatrixSum((P5, false), (P4, false), (P2, true), (P6, false));
            var C_1_2 = MatrixSum((P1, false), (P2, false));
            var C_2_1 = MatrixSum((P3, false), (P4, false));
            var C_2_2 = MatrixSum((P5, false), (P1, false), (P3, true), (P7, true));

            // fill in C and return it
            for (int i = 0; i < mRowLength / 2; i++)
            {
                for (int j = 0; j < mColLength/2;j++)
                {
                    C[i, j] = C_1_1[i, j];
                    C[i, j + mColLength / 2] = C_1_2[i, j];
                    C[i + mRowLength / 2, j] = C_2_1[i, j];
                    C[i + mRowLength / 2, j + mColLength / 2] = C_2_2[i, j];
                }
            }

            return C;
            
        }

        private static int[,] MatrixSum(params List<(int[,] Matrix ,bool isNegative)> matrices)
        {
            int[,] matrix = new int[matrices[0].Matrix.GetLength(0), matrices[0].Matrix.GetLength(1)];

            for (int i = 0; i < matrices.Count; i++)
            {
                for (int j = 0; j < matrix.GetLength(0); j++)
                {
                    for (int k = 0; k < matrix.GetLength(1); k++)
                    {
                        matrix[j, k] += (matrices[i].isNegative ? -1 : 1) * matrices[i].Matrix[j, k]; 
                    }
                }
            }
            return matrix;
        }



        private static int[,] MatrixSum(int[,] matrix, (int rowStartIdx,int rowEndIdx, int colStartIdx, int colEndIdx) mAddSelection, 
            int[,] otherMatrix, (int rowStartIdx ,int rowEndIdx,int colStartIdx,int colEndIdx) otherMAddSelection)
        {
            return MatrixSumDifferenceInternal(matrix, mAddSelection, otherMatrix, otherMAddSelection, true);
        }

        private static int[,] MatrixDifference(int[,] matrix, (int rowStartIdx,int rowEndIdx, int colStartIdx, int colEndIdx) mAddSelection, 
            int[,] otherMatrix, (int rowStartIdx ,int rowEndIdx,int colStartIdx,int colEndIdx) otherMAddSelection)
        {
            return MatrixSumDifferenceInternal(matrix, mAddSelection, otherMatrix, otherMAddSelection, false);
        }

        private static int[,] MatrixSumDifferenceInternal(int[,] matrix, (int rowStartIdx,int rowEndIdx, int colStartIdx, int colEndIdx) mAddSelection, 
            int[,] otherMatrix, (int rowStartIdx ,int rowEndIdx,int colStartIdx,int colEndIdx) otherMAddSelection, bool isSum)
        {
            int rowDiff = mAddSelection.rowEndIdx - mAddSelection.rowStartIdx + 1;
            int colDiff = mAddSelection.colEndIdx - mAddSelection.colStartIdx + 1;
            int[,] Res = new int[rowDiff,colDiff];

            // compute the sum/diff
            for (int i = 0; i < rowDiff; i++)
            {
                for (int j = 0; j < colDiff; j++)
                {
                    if (isSum)
                    {
                        Res[i,j] = matrix[mAddSelection.rowStartIdx+i,mAddSelection.colStartIdx+j] + otherMatrix[otherMAddSelection.rowStartIdx+i,otherMAddSelection.colStartIdx+j];
                    }
                    else
                    {
                        Res[i,j] = matrix[mAddSelection.rowStartIdx+i,mAddSelection.colStartIdx+j] - otherMatrix[otherMAddSelection.rowStartIdx+i,otherMAddSelection.colStartIdx+j];
                    }
                    
                }
            }
            return Res;
        }

        public static int[,] MatrixMultiply(int[,] matrix, int[,] otherMatrix)
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
}
