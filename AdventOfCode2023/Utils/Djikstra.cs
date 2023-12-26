namespace AdventOfCode.Utils;

public class Djikstra
{
    protected class Node(Point2D point) : IEquatable<Node>
    {
        public readonly Point2D Point = point;

        public List<Point2D> History = [];

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

    protected Grid2D Map;
    protected Node CurrentNode = new(new(0, 0));
    protected readonly PriorityQueue<Node, int> Unvisited = new();
    protected readonly HashSet<Node> Visited = [];
    protected readonly Dictionary<Point2D, int> Distances = [];

    public Djikstra(Grid2D map) {
        Map = map;
        DestinationPoint = new(Map.Width - 1, Map.Height - 1);
    }

    public Djikstra(Djikstra other) {
        Map = other.Map;
        DestinationPoint = other.DestinationPoint;
    }

    public int RouteLength
    {
        get {
            if (Visited.Count == 0) { ApplySearch(); }
            Console.WriteLine(Visited.Count);
            Console.WriteLine(Visited.Count(v => v.Point == DestinationPoint));
            return Distances[DestinationPoint];
        }
    }

    public void OutputPath()
    {
        if (Visited.Count == 0) { ApplySearch(); }

        var destinationNode = Visited.Single(v => v.Point == destinationPoint);
        for (int y = 0; y < Map.Height; y++) {
            for (int x = 0; x < Map.Width; x++) {
                Console.Write(destinationNode.History.Contains(new(x, y)) ? '#' : '.');
            }
            Console.WriteLine();
        }
    }

    protected virtual Node InitialCurrentNode =>
        new(new(InitialPoint.X, InitialPoint.Y));

    private void Reset()
    {
        Distances.Clear();
        Visited.Clear();
        Unvisited.Clear();

        Unvisited.Enqueue(InitialCurrentNode, 0);
        Distances[InitialPoint] = 0;
    }

    protected IEnumerable<Point2D> AdjacentPoints()
    {
        yield return new Point2D(CurrentNode.Point.X - 1, CurrentNode.Point.Y);
        yield return new Point2D(CurrentNode.Point.X + 1, CurrentNode.Point.Y);
        yield return new Point2D(CurrentNode.Point.X, CurrentNode.Point.Y - 1);
        yield return new Point2D(CurrentNode.Point.X, CurrentNode.Point.Y + 1);
    }

    protected virtual IEnumerable<Node> NextNodes()
    {
        foreach (Point2D point in AdjacentPoints()) {
            if (Map.PointOutOfBounds(point)) { continue; }

            Node unvisited = new(point);
            if (Visited.Contains(unvisited)) { continue; }

            unvisited.History.AddRange(CurrentNode.History);
            unvisited.History.Add(point);
            yield return unvisited;
        }
    }

    private void ApplySearch()
    {
        while (Unvisited.Count > 0) {
            CurrentNode = Unvisited.Dequeue();

            foreach (Node node in NextNodes()) {
                var newDistance = Distances[CurrentNode.Point] +
                    int.Parse(Map[node.Point].ToString());
                if (newDistance < Distances.GetValueOrDefault(node.Point, int.MaxValue)) {
                    Distances[node.Point] = newDistance;
                    Unvisited.Enqueue(node, Distances[node.Point]);
                }
            }

            Visited.Add(CurrentNode);
        }
    }
}
