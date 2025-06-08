using DataStructures.Graphs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.Tests.Graphs;
public class TestMinimumSpanningTree
{
    [Fact]
    public void TestMST()
    {
        (List<List<int>> adj, List<List<int>> weights) = BuildTree();

        List<List<int>> mstBoruvka = MinimumSpanningTree.Boruvka(adj, weights);

        Dictionary<int, Dictionary<int, int>> weightMap = BuildWeightMap(adj, weights);

        VerifyMST(mstBoruvka, weightMap);


        List<List<int>> mstKruskal = MinimumSpanningTree.Kruskal(adj, weights);
        VerifyMST(mstKruskal, weightMap);

        List<List<int>> mstPrim = MinimumSpanningTree.Prim(adj, weights);
        VerifyMST(mstPrim, weightMap);

    }



    private static (List<List<int>> Adj, List<List<int>> Weights) BuildTree()
    {
        List<List<int>> adj = new();
        List<List<int>> weights = new();

        for (int i = 0; i < 6; i++)
        {
            adj.Add(new List<int>());
            weights.Add(new List<int>());
        }

        adj[0].Add(1);
        adj[1].Add(0);
        weights[0].Add(1);
        weights[1].Add(1);

        adj[0].Add(3);
        adj[3].Add(0);
        weights[0].Add(3);
        weights[3].Add(3);

        adj[1].Add(2);
        adj[2].Add(1);
        weights[1].Add(6);
        weights[2].Add(6);

        adj[1].Add(3);
        adj[3].Add(1);
        weights[1].Add(5);
        weights[3].Add(5);

        adj[1].Add(4);
        adj[4].Add(1);
        weights[1].Add(1);
        weights[4].Add(1);

        adj[2].Add(4);
        adj[4].Add(2);
        weights[2].Add(5);
        weights[4].Add(5);

        adj[2].Add(5);
        adj[5].Add(2);
        weights[2].Add(2);
        weights[5].Add(2);

        adj[3].Add(4);
        adj[4].Add(3);
        weights[3].Add(1);
        weights[4].Add(1);

        adj[4].Add(5);
        adj[5].Add(4);
        weights[4].Add(4);
        weights[5].Add(4);

        return (adj, weights);
    }

    private Dictionary<int,Dictionary<int,int>> BuildWeightMap(List<List<int>> adj, List<List<int>> weights)
    {
        Dictionary<int, Dictionary<int, int>> map = new();

        for (int i=0;i<adj.Count; i++)
        {
            for (int j = 0; j < adj[i].Count; j++)
            {
                int source = i;
                int dest = adj[i][j];
                int weight = weights[i][j];

                if (!map.ContainsKey(source))
                {
                    map[source] = new Dictionary<int, int>();
                }
                map[source][dest] = weight;
                
                if (!map.ContainsKey(dest))
                {
                    map[dest] = new Dictionary<int, int>();
                }
                map[dest][source] = weight;
                

            }
        }
        return map;
    }

    private static void VerifyMST(List<List<int>> mst, Dictionary<int,Dictionary<int,int>> weightMap)
    {
        // total mst will be of weight 9
        int mstWeight = 0;
        for (int i = 0; i < mst.Count; i++)
        {
            for (int j=0;j< mst[i].Count; j++)
            {
                int source = i;
                int dest = mst[i][j];

                // dont double count edges
                if (source >= dest) continue;
                int weight = weightMap[source][dest];

                mstWeight += weight;
            }
        }

        Assert.Equal(9, mstWeight);

        // verify that the edges we expect are in the MST

        Assert.Contains(1,mst[0]);

        Assert.Contains(0, mst[1]);
        Assert.Contains(4, mst[1]);

        Assert.Contains(5, mst[2]);

        Assert.Contains(4, mst[3]);

        Assert.Contains(1, mst[4]);
        Assert.Contains(3, mst[4]);
        Assert.Contains(5, mst[4]);

        Assert.Contains(4, mst[5]);
        Assert.Contains(2, mst[5]);
    } 

}
