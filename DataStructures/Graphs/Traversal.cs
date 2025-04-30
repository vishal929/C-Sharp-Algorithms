using DataStructures.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.Graphs;
public class Traversal
{
    public void DFS<T>(IList<GraphNode<T>> adjList, Action<GraphNode<T>> visitAction, Action<GraphNode<T>> alreadyVisitedAction)
    {
        HashSet<GraphNode<T>> visited = new HashSet<GraphNode<T>>();
        for (int i = 0; i < adjList.Count(); i++)
        {
            if (visited.Contains(adjList[i])) continue;

            // run DFS
            DFSHelper<T>(adjList[i], visited, visitAction, alreadyVisitedAction);
        }
    }

    private void DFSHelper<T>(GraphNode<T> source, HashSet<GraphNode<T>> visited, Action<GraphNode<T>> visitAction, Action<GraphNode<T>> alreadyVisitedAction)
    {
        Stack<GraphNode<T>> s = new();
        s.Push(source);

        while (s.Count > 0)
        {
            GraphNode<T> toVisit = s.Pop();
            if (visited.Contains(toVisit))
            {
                alreadyVisitedAction(toVisit);
                continue;
            }

            // visit this node
            visitAction(toVisit);
                        
            // push all neighbors
            foreach (GraphNode<T> neighbor in toVisit.Neighbors)
            {
                s.Push(neighbor);
            }
        }
    }

    public void BFS<T>(IList<GraphNode<T>> adjList, Action<GraphNode<T>> visitAction, Action<GraphNode<T>> alreadyVisitedAction)
    {
        HashSet<GraphNode<T>> visited = new HashSet<GraphNode<T>>();
        for (int i = 0; i < adjList.Count(); i++)
        {
            if (visited.Contains(adjList[i])) continue;

            // run DFS
            BFSHelper<T>(adjList[i], visited, visitAction, alreadyVisitedAction);
        }
    }

    private void BFSHelper<T>(GraphNode<T> source, HashSet<GraphNode<T>> visited, Action<GraphNode<T>> visitAction, Action<GraphNode<T>> alreadyVisitedAction)
    {
        Queue<GraphNode<T>> q = new();
        q.Enqueue(source);

        while (q.Count > 0)
        {
            GraphNode<T> toVisit = q.Dequeue();
            if (visited.Contains(toVisit))
            {
                alreadyVisitedAction(toVisit);
                continue;
            }

            // visit this node
            visitAction(toVisit);
                        
            // queue all neighbors
            foreach (GraphNode<T> neighbor in toVisit.Neighbors)
            {
                q.Enqueue(neighbor);
            }
        }
    }

}
