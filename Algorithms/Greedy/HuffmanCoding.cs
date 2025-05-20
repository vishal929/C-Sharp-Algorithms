using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DataStructures.Shared;

namespace Greedy;
public class HuffmanCoding
{
    public static (IDictionary<string,string> Encoding,TreeNode<string?> HuffmanTree) GetEncoding(string[] symbols, double[] probabilities)
    {

        // generate our Trees now where the root is the probability of that character appearing
        PriorityQueue<TreeNode<string?>, double> q = new(); 

        for (int i=0;i<symbols.Length;i++)
        {
            q.Enqueue(new TreeNode<string?>(symbols[i]), probabilities[i]);
        }

        while (q.Count >1)
        {
            // never null
            q.TryPeek(out _, out double firstProb);
            TreeNode<string?> firstTree = q.Dequeue();

            q.TryPeek(out _, out double secondProb);
            TreeNode<string?> secondTree = q.Dequeue();

            // create a tree with the root being some dumb value (null?)
            TreeNode<string?> parent = new(null);

            parent.AddChild(firstTree, 0);
            parent.AddChild(secondTree, 1);

            q.Enqueue(parent,firstProb + secondProb);
        }

        // the final tree in the queue is the huffman tree that is built
        TreeNode<string?> huffmanTree = q.Dequeue();

        // create a dictionary mapping our symbols to binary codes by traversing the tree
        Dictionary<string, string> coding = new();



    }    
}
