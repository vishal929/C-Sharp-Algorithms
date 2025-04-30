using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.Shared;
public class GraphNode<T>(T Value, IList<GraphNode<T>> Neighbors)
{
    public T Value { get; set; } = Value;

    public IList<GraphNode<T>> Neighbors { get; set; } = Neighbors;
}
