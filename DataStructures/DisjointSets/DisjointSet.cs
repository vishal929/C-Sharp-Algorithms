using DataStructures.Shared;

namespace DataStructures.DisjointSets;
public class DisjointSet
{

    public DisjointSetNode<T> MakeSet<T>(T item)
    {
        return new DisjointSetNode<T>(item);

    }

    public void Union<T>(DisjointSetNode<T> first, DisjointSetNode<T> second)
    {
        Link(FindSet<T>(first), FindSet<T>(second)); 
    }

    public DisjointSetNode<T> FindSet<T>(DisjointSetNode<T> item)
    {
        if (item != item.Parent)
        {
            // path compression
            item.Parent = FindSet<T>(item.Parent);
        }
        return item.Parent;
    }

    private void Link<T>(DisjointSetNode<T> first, DisjointSetNode<T> second)
    {
        if (first.Rank > second.Rank)
        {
            second.Parent = first;
        }
        else
        {
            first.Parent = second;
            if (first.Rank == second.Rank)
            {
                second.Rank++;
            }
        }
    }
}
