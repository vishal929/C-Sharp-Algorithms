using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.Heaps;
public class HeapComparer<T> : IComparer<T> where T : IComparable<T>
{
    private bool IsMax { get; }

    public HeapComparer(bool isMax)
    {
        IsMax = isMax;
    }

    public int Compare(T? x, T? y)
    {
        if (x is null) return -1;
        if (y is null) return 1;

        int natPriority = x.CompareTo(y);

        // higher values go first
        if (IsMax) return natPriority;

        // -1 turns to 1, 1 turns to -1, flipping the order essentially
        return natPriority * -1;
    }
}
