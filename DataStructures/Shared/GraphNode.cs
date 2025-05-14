using DataStructures.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.Shared;
public class GraphNode<T>:IGraphNode<T>
{
    public IList<IGraphNode<T>> Neighbors { get; }

    public T Value { get; }

    public GraphNode(T Value, IList<GraphNode<T>> Neighbors)
    {
        this.Neighbors = Neighbors.Cast<IGraphNode<T>>().ToList();
        this.Value = Value;

    }
}
