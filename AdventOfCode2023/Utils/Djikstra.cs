namespace AdventOfCode.Utils;

public abstract class DjikstraDataSource
{
    public class Node(int distance, Point2D point) : IEquatable<Node>
    {
        public int Distance = distance;
        public List<Point2D> History = [];
        public readonly Point2D Point = point;

        public bool Equals(Node? other)
        {
            if (other == null) { return false; }

            return Point == other.Point;
        }

        public override bool Equals(object? obj) => Equals(obj as Node);

        public override int GetHashCode() => Point.GetHashCode();
    }

    public abstract int Width();

    public abstract int Height();

    public Point2D InitialPoint = new(0, 0);

    public Point2D DestinationPoint = new(0, 0);

    public virtual Node InitialNode() => new(0, InitialPoint);

    public virtual int Distance(Point2D from, Point2D to) =>
        Math.Max(Math.Abs(to.X - from.X), Math.Abs(to.Y - from.Y));

    protected List<CompassDirection> Directions = [
        CompassDirection.North, CompassDirection.East,
        CompassDirection.South, CompassDirection.West,
    ];

    public abstract IEnumerable<Node> NextNodes(Node currentNode);
}

public class Djikstra(DjikstraDataSource dataSource)
{
    protected DjikstraDataSource DataSource = dataSource;
    protected readonly PriorityQueue<DjikstraDataSource.Node, int> Unvisited = new();
    protected readonly HashSet<DjikstraDataSource.Node> Visited = [];

    public virtual int RouteLength
    {
        get {
            if (Visited.Count == 0) { ApplySearch(); }

            var destinationNode = Visited.Single(v => v.Point == DataSource.DestinationPoint);
            return destinationNode.Distance;
        }
    }

    public virtual void OutputPath()
    {
        if (Visited.Count == 0) { ApplySearch(); }

        var destinationNode = Visited.Single(v => v.Point == DataSource.DestinationPoint);
        for (int y = 0; y < DataSource.Height(); y++) {
            for (int x = 0; x < DataSource.Width(); x++) {
                Console.Write(destinationNode.History.Contains(new(x, y)) ? '#' : '.');
            }
            Console.WriteLine();
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
        while (currentNode.Point != DataSource.DestinationPoint) {
            currentNode = Unvisited.Dequeue();
            if (Visited.Contains(currentNode)) { continue; }

            foreach (DjikstraDataSource.Node nextNode in DataSource.NextNodes(currentNode)) {
                Unvisited.Enqueue(nextNode, nextNode.Distance);
            }

            Visited.Add(currentNode);
        }
    }
}
