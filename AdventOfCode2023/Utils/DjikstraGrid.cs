namespace AdventOfCode.Utils;

class DjikstraGrid : Grid2D
{
    protected class NodeState(Point2D point) : IEquatable<NodeState>
    {
        public readonly Point2D Point = point;

        public bool Equals(NodeState? other)
        {
            if (other == null) { return false; }

            return Point == other.Point;
        }

        public override bool Equals(object? obj) => Equals(obj as NodeState);

        public override int GetHashCode() => Point.GetHashCode();
    }

    protected class Node(NodeState state) : IEquatable<Node>
    {
        public int Distance = int.MaxValue;
        public List<Point2D> History = [];
        public readonly NodeState State = state;

        public bool Equals(Node? other) => State.Equals(other?.State);

        public override bool Equals(object? obj) => Equals(obj as Node);

        public override int GetHashCode() => State.GetHashCode();
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

    protected Node CurrentNode = new(new NodeState(new(0, 0))) { Distance = 0 };
    protected readonly HashSet<Node> Unvisited = [];
    protected readonly HashSet<Node> Visited = [];

    public DjikstraGrid(string[] input) : base(input) {
        DestinationPoint = new(Width - 1, Height - 1);
    }

    public DjikstraGrid(Grid2D other) : base(other) {
        DestinationPoint = new(Width - 1, Height - 1);
    }

    public int ShortestRouteLength
    {
        get {
            if (Visited.Count == 0) { ApplySearch(); }
            return CurrentNode.Distance;
        }
    }

    public void OutputPath()
    {
        if (Visited.Count == 0) { ApplySearch(); }
        for (int y = 0; y < Height; y++) {
            for (int x = 0; x < Width; x++) {
                Console.Write(CurrentNode.History.Contains(new(x, y)) ? '#' : '.');
            }
            Console.WriteLine();
        }
    }

    protected virtual Node InitialCurrentNode =>
        new(new(new(InitialPoint.X, InitialPoint.Y))) { Distance = 0 };

    private void Reset()
    {
        CurrentNode = InitialCurrentNode;
        Visited.Clear();
        Unvisited.Clear();
    }

    protected IEnumerable<Point2D> AdjacentPoints()
    {
        yield return new Point2D(CurrentNode.State.Point.X - 1, CurrentNode.State.Point.Y);
        yield return new Point2D(CurrentNode.State.Point.X + 1, CurrentNode.State.Point.Y);
        yield return new Point2D(CurrentNode.State.Point.X, CurrentNode.State.Point.Y - 1);
        yield return new Point2D(CurrentNode.State.Point.X, CurrentNode.State.Point.Y + 1);
    }

    virtual protected IEnumerable<Node> NextNodes()
    {
        foreach (Point2D point in AdjacentPoints()) {
            if (PointOutOfBounds(point)) { continue; }
            if (!Visited.Any(pp => pp.State.Equals(new(point)))) {
                var unvisited = Unvisited.FirstOrDefault(pp => pp.State.Equals(new(point)));
                if (unvisited == null) {
                    unvisited = new(new(point));
                    unvisited.History.AddRange(CurrentNode.History);
                    Unvisited.Add(unvisited);
                } else {
                    unvisited.History.Remove(unvisited.History.Last());
                }

                unvisited.History.Add(point);

                yield return unvisited;
            }
        }
    }

    private void ApplySearch()
    {
        while (CurrentNode.State.Point != DestinationPoint) {
            foreach (Node node in NextNodes()) {
                var newDistance = CurrentNode.Distance +
                    int.Parse(this[node.State.Point].ToString());
                if (newDistance < node.Distance) {
                    node.Distance = newDistance;
                }
            }

            Visited.Add(CurrentNode);
            Unvisited.Remove(CurrentNode);
            CurrentNode = Unvisited.MinBy(pp => pp.Distance) ?? new(new(default));
        }
    }
}
