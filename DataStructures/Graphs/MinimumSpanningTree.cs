using DataStructures.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.Graphs;
public class MinimumSpanningTree
{
    
    public static List<List<int>> Boruvka(List<List<int>> adj, List<List<int>> weights)
    {
        // Boruvka will maintain a list of connected components
        // at each step, we choose the minimum edge which spans across connected components
        List<List<int>> forestAdj = new List<List<int>>();
        foreach (var list in adj) forestAdj.Add(new List<int>());

        while (true)
        {
            List<List<int>> connectedComponents = GetConnectedComponents(forestAdj);
            (int, int)?[] bestEdge = new (int,int)?[connectedComponents.Count];

            Dictionary<int, int> vertexComponentMapping = new();
            // create a mapping from a vertex to its connected component index
            for (int i=0;i<connectedComponents.Count;i++)
            {
                for (int j = 0; j < connectedComponents[i].Count; j++)
                {
                    // node inside the connected component
                    int u = connectedComponents[i][j];
                    vertexComponentMapping[u] = i;
                }
            }

            // go through all the edges, see which edges map to different components
            for (int i = 0; i < adj.Count; i++)
            {
                for (int j = 0; j < adj[i].Count; j++)
                {
                    if (vertexComponentMapping[i] != vertexComponentMapping[adj[i][j]])
                    {
                        // this edge spans across components
                        // see if it beats the current weight with tie breaking rule
                        // tie breaking rule needs to impose a total order on edges to prevent cycles
                        // we will just compare node values of the first and second nodes in the edge
                        // think 3 vertices a,b,c with edge weights of all 1

                        int uComponent = vertexComponentMapping[i];
                        int vComponent = vertexComponentMapping[j];

                        int currEdgeWeight = weights[i][j];

                        (int Source, int Dest)? uChosenBest = bestEdge[uComponent];
                        (int Source, int Dest)? vChosenBest = bestEdge[vComponent];
                        
                        if (uChosenBest is null || 
                            weights[uChosenBest.Value.Source][uChosenBest.Value.Dest] > currEdgeWeight ||
                            (weights[uChosenBest.Value.Source][uChosenBest.Value.Dest]==currEdgeWeight && (i < uChosenBest.Value.Source && j < uChosenBest.Value.Dest))
                        {
                            bestEdge[uComponent] = (i, j);
                        }

                        if (vChosenBest is null || 
                            weights[vChosenBest.Value.Source][vChosenBest.Value.Dest] > currEdgeWeight ||
                            (weights[vChosenBest.Value.Source][vChosenBest.Value.Dest]==currEdgeWeight && (i < vChosenBest.Value.Source && j < vChosenBest.Value.Dest))
                        {
                            bestEdge[vComponent] = (i, j);
                        }
                    }
                }
            }

            // add the best edges to the forest
            bool areEdgesAdded = false;
            HashSet<(int, int)> edgesAdded = new(); 
            for (int i = 0; i < bestEdge.Length; i++)
            {
                (int, int)? edge = bestEdge[i];
                if (edge is null) continue;

                int source = Math.Min(edge.Value.Item1, edge.Value.Item2);
                int dest = Math.Max(edge.Value.Item1, edge.Value.Item2);

                if (edgesAdded.Contains((source, dest))) continue;

                forestAdj[source].Add(dest);
                forestAdj[dest].Add(source);

            }

            // process is done
            if (!areEdgesAdded) break;
        }

        return forestAdj;
    }


    public static List<List<int>> Kruskal(List<List<int>> adj, List<List<int>> weights)
    {
        // kruskal makes sets for each vertex
        // then sorts the edges by weight
        // see if adding the edge to the MST would create a cycle, if so dont add it
        // if not, add the edge to the forest

        List<List<int>> forestAdj = new();
        foreach (var list in adj) forestAdj.Add(new List<int>());
        List<DisjointSetNode<int>> sets = new(); 

        for(int i=0;i<adj.Count;i++)
        {
            sets.Add(DisjointSets.DisjointSet.MakeSet<int>(i));
        }

        List<(int U ,int V,int Weight)> edges= new(); 

        // sort edges by weight
        for(int i = 0; i < adj.Count; i++)
        {
            for (int j = 0; j < adj[i].Count; j++)
            {
                int u = i;
                int v = adj[i][j];
                
                // dont consider an edge twice
                if (u < v) continue;

                edges.Add((u, v, weights[i][j]));
            }
        }
        edges.Sort((edge1, edge2) => edge1.Weight.CompareTo(edge2.Weight));

        // go through the edges, and follow the algorithm to see if we make a cycle or not
        foreach ((int U, int V, int Weight) in edges)
        {
            if (DisjointSets.DisjointSet.FindSet(sets[U]) != DisjointSets.DisjointSet.FindSet(sets[V]))
            {
                // adding the edge will not create a cycle, add it to the forest
                forestAdj[U].Add(V);
                forestAdj[V].Add(U);

                DisjointSets.DisjointSet.Union(sets[U], sets[V]);
            }
        }

        return forestAdj;
    }


    public static List<List<int>> Prim(List<List<int>> adj, List<List<int>> weights)
    {
        List<List<int>> forestAdj = new();
        foreach (var list in adj) forestAdj.Add(new List<int>());

        // initialize a tree with 1 vertex
        // grow the tree by 1 edge with the smallest edge going to a vertex not already in the tree
        // repeat until all vertices covered

        (int Source, int Dest, int Weight)?[] cheapestEdges = new (int, int, int)?[adj.Count];

        bool[] explored = new bool[adj.Count];

        // create a priority queue of vertices and their min distances encountered so far
        PriorityQueue<int, int> vHeap = new();

        // choose the first vertex to start from
        vHeap.Enqueue(0, 0); 

        for (int i = 1; i < adj.Count; i++)
        {
            vHeap.Enqueue(1, int.MaxValue);
        }

        while(vHeap.Count > 0)
        {
            int node = vHeap.Dequeue();

            if (explored[node]) continue;

            explored[node] = true;

            for (int i = 0; i < adj[node].Count; i++)
            {
                int neighbor = adj[node][i];

                if (!explored[neighbor] && (cheapestEdges[neighbor] == null || cheapestEdges[neighbor]!.Value.Weight > weights[node][i]))
                {
                    cheapestEdges[neighbor] = (node, neighbor, weights[node][i]);
                }
            }
        }

        foreach((int Source, int Dest, int Weight)? item in cheapestEdges)
        {
            if (item != null)
            {
                forestAdj[item.Value.Source].Add(item.Value.Dest);
                forestAdj[item.Value.Dest].Add(item.Value.Source);
            }
        }

        return forestAdj;
    }

    private static List<List<int>> GetConnectedComponents(List<List<int>> adj)
    {
        List<List<int>> components = new();
        int dfsCount = -1;
        
        // every dfs traversal will create a connected component
        Action<int> preTraversalAction = (int node) =>
        {
            dfsCount++;
            components.Add(new List<int>());
        };
        
        // on visiting a node, we just add it to the connected components
        Action<int> visitAction = (int node) =>
        {
            components[dfsCount].Add(node);
        };

        // actual DFS traversal 
        Traversal.DFS(adj, preTraversalAction, visitAction, (int node) => { });

        return components;
    }
}
