using DataStructures.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.Trees;

/// <summary>
/// A binary tree is a tree where each node has a left and right child
/// </summary>
public class BinaryTree<T> where T : IComparable<T>
{
    TreeNode<T>? Root { get; set; }
    
     
}
