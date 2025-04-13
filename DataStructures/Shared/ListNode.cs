using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.Shared;
public class ListNode<T> where T : IComparable<T>
{
    public ListNode<T>? Next { get; set; }

    public T Val { get; set; }

    public ListNode(T val) : this(val, null) { }

    public ListNode(T val, ListNode<T>? next)
    {
        Val = val;
        Next = next;
    }

}
