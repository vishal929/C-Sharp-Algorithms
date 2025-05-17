using DataStructures.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.Graphs;
public class Traversal
{
    public void DFS(IList<IList<int>> adjList, Action<int> visitAction, Action<int> alreadyVisitedAction)
    {
        HashSet<int> visited = new();
        for (int i = 0; i < adjList.Count(); i++)
        {
            if (visited.Contains(i)) continue;

            // run DFS
            DFSHelper(i,adjList,visited, visitAction, alreadyVisitedAction);
        }
    }

    private void DFSHelper(int source, IList<IList<int>> adjList, HashSet<int> visited, Action<int> visitAction, Action<int> alreadyVisitedAction)
    {
        Stack<int> s = new();
        s.Push(source);

        while (s.Count > 0)
        {
            int toVisit = s.Pop();
            if (visited.Contains(toVisit))
            {
                alreadyVisitedAction(toVisit);
                continue;
            }

            // visit this node
            visitAction(toVisit);
                        
            // push all neighbors
            foreach (int neighbor in adjList[toVisit])
            {
                s.Push(neighbor);
            }

            // set visited
            visited.Add(toVisit);
        }
    }

    public void BFS<T>(IList<IList<int>> adjList, Action<int> visitAction, Action<int> alreadyVisitedAction)
    {
        HashSet<int> visited = new();
        for (int i = 0; i < adjList.Count(); i++)
        {
            if (visited.Contains(i)) continue;

            // run BFS
            BFSHelper(i,adjList, visited, visitAction, alreadyVisitedAction);
        }
    }

    private void BFSHelper(int source, IList<IList<int>> adjList, HashSet<int> visited, Action<int> visitAction, Action<int> alreadyVisitedAction)
    {
        Queue<int> q = new();
        q.Enqueue(source);

        while (q.Count > 0)
        {
            int toVisit = q.Dequeue();
            if (visited.Contains(toVisit))
            {
                alreadyVisitedAction(toVisit);
                continue;
            }

            // visit this node
            visitAction(toVisit);
                        
            // queue all neighbors
            foreach (int neighbor in adjList[toVisit])
            {
                q.Enqueue(neighbor);
            }

            // set visited status
            visited.Add(toVisit);
        }
    }

}
