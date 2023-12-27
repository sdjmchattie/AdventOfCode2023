namespace AdventOfCode.Utils;

public interface IDjikstraDataSource
{
    public int Width();
    public int Height();
    public int Distance(Point2D from, Point2D to);
}

public class Djikstra
{
    protected class Node(int distance, Point2D point) : IEquatable<Node>
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

    private Point2D initialPoint = new(0, 0);
    public Point2D InitialPoint
    {
        get { return initialPoint; }
        set {
            if (initialPoint == value) { return; }
            initialPoint = value;
            Reset();
        }
    }

    private Point2D destinationPoint = new(0, 0);
    public Point2D DestinationPoint
    {
        get { return destinationPoint; }
        set {
            if (destinationPoint == value) { return; }
            destinationPoint = value;
            Reset();
        }
    }

    protected IDjikstraDataSource DataSource;
    protected readonly PriorityQueue<Node, int> Unvisited = new();
    protected readonly HashSet<Node> Visited = [];

    public Djikstra(IDjikstraDataSource dataSource) {
        DataSource = dataSource;
        DestinationPoint = new(DataSource.Width() - 1, DataSource.Height() - 1);
    }

    public Djikstra(Djikstra other) {
        DataSource = other.DataSource;
        DestinationPoint = other.DestinationPoint;
    }

    public int RouteLength
    {
        get {
            if (Visited.Count == 0) { ApplySearch(); }

            var destinationNode = Visited.Single(v => v.Point == destinationPoint);
            return destinationNode.Distance;
        }
    }

    public void OutputPath()
    {
        if (Visited.Count == 0) { ApplySearch(); }

        var destinationNode = Visited.Single(v => v.Point == destinationPoint);
        for (int y = 0; y < DataSource.Height(); y++) {
            for (int x = 0; x < DataSource.Width(); x++) {
                Console.Write(destinationNode.History.Contains(new(x, y)) ? '#' : '.');
            }
            Console.WriteLine();
        }
    }

    protected virtual Node InitialCurrentNode =>
        new(0, new(InitialPoint.X, InitialPoint.Y));

    protected void Reset()
    {
        Visited.Clear();
        Unvisited.Clear();
        Unvisited.Enqueue(InitialCurrentNode, 0);
    }

    protected List<CompassDirection> Directions = [
        CompassDirection.North, CompassDirection.East,
        CompassDirection.South, CompassDirection.West,
    ];

    protected bool OutOfBounds(Point2D point)
    {
        return point.X < 0 || point.X >= DataSource.Width() ||
            point.Y < 0 || point.Y >= DataSource.Height();
    }

    protected virtual IEnumerable<Node> NextNodes(Node currentNode)
    {
        foreach (CompassDirection newDirection in Directions) {
            var newPoint = currentNode.Point.OffsetBy(newDirection.GetOffset());

            if (OutOfBounds(newPoint)) { continue; }

            var newDistance = currentNode.Distance + DataSource.Distance(currentNode.Point, newPoint);

            yield return new Node(newDistance, newPoint);
        }
    }

    private void ApplySearch()
    {
        var currentNode = InitialCurrentNode;
        while (currentNode.Point != DestinationPoint) {
            currentNode = Unvisited.Dequeue();
            if (Visited.Contains(currentNode)) { continue; }

            foreach (Node nextNode in NextNodes(currentNode)) {
                nextNode.History.AddRange(currentNode.History);
                nextNode.History.Add(nextNode.Point);
                Unvisited.Enqueue(nextNode, nextNode.Distance);
            }

            Visited.Add(currentNode);
        }
    }
}
