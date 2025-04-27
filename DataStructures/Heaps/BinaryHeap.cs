using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.Heaps;
public class BinaryHeap<T> where T: IComparable<T>
{
    // the backing store for the binary heap is a list
    private IList<T> ListInternal { get; } = new List<T>();

    public int Count() => ListInternal.Count;
    
    public BinaryHeap(bool isMax)
    {
        // if isMax is true, create a max binary heap, else create a min heap
    }

    public T Peek()
    {
        if (ListInternal.Count == 0) throw new InvalidOperationException("No elements are in the heap");
        return ListInternal[0];
    } 

    

}
