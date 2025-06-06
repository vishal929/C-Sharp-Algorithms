using DataStructures.Graphs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.Tests.Graphs;
public class TestMaxFlow
{
    [Fact]
    public void TestEdmondsKarp()
    {
        (List<List<int>> adj, List<List<int>> cap) = BuildGraph();

        (int maxFlow, List<List<int>> flows) = MaxFlow.EdmondsKarp(adj, cap,0,5);

        Assert.Equal(23, maxFlow);

        // assert that each edge in the flow is less than the capacity of the edge 

        for (int i = 0; i < flows.Count; i++)
        {
            for (int j = 0; j < flows[i].Count; j++)
            {
                Assert.True(flows[i][j] <= cap[i][j]);
            }
        }

    }

    public (List<List<int>> AdjList, List<List<int>> Capacities) BuildGraph()
    {
        List<List<int>> adj = new();
        List<List<int>> cap = new();

        for (int i = 0; i < 6; i++)
        {
            adj.Add(new List<int>());
            cap.Add(new List<int>());
        }

        adj[0].Add(1);
        cap[0].Add(16);

        adj[0].Add(2);
        cap[0].Add(13);

        adj[1].Add(2);
        cap[1].Add(10);

        adj[1].Add(3);
        cap[1].Add(12);

        adj[2].Add(1);
        cap[2].Add(4);

        adj[2].Add(4);
        cap[2].Add(14);

        adj[3].Add(2);
        cap[3].Add(9);

        adj[3].Add(5);
        cap[3].Add(20);

        adj[4].Add(3);
        cap[4].Add(7);

        adj[4].Add(5);
        cap[4].Add(4);

        return (adj, cap);
    }
}
