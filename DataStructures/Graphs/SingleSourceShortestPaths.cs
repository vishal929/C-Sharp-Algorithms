using DataStructures.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.Graphs;
public class SingleSourceShortestPaths
{
    public (double[] Dist,int[] Prev) BellmanFord<T>(IList<IList<int>> adjList, IList<IList<double>> weights, int source)
    {
        // idea is that from a source, instead of getting the smallest distance vertex and updating based on edges,
        // we just update on all edges, v-1 times.

        double[] dist = new double[adjList.Count];
        int[] prev = new int[adjList.Count];
        prev[source] = source;

        // O(V)
        for (int i = 0; i < adjList.Count; i++)
        {
            if (i == source)
            {
                dist[i] = 0;
            }
            else
            {
                dist[i] = double.PositiveInfinity;
            }
        }
        
        // O(V*E)
        for (int i = 0; i < adjList.Count-1; i++)
        {
            if (dist[i] == double.PositiveInfinity) continue;

            for (int j = 0; j < adjList[i].Count; j++)
            {
                int neighbor = adjList[i][j];

                double weight = weights[i][j];

                double altDist = dist[i] + weight;
                if (altDist < dist[neighbor])
                {
                    // update
                    dist[neighbor] = altDist;
                    prev[neighbor] = i;
                }
            }
        }

        // check for negative weight cycles (negative cycle means no shortest path (can always just take the cycle)
        
        return (dist, prev);
    }

    public (double[] Dist,int[] Prev) 
        Dijkstra<T>(IList<IList<int>> adjList, IList<IList<double>> weights, int source)
    {
        // dijkstras maintains a priority queue of edges that we could take
        double[] dist = new double[adjList.Count];
        int[] prev = new int[adjList.Count];
        
        // set the prev of the source to itself
        prev[source] = source;
        
        HashSet<int> done = new();
        PriorityQueue<int, double> unvisited = new();

        // O(V)
        for (int i = 0; i < adjList.Count; i++)
        {
            if (i == source)
            {
                dist[i] = 0;
            }
            else
            {
                dist[i] = double.PositiveInfinity;
            }

            unvisited.Enqueue(i, dist[i]);
        }
        
        // (O(VLgV + ELgV) because we dequeue every vertex in VLgV and for each edge we might enqueue a vertex, ELgV
        // ---> O((E+V)LgV)
        while(unvisited.Count > 0)
        {
            int  node = unvisited.Dequeue();
            if (done.Contains(node)) continue;

            IList<int> edges = adjList[node];
            for (int i=0;i<edges.Count;i++)
            {
                int neighbor = edges[i];

                double weight = weights[node][neighbor];
                if (done.Contains(neighbor)) continue;
                double altDist = dist[node] + weight;
                if (altDist < dist[neighbor])
                {
                    prev[neighbor] = node;
                    dist[neighbor] = altDist;
                    unvisited.Enqueue(neighbor, altDist);
                }
            }
            done.Add(node);
        }
        return (dist, prev);
    }
}
