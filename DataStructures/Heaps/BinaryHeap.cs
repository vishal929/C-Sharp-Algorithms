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

    /// <summary>
    /// Comparer for our values depending on if we chose a min heap or max heap
    /// </summary>
    private IComparer<T> ComparerInternal { get; }
    
    /// <summary>
    /// Count of elements in the heap
    /// </summary>
    /// <returns>number of elements in the heap</returns>
    public int Count() => ListInternal.Count;
    
    /// <summary>
    /// Constructs an initially empty heap, min or max property is set by the boolean param
    /// </summary>
    /// <param name="isMax">param to set whether this is a min or max heap</param>
    public BinaryHeap(bool isMax)
    {
        // if isMax is true, create a max binary heap, else create a min heap
        ComparerInternal = new HeapComparer<T>(isMax);
    }
    
    /// <summary>
    /// Constructs a min or max heap from an array in linear time
    /// </summary>
    /// <param name="isMax">flag whether this is a min or max heap</param>
    /// <param name="initial">list to construct the heap from, this list is modified</param>
    public BinaryHeap(bool isMax, IList<T> initial):this(isMax)
    {
        // set the internal list to the provided one, and then sift down on the levels of leaf nodes to heapify the list 
        ListInternal = initial;
        // children after Count()/2 are leaf nodes in the heap, so dont need to sift down on them
        for (int i = Count() / 2; i >= 0; i--)
        {
            SiftDown(i);
        }
        
        // a heap has at most n/2^{h+1} nodes of any height h
        // so, we take SUM_{h=0}^{lgn} n/2^{h+1} and then we get n * SUM h/2^{h} ---> (1/2)/(1-(1/2))^2 ---> O(n)
        // so, we can heapify an array in O(n)
    }
    
    /// <summary>
    /// Returns the smallest or largest value from the heap based on min/max property, and maintains the heap invariant
    /// </summary>
    /// <returns>the value of the root of the heap, after removing it</returns>
    /// <exception cref="InvalidOperationException">thrown if there are no items in the heap</exception>
    public T Extract()
    {
        if (ListInternal.Count == 0) throw new InvalidOperationException("No elements are in the heap");
        
        // if there's just 1 item, return it
        if (ListInternal.Count == 1)
        {
            T tmp = ListInternal[0];
            ListInternal.Clear();
            return tmp;
        }
        else
        {
            // place the last leaf in the position of the root, then sift down
            T tmp = ListInternal[0];

            T leaf = ListInternal[ListInternal.Count - 1];
            ListInternal.RemoveAt(ListInternal.Count - 1);

            ListInternal[0] = leaf;

            SiftDown(0);
            return tmp;
        }
    }
    
    /// <summary>
    /// Peeks the root of the heap
    /// </summary>
    /// <returns>value at the root of the heap, without removing it</returns>
    /// <exception cref="InvalidOperationException">thrown if the heap is empty</exception>
    public T Peek()
    {
        if (ListInternal.Count == 0) throw new InvalidOperationException("No elements are in the heap");
        return ListInternal[0];
    } 

    /// <summary>
    /// Sift down operation to maintain the heap invariant of a parent
    /// </summary>
    /// <param name="idx">parent to sift down on</param>
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

}
