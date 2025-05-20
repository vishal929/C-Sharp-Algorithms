using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.Graphs;
public class TopologicalSort
{
    public IList<int> TopSort(IList<IList<int>> adjList)
    {
        // directed acyclic graph 

        // given a list of nodes, we want to return an ordering of nodes s.t the i-th node in the list does not have a dependency on any node after i
        // in other words, all the nodes that point to the i-th node must have already been completed

        // so we do DFS, when we visit the node, we prepend the node to the list during backtracking
        // this ensures that when we visit a node, the dependencies have already been prepended to the list

        // O(V+E) taking O(V) space
        IList<int> topSort = new List<int>();

        Stack<int> s = new();
        HashSet<int> visited = new();
        
        for (int i = 0; i < adjList.Count; i++)
        {
            if (visited.Contains(i)) continue;

            s.Push(i);

            Dictionary<int, int> numVisited = new();


            while (s.Count > 0)
            {
                int node = s.Pop();

                if (numVisited.ContainsKey(node))
                {
                    // backtrack, but if the stack expansion was not finalized, need to report a cycle
                    if (numVisited[node] > 1)
                    {
                        // visited again,
                        throw new InvalidOperationException("DAG has a cycle!");
                    }
                    else
                    {
                        // this is the second time we are visiting the node, add it to the list
                        topSort.Add(node);
                        numVisited[node]++;
                        continue;
                    }

                }

                // add back to stack and expand neighbors
                s.Push(node);
                numVisited[node]=1;

                foreach (int neighbor in adjList[node])
                {
                    s.Push(neighbor);
                }
                
                // dont want to consider this in other DFS calls
                visited.Add(node);

            }
        }

        return topSort;

    }
}
