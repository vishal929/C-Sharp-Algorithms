using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.Shared;

public class TreeNode<T> where T : IComparable<T>
{
    public TreeNode<T>?[] Children { get; init; }

    public T Value { get; init;}

    public TreeNode(T value, int numChildren = 2)
    {
        // allocate space for children, unfilled children are null  
        Children = new TreeNode<T>?[numChildren];
        Value = value;
    }

    public void AddChild(TreeNode<T> child, int index)
    {
        if (index <0 || index >= Children.Length)
        {
            throw new ArgumentOutOfRangeException(nameof(index));
        }

        Children[index] = child;
    }

}
