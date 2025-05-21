using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DataStructures.Shared;

namespace Greedy;
public class HuffmanCoding
{
    public static (IDictionary<string,string> Encoding,TreeNode<(double,string?)> HuffmanTree) GetEncoding(string[] symbols, double[] probabilities)
    {

        // generate our Trees now where the root is the probability of that character appearing
        PriorityQueue<TreeNode<(double,string?)>, double> q = new(); 
        
        // enqueue symbols in n log n
        for (int i=0;i<symbols.Length;i++)
        {
            q.Enqueue(new TreeNode<(double,string?)>((probabilities[i],symbols[i])), probabilities[i]);
        }
        
        // replace each 2 nodes with 1 node
        // n log n operations
        while (q.Count >1)
        {
            // never null
            q.TryPeek(out _, out double firstProb);
            TreeNode<(double,string?)> firstTree = q.Dequeue();

            q.TryPeek(out _, out double secondProb);
            TreeNode<(double,string?)> secondTree = q.Dequeue();

            // create a tree with the root being some dumb value (null?)
            TreeNode<(double,string?)> parent = new((firstProb+secondProb,null));

            parent.AddChild(firstTree, 0);
            parent.AddChild(secondTree, 1);

            q.Enqueue(parent,firstProb+secondProb);
        }

        // the final tree in the queue is the huffman tree that is built
        TreeNode<(double Prob,string? Symbol)> huffmanTree = q.Dequeue();

        // create a dictionary mapping our symbols to binary codes by traversing the tree
        Dictionary<string, string> coding = new();

        Stack<(StringBuilder, TreeNode<(double Prob,string? Symbol)>?,int)> s = new();
        s.Push((new StringBuilder(),huffmanTree,0));
       
        // we have O(k) leaf nodes, so worst case is a full binary tree with O(k) leaf nodes
        // if the last layer has k leaf nodes, then the entire tree has 2*k -1 nodes --> O(k)
        // so 2^{h-1} = k --> h-1 = log(k) --> h = log(k)+1
        // the entire tree contains 2^{h}-1 nodes --> 2^{log(k)+1}-1 --> 2k-1 nodes
        while(s.Count > 0)
        {
            (StringBuilder builder, TreeNode<(double Prob, string? Symbol)>? node, int backTrack) = s.Pop();

            if (node is null) continue;

            // if backtrack is 0, we process the left node
            // if backtrack is 1, we process the right
            // if backtrack is 3, we remove whatever we set for the right

            // if the node is not null, the builder represents the coding for this symbol
            // this will only happen at leaf nodes
            if (node.Value.Symbol!= null && backTrack==0)
            {
                coding.Add(node.Value.Symbol, builder.ToString());
            }

            if (backTrack == 0) 
            {
                builder.Append('0');
                s.Push((builder, node, 1));
                s.Push((builder, node.Children[0], 0));
            }
            else if (backTrack== 1)
            {
                // remove '0' and process the right
                builder.Remove(builder.Length - 1,1);
                builder.Append('1');
                s.Push((builder, node, 2));
                s.Push((builder, node.Children[1], 0));

            }
            else
            {
                builder.Remove(builder.Length - 1, 1);
            }
                         
        }

        return (coding, huffmanTree);

    }    
}
