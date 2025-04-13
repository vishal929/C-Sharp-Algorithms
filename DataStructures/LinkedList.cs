using DataStructures.Shared;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures;
public class LinkedList<T>:IList<T> where T:IComparable<T>
{
    private ListNode<T>? Head { get; set; } = null;

    private int Count { get; set; } = 0;

    int ICollection<T>.Count => this.Count;

    public bool IsReadOnly => throw new NotImplementedException();

    public T this[int index] { get => InternalFindIdx(index).FoundNode!.Val; set => InternalFindIdx(index).FoundNode!.Val=value; }

    public void RemoveValue(T val)
    {
        // search for the value and delete it
        Remove(val);
    }

    public void RemoveAt(int index)
    {
        // cant specify index thats greater than or equal to count
        if (index < 0 || index >= Count) throw new ArgumentOutOfRangeException("index");

        var foundInfo = InternalFindIdx(index); 

        if (foundInfo.Prev is null)
        {
            // removing the head
            Head = foundInfo.FoundNode!.Next;
        }
        else
        {
            foundInfo.Prev.Next = foundInfo.FoundNode!.Next; 
        }
    }

    public int IndexOf(T item)
    {
        var foundInfo = InternalFindValue(item);
        return foundInfo.Found ? foundInfo.idx : -1;
    }

    public void Insert(int index, T item)
    {
        if (index <0 || index > Count)
        {
            throw new ArgumentException("Index was outside the bounds of the list", nameof(index));
        }
        
        if (Head == null && index != 0)
        {
            throw new InvalidOperationException("Head was null but we want to insert at non-zero index");
        }
        ListNode<T>? runner = Head;
        ListNode<T>? prev = null;
        int runnerIdx = 0;
        while (runnerIdx!=index)
        {
            prev = runner;
            runner = runner!.Next;
            runnerIdx++;
        }

        ListNode<T> toInsert = new ListNode<T>(item, runner);
        // insert
        if (prev is null)
        {
            // becomes new head 
            Head = toInsert;
        }
        else
        {
            prev.Next = new ListNode<T>(item, runner);
        }

        // increment the count
        Count++;
    }

    public void Add(T item)
    {
        // insert at the end of the list
        Insert(Count, item);
    }

    public void Clear()
    {
        Head = null;
        Count = 0;
    }

    public bool Contains(T item)
    {
        return InternalFindValue(item).Found;
    }

    public void CopyTo(T[] array, int arrayIndex)
    {
        throw new NotImplementedException();
    }

    public bool Remove(T item)
    {
        var foundInfo = InternalFindValue(item);
        if (!foundInfo.Found) return false;

        if (foundInfo.Prev is null)
        {
            // removing the head
            Head = null;
        }
        else
        {
            // found node wont be null if prev is not null
            foundInfo.Prev = foundInfo.FoundNode!.Next;

        }
        Count--;
        return true;
    }

    public IEnumerator<T> GetEnumerator()
    {
        throw new NotImplementedException();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        throw new NotImplementedException();
    }

    private (ListNode<T>? Prev, bool Found, ListNode<T>? FoundNode, int idx) InternalFindValue(T val)
    {
        Func<ListNode<T>?, int, bool> filter = (ListNode<T>? node, int index) => (node != null && node.Val.CompareTo(val)==0);
        var foundInfo = InternalFind(filter); 
        return InternalFind(filter);
    }

    private (ListNode<T>? Prev, bool Found, ListNode<T>? FoundNode) InternalFindIdx(int idx)
    {
        if (idx <0 || idx > Count) throw new ArgumentOutOfRangeException(nameof(idx));

        Func<ListNode<T>?, int, bool> filter = (ListNode<T>? node, int index) => (index == idx);

        var foundInfo = InternalFind(filter);
        return (foundInfo.Prec, foundInfo.Found, foundInfo.FoundNode);
    }

    private (ListNode<T>? Prec, bool Found, ListNode<T>? FoundNode, int idx) InternalFind(Func<ListNode<T>?,int,bool> filter)
    {
        ListNode<T>? runner = Head;
        ListNode<T>? prev = null;
        int runnerIdx = 0;
        bool found = filter(runner, runnerIdx);
        while (!found)
        {
            prev = runner;
            runner = runner?.Next ?? throw new InvalidOperationException("Null iteration!");
            runnerIdx++;
            found = filter(runner, runnerIdx);
        }

        // check if we found our node
        return (prev, found, runner,runnerIdx);


    }
}
