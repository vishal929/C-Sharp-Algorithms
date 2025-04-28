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

    private IComparer<T> ComparerInternal { get; }

    public int Count() => ListInternal.Count;
    
    public BinaryHeap(bool isMax)
    {
        // if isMax is true, create a max binary heap, else create a min heap
        ComparerInternal = new HeapComparer<T>(isMax);
    }

    public BinaryHeap(bool isMax, IList<T> initial):this(isMax)
    {
        // set the internal list to the provided one, and then sift down on the levels of leaf nodes to heapify the list 
        ListInternal = initial;
        for (int i = Count() / 2; i >= 0; i--)
        {
            SiftDown(i);
        }
    }

    public T Peek()
    {
        if (ListInternal.Count == 0) throw new InvalidOperationException("No elements are in the heap");
        return ListInternal[0];
    } 

    
    private void SiftDown(int idx)
    {
        // examine the invariant properties and sift down the rooted tree at i
        // need 2i+1 and 2i+2 for 0-based index
        int leftChild = 2 * idx +1;
        int rightChild = 2 * idx + 2;

        int parent = idx;
        
        if (leftChild < ListInternal.Count && ComparerInternal.Compare(ListInternal[leftChild], ListInternal[parent]) == 1)
        {
            // the child should take the place of the parent
            parent = leftChild;
        }

        if (rightChild < ListInternal.Count && ComparerInternal.Compare(ListInternal[rightChild], ListInternal[parent]) == 1)
        {
            // the child should take the place of the parent
            parent = rightChild;
        }

        if (parent != idx)
        {
            // swap and recurse
            T tmp = ListInternal[parent];
            ListInternal[parent] = ListInternal[idx];
            ListInternal[idx] = tmp;
            SiftDown(parent);
        }
        
    }

    private void SiftUp(int idx)
    {

        // given a child at idx, we sift up to maintain the heap invariant
        int parent = (idx - 1) / 2;

        // if this is greater than the parent we need to swap
        if (parent>=0 && parent != idx && ComparerInternal.Compare(ListInternal[idx], ListInternal[parent])==1)
        {
            // this should swap with the parent
            // swap and recurse
            T tmp = ListInternal[parent];
            ListInternal[parent] = ListInternal[idx];
            ListInternal[idx] = tmp;
            SiftUp(parent);
        }

    }


    

}
