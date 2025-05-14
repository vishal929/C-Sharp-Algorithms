using DataStructures.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.Shared;
public class WeightedGraphNode<T>:GraphNode<T>
{
    public IList<double> Weights { get; }

    public WeightedGraphNode(T Value, IList<GraphNode<T>> Neighbors, IList<double> Weights):base(Value,Neighbors)
    {
        this.Weights = Weights;
    }
}
