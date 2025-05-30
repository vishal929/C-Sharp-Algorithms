using DataStructures.Graphs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.Tests.Graphs;
public class TestSingleSourceShortestPaths
{

    [Fact]
    public void TestDijkstra()
    {
        // run shortest paths from 0, and reconstruct a path as a test case
        (List<List<int>> adj, List<List<double>> weights) = BuildGraph();
        (double[] dist, int [] prev) = SingleSourceShortestPaths.Dijkstra(adj, weights, 0);

        RunAssertionsPositiveGraph(dist, prev);
    }


    [Fact]
    public void TestBellmanFord()
    {
        // run shortest paths from 0, and reconstruct a path as a test case
        (List<List<int>> adj, List<List<double>> weights) = BuildGraph();
        (double[] dist, int [] prev) = SingleSourceShortestPaths.BellmanFord(adj, weights, 0);

        RunAssertionsPositiveGraph(dist, prev);

        (adj, weights) = BuildGraphNegativeCycle();

        Assert.Throws<InvalidOperationException>(() => SingleSourceShortestPaths.BellmanFord(adj, weights, 0));

        (adj, weights) = BuildGraphNegativeWeight();

        // start from 1 as our source in the grpah with negative weights
        (dist, prev) = SingleSourceShortestPaths.BellmanFord(adj, weights, 1);

        RunAssertionsNegativeGraph(dist, prev);

    }

    private (List<List<int>> Adj, List<List<double>> weights) BuildGraphNegativeWeight()
    {
        List<List<int>> adj = new List<List<int>>();
        List<List<double>> weights = new List<List<double>>();
        for (int i = 0; i < 5; i++)
        {
            adj.Add(new List<int>());
            weights.Add(new List<double>());
        }

        // directed graph with negative edges
        adj[0].Add(2);
        weights[0].Add(5);
        adj[0].Add(3);
        weights[0].Add(4);

        adj[1].Add(0);
        weights[1].Add(4);
        adj[1].Add(3);
        weights[1].Add(7);
        adj[1].Add(2);
        weights[1].Add(3);

        adj[2].Add(3);
        weights[2].Add(3);
        adj[2].Add(4);
        weights[2].Add(2);

        adj[3].Add(0);
        weights[3].Add(-3);

        adj[4].Add(3);
        weights[4].Add(-4);

        return (adj, weights);

    }

    private (List<List<int>> Adj, List<List<double>> weights) BuildGraphNegativeCycle()
    {
        List<List<int>> adj = new List<List<int>>();
        List<List<double>> weights = new List<List<double>>();
        for (int i = 0; i < 4; i++)
        {
            adj.Add(new List<int>());
            weights.Add(new List<double>());
        }
        
        // edges from 0 to 1 with weight 6
        adj[0].Add(1);
        adj[1].Add(0);
        weights[0].Add(6);
        weights[1].Add(6);

        // edge from 1 to 2 with weight 2
        adj[1].Add(2);
        adj[2].Add(1);
        weights[2].Add(2);
        weights[1].Add(2);
        
        // edge from 2 to 3 with weight -2
        adj[2].Add(3);
        adj[3].Add(2);
        weights[2].Add(-2);
        weights[3].Add(-2);

        // edge from 3 to 1 with weight -1
        adj[3].Add(1);
        adj[1].Add(3);
        weights[3].Add(-1);
        weights[1].Add(-1);

        
        return (adj, weights);

    }


    private (List<List<int>> Adj, List<List<double>> weights) BuildGraph()
    {
        List<List<int>> adj = new List<List<int>>();
        List<List<double>> weights = new List<List<double>>();
        for (int i = 0; i < 7; i++)
        {
            adj.Add(new List<int>());
            weights.Add(new List<double>());
        }
        
        // edges from 0 to 2 with weight 6
        adj[0].Add(2);
        adj[2].Add(0);
        weights[0].Add(6);
        weights[2].Add(6);

        // edge from 0 to 1 with weight 2
        adj[0].Add(1);
        adj[1].Add(0);
        weights[0].Add(2);
        weights[1].Add(2);
        
        // edge from 1 to 3 with weight 5
        adj[1].Add(3);
        adj[3].Add(1);
        weights[1].Add(5);
        weights[3].Add(5);

        // edge from 2 to 3 with weight 8
        adj[2].Add(3);
        adj[3].Add(2);
        weights[2].Add(8);
        weights[3].Add(8);

        // edge from 3 to 4 with weight 10
        adj[3].Add(4);
        adj[4].Add(3);
        weights[3].Add(10);
        weights[4].Add(10);

        // edge from 3 to 5 with weight 15
        adj[3].Add(5);
        adj[5].Add(3);
        weights[3].Add(15);
        weights[5].Add(15);

        // edge from 5 to 6 with weight 6
        adj[5].Add(6);
        adj[6].Add(5);
        weights[5].Add(6);
        weights[6].Add(6);

        // edge from 4 to 6 with weight 6
        adj[4].Add(6);
        adj[6].Add(4);
        weights[4].Add(2);
        weights[6].Add(2);

        return (adj, weights);

    }

    private void RunAssertionsNegativeGraph(double[] dist, int[] prev)
    {
        // assert our distances from idx 1
        Assert.Equal(0, dist[1], 0.001);
        Assert.Equal(-2, dist[0], 0.001);
        Assert.Equal(3, dist[2], 0.001);
        Assert.Equal(1, dist[3], 0.001);
        Assert.Equal(5, dist[4], 0.001);

        // build a sample path
        // get a shortest path using prev
        int last = 3;
        List<int> shortPath = new();
        shortPath.Add(last);
        while (last != 1)
        {
            last = prev[last];
            shortPath.Add(last);
        }

        shortPath.Reverse();

        Assert.Equal(1, shortPath[0]);
        Assert.Equal(2, shortPath[1]);
        Assert.Equal(4, shortPath[2]);
        Assert.Equal(3, shortPath[3]);
    }


    private void RunAssertionsPositiveGraph(double[] dist, int[] prev)
    {
        // assert our distances from 0
        Assert.Equal(0, dist[0],0.001);
        Assert.Equal(2, dist[1],0.001);
        Assert.Equal(6, dist[2], 0.001);
        Assert.Equal(7, dist[3], 0.001);
        Assert.Equal(17, dist[4], 0.001);
        Assert.Equal(22, dist[5], 0.001);
        Assert.Equal(19, dist[6], 0.001);

        // get a shortest path using prev
        int last = 4;
        List<int> shortPath = new();
        shortPath.Add(last);
        while (last != 0)
        {
            last = prev[last];
            shortPath.Add(last);
        }

        shortPath.Reverse();

        Assert.Equal(0, shortPath[0]);
        Assert.Equal(1, shortPath[1]);
        Assert.Equal(3, shortPath[2]);
        Assert.Equal(4, shortPath[3]);
    }
}
