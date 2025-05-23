using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.Graphs;
public class TopologicalSort
{
    public static IList<int> TopSort(IList<IList<int>> adjList)
    {
        // directed acyclic graph 

        // given a list of nodes, we want to return an ordering of nodes s.t the i-th node in the list does not have a dependency on any node after i
        // in other words, all the nodes that point to the i-th node must have already been completed

        // we continually find the node with the least indegree, and add that to the list

        // O(V+E) taking O(V) space
        int[] topSort = new int[adjList.Count];
        int topSortIdx = 0;

        Dictionary<int, int> inDegree = new();
        
        // O(E)
        for (int i = 0; i < adjList.Count; i++)
        {
            for (int j = 0; j < adjList[i].Count; j++)
            {
                if (!inDegree.ContainsKey(adjList[i][j]))
                {
                    inDegree[adjList[i][j]] = 1;
                }
                else
                {
                    inDegree[adjList[i][j]]++;
                }
            }
        }

        Stack<int> sources = new();

        for (int i = 0; i < adjList.Count; i++)
        {
            if (!inDegree.ContainsKey(i))
            {
                // no indegree
                sources.Push(i);
            }
        }

        while (sources.Count > 0)
        {
            int source = sources.Pop();

            topSort[topSortIdx] = source;
            topSortIdx++;

            for (int i = 0; i < adjList[source].Count; i++)
            {
                inDegree[adjList[source][i]]--;
                if (inDegree[adjList[source][i]] == 0)
                {
                    // add this to the list of sinks
                    sources.Push(adjList[source][i]);
                }
            }
        }

        // if our topSort length is not the same length as our adjList we have a cycle
        if (topSortIdx!=adjList.Count)
        {
            throw new InvalidOperationException("Provided graph has a cycle!");
        }

        return topSort;

    }
}
