using System.Numerics;

namespace DivideAndConquer
{
    public class MaxSubarray
    {
        public static (int Start ,int End , int Sum) FindMaxSubarray(int[] arr,int start, int end)
        {
            if (start == end)
            {
                return (start, end, arr[start]);
            }
            // the maximum subarray can be entirely within [0..mid]
            // or it can be within [mid..end]
            // or it can cross over between [k....mid....z]

            // note that the first 2 cases are subproblems, but the third case of crossing is not exactly a subproblem 
            int low = start;
            int high = end;
            int mid = (low+high)/ 2;

            // T(n) = 2T(n/2) + O(n) ----> O(nlgn) by master theorem

            (int leftStart, int leftEnd, int leftSum) = FindMaxSubarray(arr, low, mid);
            (int rightStart, int rightEnd, int rightSum) = FindMaxSubarray(arr, mid + 1, high);
            (int crossStart, int crossEnd, int crossSum) = FindMaxSubarrayCrossing(arr, low, mid, end);

            // need >=
            // consider leftSum = 4, rightSum = 4 and crossSum=3
            if (leftSum >= rightSum && leftSum >= crossSum)
            {
                return (leftStart, leftEnd, leftSum);
            } 
            else if (rightSum >= leftSum && rightSum >= crossSum)
            {
                return (rightStart, rightEnd, rightSum);
            } else
            {
                return (crossStart, crossEnd, crossSum);
            }
            
        }

        public static (int Start, int End, int Sum) FindMaxSubarrayCrossing(int[] arr, int start, int mid, int end) 
        {
            if (start == end)
            {
                return (start, end, arr[start]);
            }
            // basically get the max left sum, the max right sum, and combine them
            int leftSum = arr[mid];
            int currSum = arr[mid];
            int leftBestIdx = mid;
            for (int j = mid-1; j >= start; j--)
            {
                currSum += arr[j];
                if (currSum > leftSum)
                {
                    leftSum = currSum;
                    leftBestIdx = j;
                }
            }
            int rightSum = arr[mid+1];
            currSum = arr[mid+1];
            int rightBestIdx = mid+1;
            for (int j = mid + 2; j <= end; j++)
            {
                currSum += arr[j];
                if (currSum > rightSum)
                {
                    rightSum = currSum;
                    rightBestIdx = j;
                }
            }

            return (leftBestIdx, rightBestIdx, leftSum + rightSum);
        }

    }
}
