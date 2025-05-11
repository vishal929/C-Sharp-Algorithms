using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.Shared;
public class DisjointSetNode<T>
{
    public T Value { get;  }

    public DisjointSetNode<T> Parent { get; set; } 

    public int Rank { get; set; }

    public DisjointSetNode(T Value)
    {
        this.Value = Value;
        Parent = this;
        Rank = 0;
    }

}
