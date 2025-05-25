using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicProgramming;
public class EditDistance
{
    public static int Determine(string s1, string s2)
    {
        // O(n^2) complexity  with O(n) space

        // determines the edit distance of s1 to s2 (minimum # of operations needed to transform s1 to s2)
        // we can add characters , remove characters, or transform characters into another character

        // let dp[i][j] be the edit distance of s1[0...i] to s2[0..j]
        // if either substring is empty, we just increment with the remaining chars in the other substring
        // if s1[i] == s2[j] recurse on the subproblem s[0...i-1], s2[0...j-1]
        // if s1[i] != s2[j] :
        //    - suppose we add s2[j] to the end of s1[0...i] (1 operation) and recurse on the case (i,j-1)
        //    - we can replace s1[i] with s2[j] (1 operation) and recurse on the case (i-1,j-1)
        //    - we can delete s1[i] (1 operation) and recurse on (i-1,j)

        // so, dp[i][j] depends on the previous column's entry, the last diagonal entry, and the previous rows entry
        // so we can update row by row

        // create a margin of zeros
        int[,] dp = new int[s1.Length + 1, s2.Length + 1];

        for (int i = 0; i < s2.Length + 1; i++)
        {
            dp[0, i] = i;
        }

        for (int i = 0; i < s1.Length + 1; i++)
        {
            dp[i, 0] = i;
        }

        // update
        for (int i = 1; i < s1.Length + 1; i++)
        {
            for (int j = 1; j < s2.Length + 1; j++)
            {
                int subCost = s1[i - 1] == s2[j - 1] ? 0 : 1;
                
                // replacing s1[i] with s2[j] if they are not the same
                int replaceChar = dp[i - 1, j - 1] + subCost;

                // adding s2[j] to the end of s1[i]
                int addChar = dp[i, j - 1] + 1;

                // deleting s1[i] and recursing
                int delChar = dp[i - 1, j] + 1;

                dp[i, j] = Math.Min(Math.Min(replaceChar, addChar), delChar);
            }
        }

        return dp[s1.Length, s2.Length];

    }
}
