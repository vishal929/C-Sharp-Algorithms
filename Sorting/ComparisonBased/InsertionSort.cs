using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorting.ComparisonBased;

/// <summary>
/// Insertion sort manages and increases a section of the array which is sorted
/// ... kind of like sorting cards in your hand during a card game
/// </summary>
public class InsertionSort
{
    /// <summary>
    /// Sorts in place using insertion sort algorithm
    /// </summary>
    /// <param name="list">list of comparables to sort</param>
    public static void Sort<T>(T[] list) where T:IComparable<T>
    {
        for (int i = 1; i < list.Length; i++)
        {
            int j = i;
            while (j >0 && list[j].CompareTo(list[j-1]) < 0)
            {
                // swap
                T tmp = list[j];
                list[j] = list[j+1];
                list[j] = tmp;
                j--;
            } 
        }
    }
}
