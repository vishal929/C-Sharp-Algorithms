using DataStructures.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.Trees;
public class NaryTree<T> where T:IComparable<T>
{
    TreeNode<T>? Root { get; set; } = null;

    public void PreorderTraversal(TreeNode<T>? root, Action<TreeNode<T>> func)
    {
        // visit node first then children from left to right
        if (root is null) return;

        func(root);
        for (int i = 0; i < root.Children.Length; i++)
        {
          PreorderTraversal(root.Children[i],func); 
        }
    } 

    public void PostorderTraversal(TreeNode<T>? root, Action<TreeNode<T>> func)
    {
        // visit children first, then this node 
        if (root is null) return;

        for (int i = 0; i < root.Children.Length; i++)
        {
          PreorderTraversal(root.Children[i], func); 
        }

        func(root);
    }

    
    public void InorderTraversal(TreeNode<T>? root, Action<TreeNode<T>> func)
    {
        // visit half of children first then this node then the rest of the children from left to right
        if (root is null) return;
        for (int i = 0; i < root.Children.Length; i++)
        {
            if (i == root.Children.Length / 2)
            {
                // visit the parent
                func(root);
            }

            // visit the child
            InorderTraversal(root.Children[i],func);
        }
    }
}
