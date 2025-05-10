using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.Graphs;
public class AllPairsShortestPaths
{

    public int[,] FloydWarshall(IList<IList<int>> adjMatrix, IDictionary<int, IDictionary<int, double>> weights)
    {
        // the idea is that the shortest path from v to w incorporates the shortest path from an intermediate node k to w
        // if it didnt, then we could just replace the intermediate nodes path from k to w with the shortest one and receive a shorter path

        // therefore, for n vertices, we have n-1 iterations
        // at each iteration we compute the shortest path from v to w using at most i nodes
        // basically for a given v and a given w, we loop through all nodes and see if there is some k s.t taking v to k to w makes the path shorter
        // O(V^3)

        // we start with all distances as the max edge length
        // and initialize the distance to itself to be 0
        double maxEdgeLength = adjMatrix.Max(x => x.Max());

        Dictionary<int, Dictionary<int, IList<int>>> paths = new();

        // VxV adjacency matrix 
        double[,] distances = new double[adjMatrix.Count, adjMatrix.Count];

        // prev[i,j] holds the second-to-last node on the path from i to j
        // we return this array so that the caller can reconstruct the shortest path from u to v in O(V) time
        int[,] prev = new int[adjMatrix.Count, adjMatrix.Count];

        for (int i = 0; i < adjMatrix.Count; i++)
        {
            for (int j = 0; j < adjMatrix.Count; j++)
            {
                if (weights.ContainsKey(i) && weights[i].ContainsKey(j))
                {
                    distances[i, j] = weights[i][j];
                }
                else
                {
                    distances[i, j] = maxEdgeLength;
                }
                prev[i, j] = i;
            }
            distances[i, i] = 0;
        }

        for (int i = 0; i < adjMatrix.Count; i++)
        {
            for (int j = 0; j < adjMatrix.Count; j++)
            {
                for (int k = 0; k < adjMatrix.Count; k++)
                {
                    // if the distance from i to k + k to j is less than the distance from i to j, update
                    if (distances[i, k] + distances[k, j] < distances[i, j])
                    {
                        distances[i, j] = distances[i, k] + distances[k, j];
                        // update prev
                        prev[i, j] = prev[k, j];
                    }
                }
            }
        }
        return prev;
    }
}
