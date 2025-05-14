using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.Shared.Interfaces;
public interface IGraphNode<T>
{
    IList<IGraphNode<T>> Neighbors { get; }

    T Value { get; }
}
