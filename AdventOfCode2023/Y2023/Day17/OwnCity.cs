namespace AdventOfCode.Utils;

public class OwnCity
{
    protected class Node(
        int distance, Point2D point, CompassDirection entryDirection, int straightCount
    ) : IEquatable<Node>
    {
        public int Distance = distance;
        public readonly Point2D Point = point;
        public readonly CompassDirection EntryDirection = entryDirection;
        public readonly int StraightCount = straightCount;
        public List<Point2D> History = [];

        public bool Equals(Node? other)
        {
            if (other == null) { return false; }

            return Point == other.Point &&
            EntryDirection == other.EntryDirection &&
            StraightCount == other.StraightCount;
        }

        public override bool Equals(object? obj) => Equals(obj as Node);

        public override int GetHashCode() =>
            HashCode.Combine(Point, EntryDirection, StraightCount);
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
    protected readonly PriorityQueue<Node, int> Unvisited = new();
    protected readonly HashSet<Node> Visited = [];

    public OwnCity(Grid2D map) {
        Map = map;
        DestinationPoint = new(Map.Width - 1, Map.Height - 1);
    }

    public OwnCity(OwnCity other) {
        Map = other.Map;
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
        for (int y = 0; y < Map.Height; y++) {
            for (int x = 0; x < Map.Width; x++) {
                Console.Write(destinationNode.History.Contains(new(x, y)) ? '#' : '.');
            }
            Console.WriteLine();
        }
    }

    protected Node InitialCurrentNode =>
        new(
            0,
            new(InitialPoint.X, InitialPoint.Y),
            CompassDirection.SouthEast,
            0
        ) { Distance = 0 };

    private void Reset()
    {
        Visited.Clear();
        Unvisited.Clear();
    }

    protected List<CompassDirection> Directions = [
        CompassDirection.North, CompassDirection.East,
        CompassDirection.South, CompassDirection.West,
    ];

    // protected IEnumerable<Node> NextNodes(Node source)
    // {
    //     foreach (CompassDirection direction in Directions) {
    //         if (Map.PointOutOfBounds(point)) { continue; }

    //         var sameDirection = source.EntryDirection == direction;
    //         var reverseDirection = source.EntryDirection.Opposite() == direction;
    //         var straightCount = sameDirection ? source.StraightCount + 1 : 1;
    //         if (reverseDirection || straightCount > 3) { continue; }

    //         var unvisited = new Node(point, direction, straightCount);
    //         if (Visited.Contains(unvisited)) { continue; }

    //         unvisited.History.AddRange(source.History);
    //         unvisited.History.Add(point);
    //         yield return unvisited;
    //     }
    // }

    private void ApplySearch()
    {
        Reset();
        Unvisited.Enqueue(InitialCurrentNode, 0);

        var currentNode = InitialCurrentNode;
        while (currentNode.Point != DestinationPoint) {
            currentNode = Unvisited.Dequeue();
            if (Visited.Contains(currentNode)) { continue; }
            Visited.Add(currentNode);

            foreach (CompassDirection newDirection in Directions) {
                var newPoint = currentNode.Point.OffsetBy(newDirection.GetOffset());

                if (Map.PointOutOfBounds(newPoint)) { continue; }

                var newDistance = currentNode.Distance + int.Parse(Map[newPoint].ToString());

                Node? newNode = null;
                if (newDirection == currentNode.EntryDirection) {
                    if (currentNode.StraightCount < 3) {
                        newNode = new Node(
                            newDistance, newPoint, newDirection,
                            currentNode.StraightCount + 1
                        );
                    }
                } else if (newDirection != currentNode.EntryDirection.Opposite()) {
                    newNode = new Node(newDistance, newPoint, newDirection, 1);
                }

                if (newNode != null) {
                    newNode.History.AddRange(currentNode.History);
                    newNode.History.Add(newNode.Point);
                    Unvisited.Enqueue(newNode, newNode.Distance);
                }
            }
        }
    }
}
