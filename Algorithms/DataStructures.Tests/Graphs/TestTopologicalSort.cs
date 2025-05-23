using DataStructures.Graphs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.Tests.Graphs;
public class TestTopologicalSort
{

    [Fact]
    public void TestTopSort()
    {
        // sample test case 
        List<IList<int>> edges = new List<IList<int>>();
        for (int i = 0; i < 6; i++) edges.Add(new List<int>());

        // 0 is the sink
        edges[5].Add(0);
        edges[5].Add(2);
        edges[4].Add(0);
        edges[4].Add(1);
        edges[2].Add(3);
        edges[3].Add(1);

        IList<int> topOrder = TopologicalSort.TopSort(edges);

        // we should include all the nodes in the top sort
        Assert.Equal(edges.Count, topOrder.Count);

        Assert.True(VerifyTopOrder(topOrder, edges));

        // provide some graph with a cycle

        List<IList<int>> cycleEdges = new List<IList<int>>();
        for (int i = 0; i < 3; i++) cycleEdges.Add(new List<int>());

        cycleEdges[0].Add(1);
        cycleEdges[1].Add(2);
        cycleEdges[2].Add(0);

        Assert.Throws<InvalidOperationException>(() => TopologicalSort.TopSort(cycleEdges));

        List<IList<int>> anotherCycleEdges = new List<IList<int>>();
        for (int i = 0; i < 6; i++) anotherCycleEdges.Add(new List<int>());

        anotherCycleEdges[0].Add(1);
        anotherCycleEdges[1].Add(2);
        anotherCycleEdges[2].Add(3);
        anotherCycleEdges[3].Add(4);
        anotherCycleEdges[4].Add(5);
        anotherCycleEdges[5].Add(3);

        Assert.Throws<InvalidOperationException>(() => TopologicalSort.TopSort(anotherCycleEdges));


    }

    private bool VerifyTopOrder(IList<int> topSort, IList<IList<int>> edges)
    {
        HashSet<int> completed = new();
        for (int i = 0; i < topSort.Count; i++)
        {
            // for every node that we need to execute, all the directed edges into it must have already been done
            for (int u = 0; u < edges.Count; u++)
            {
                for (int v = 0; v < edges[u].Count; v++)
                {
                    if (edges[u][v]== topSort[i])
                    {
                        // see if u is done, if not then we have a problem
                        if (!completed.Contains(u)) return false;
                    }
                }
            }
            completed.Add(topSort[i]);
        }
        return true;
    }
}
