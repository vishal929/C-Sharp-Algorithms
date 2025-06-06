using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.Graphs;
public class MaxFlow 
{
    
    
    /// <summary>
    /// Runs the edmonds karp max flow algorithm
    /// </summary>
    /// <param name="adj">adjacency list</param>
    /// <param name="capacities">capacities based on indices in the adjacency list</param>
    /// <param name="source">source node</param>
    /// <param name="sink">sink node</param>
    /// <returns>the total flow value and the list of flows based on the indices in the adjacency list</returns>
    public static (int TotalFlow, List<List<int>> Flows) EdmondsKarp
        (List<List<int>> adj, List<List<int>> capacities, int source, int sink)
    {
        // using BFS, we can get the max flow using the ford fulkerson template in O(VE^2) time
        // proof is complicated but basically with a shortest augmenting path, we ensure that the distance of the augmenting
        // flows increases monotonically, so we have E iterations where each of the edges can be critical at most V/2 times --> O(VE)
        // so combining with the ford fulkerson template, we have O(VE^2)
        return FordFulkersonTemplate(adj, capacities, BFSHelper, source, sink); 
    }

    private static (int TotalFlow, List<List<int>> Flows) FordFulkersonTemplate
        (List<List<int>> adj, List<List<int>> capacities, Func<List<List<int>>,List<List<int>>,int,int,List<(int,int)>?> augmenting, int source, int sink)
    {
        // E in a dense graph will be >> V so O((E+V)f) --> O(Ef)
        // ford fulkerson provides a template to generate a max flow in O(Ef) time where f is the value of the maximum flow

        // O(V+E)
        List<List<int>> flows = new();
        for (int i = 0; i < capacities.Count; i++)
        {
            List<int> edgeFlows = new();
            for (int j = 0; j < capacities[i].Count; j++)
            {
                edgeFlows.Add(0);
            }
            flows.Add(edgeFlows);
        }
        

        // create the residual graph in O(V+E) time
        // the residual graph is defined as the network with capacities c_f(u,v) = c(u,v)-f(u,v)
        // and the flow is set in reverse
        // initially, we have all capacities as the edges in the residual graph
        List<List<int>> residual = capacities.Select(x => x.ToList()).ToList();
        List<List<int>> residualAdj = adj.Select(x => x.ToList()).ToList();

        // for each directed edge from u to v, add an edge from v to u with 0 flow (if there is already the opposite edge, dont add it)
        // O(E)
        for (int i = 0; i < residualAdj.Count; i++)
        {
            for (int j=0;j< residualAdj[i].Count; j++)
            {
                int v = residualAdj[i][j];
                // add only if the reverse doesnt already exist
                bool reverseExists = false;
                for (int z = 0; z < residualAdj[v].Count; z++)
                {
                    if (residualAdj[v][z] == i)
                    {
                        reverseExists = true;
                        break;
                    }
                }
                if (!reverseExists)
                {
                    residualAdj[v].Add(i);
                    residual[v].Add(0);
                }
                
            }
        }

        List<(int, int)>? augmentingPath = augmenting(residualAdj,residual,source,sink);
        
        // augmenting path takes O(V+E) time, but the loop might run f times, if we always just send 1 flow at a time
        while (augmentingPath != null)
        {
            // find the smallest capacity edge in this augmenting path and send that much flow to the entire path
            // O(E)
            int smallest = int.MaxValue;
            foreach ((int u, int vIdx) in augmentingPath)
            {
                smallest = Math.Min(smallest, residual[u][vIdx]);
            }

            // send this to the entire path
            // O(E)
            foreach ((int u, int vIdx) in augmentingPath)
            {
                int v = residualAdj[u][vIdx];

                // decrease the capacity by the flow amount
                residual[u][vIdx] -= smallest;

                // find the edge from vIdx, to u and increase the flow amount in the residual
                for (int i = 0; i < residualAdj[v].Count; i++)
                {
                    if (residualAdj[v][i] == u)
                    {
                        // found the reverse edge
                        residual[v][i] += smallest;
                        break;
                    }
                }

                // send flow along u to v
                flows[u][vIdx] += smallest;

                // find a new augmenting path
                augmentingPath = augmenting(residualAdj, residual, source, sink);
            }
        }

        int maxFlow = 0;
        // add up the max flow from the source
        // O(E)
        for (int i = 0; i < flows[source].Count; i++)
        {
            maxFlow += flows[source][i];
        }

        return (maxFlow, flows);
    }

    private static List<(int,int)>? BFSHelper(List<List<int>> residualAdj,List<List<int>> residual,int source,int sink)
    {
        // use BFS to find a shortest non-zero flow from source to sink in the residual graph (O(V+E))
        // want to support bidirectional graphs, so maintain a "seen" set
        HashSet<int> seen = new();
        Queue<int> q = new();
        q.Enqueue(source);

        int[] pred = new int[residualAdj.Count];
        pred[source] = source;

        bool pathFound = false;

        while (q.Count > 0)
        {
            int node = q.Dequeue();

            if (node == sink)
            {
                // got a shortest path to our sink, just break
                pathFound = true;
                break;
            }
            
            for (int i = 0; i < residualAdj[node].Count; i++)
            {
                if (residual[node][i] > 0 && !seen.Contains(residualAdj[node][i]))
                {
                    // possible neighbor
                    pred[residualAdj[node][i]] = node;
                    q.Enqueue(residualAdj[node][i]);
                    seen.Add(residualAdj[node][i]);
                }
            }
            seen.Add(node);
        }
        
        // indicate no augmenting path exists
        if (!pathFound) return null;

        // build our edges using pred
        List<(int, int)> edges = new();
        List<int> order = new();
        int item = sink;
        while(item!=pred[item])
        {
            order.Add(item);
            item = pred[item];
        }
        // add the sink
        order.Add(item);

        order.Reverse();

        // now order is like source --- v1 ---- v2 --- ... --- sink
        
        // building this list takes O(E) time (we wont scan the same edge twice)
        for (int i = 0; i < order.Count-1; i++)
        {
            int u = order[i];
            int v = order[i + 1];

            for (int j = 0; j < residualAdj[u].Count; j++)
            {
                if (residualAdj[u][j] == v)
                {
                    edges.Add((u, j));
                    break;
                }
            }
        }
        return edges;

    }
}
