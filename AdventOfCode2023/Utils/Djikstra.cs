namespace AdventOfCode.Utils;

public abstract class DjikstraDataSource
{
    public class Node(int distance, string identifier) : IEquatable<Node>
    {
        public int Distance = distance;
        public List<string> History = [];
        public readonly string Identifier = identifier;

        public bool Equals(Node? other)
        {
            if (other == null) { return false; }

            return Identifier.Equals(other.Identifier);
        }

        public override bool Equals(object? obj) => Equals(obj as Node);

        public override int GetHashCode() => Identifier.GetHashCode();
    }

    public string InitialIdentifier = "";

    public string DestinationIdentifier = "";

    public virtual Node InitialNode() => new(0, InitialIdentifier);

    public abstract IEnumerable<Node> NextNodes(Node currentNode);
}

public class Djikstra
{
    protected DjikstraDataSource DataSource;
    protected readonly PriorityQueue<DjikstraDataSource.Node, int> Unvisited = new();
    protected readonly HashSet<DjikstraDataSource.Node> Visited = [];

    public Djikstra(DjikstraDataSource dataSource)
    {
        DataSource = dataSource;
        Reset();
    }

    public virtual int OptimalRouteLength
    {
        get {
            if (Visited.Count == 0) { Reset(); ApplySearch(); }

            var destinationNode = Visited.Single(
                v => v.Identifier == DataSource.DestinationIdentifier);
            return destinationNode.Distance;
        }
    }

    public virtual IEnumerable<string> OptimalRouteHistory
    {
        get {
            if (Visited.Count == 0) { Reset(); ApplySearch(); }

            var destinationNode = Visited.Single(
                v => v.Identifier == DataSource.DestinationIdentifier);
            return destinationNode.History;
        }
    }

    public void Reset()
    {
        Visited.Clear();
        Unvisited.Clear();
        Unvisited.Enqueue(DataSource.InitialNode(), 0);
    }

    protected virtual void ApplySearch()
    {
        var currentNode = DataSource.InitialNode();
        while (currentNode.Identifier != DataSource.DestinationIdentifier) {
            currentNode = Unvisited.Dequeue();
            if (Visited.Contains(currentNode)) { continue; }
            Visited.Add(currentNode);

            foreach (DjikstraDataSource.Node nextNode in DataSource.NextNodes(currentNode)) {
                Unvisited.Enqueue(nextNode, nextNode.Distance);
            }
        }
    }
}
