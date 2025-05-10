using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.Shared;
public class WeightedGraphNode<T>(T Value, IList<GraphNode<T>> Neighbors, IList<double> Weights):GraphNode<T>(Value,Neighbors)
{
    public IList<double> Weights = Weights;
}
