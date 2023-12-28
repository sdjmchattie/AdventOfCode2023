namespace AdventOfCode.Utils.Y2023.Day23;

public class MazeDataSource(string[] input) : DjikstraGridDataSource(input)
{
    public bool CanClimbSlopes = false;
    private readonly List<Intersection> Intersections = [];

    public override int Width() => Map.Width;

    public override int Height() => Map.Height;

    public override IEnumerable<Node> NextNodes(Node currentNode)
    {
        var intersection = Intersections.First(i => i.Point == currentNode.Point);
        foreach (Edge edge in intersection.Edges) {
            var newPoint = edge.End;

            if (!CanClimbSlopes && edge.UphillSlopes) { continue; }
            if (currentNode.History.Contains(newPoint)) { continue; }

            yield return new Node(currentNode.Distance + edge.Length, newPoint)
                { History = [.. currentNode.History, newPoint] };
        }
    }

    private readonly record struct Intersection(Point2D Point)
    {
        public readonly Dictionary<CompassDirection, bool> Directions = [];
        public readonly List<Edge> Edges = [];
    }

    private readonly record struct Edge(
        Point2D Start,
        Point2D End,
        int Length,
        bool UphillSlopes
    );

    public void ParseMaze()
    {
        var start = new Intersection(InitialPoint);
        start.Directions.Add(CompassDirection.South, false);
        Intersections.Add(start);

        var intersection = start;
        while (intersection != default) {
            var undiscoveredDirections = intersection.Directions.Keys
                .Where(direction => !intersection.Directions[direction]);
            foreach (CompassDirection direction in undiscoveredDirections) {
                var found = FindEdge(intersection, direction);
                intersection.Edges.Add(found.Edge);
                intersection.Directions[direction] = true;

                if (!Intersections.Any(i => i.Point == found.Intersection.Point)) {
                    Intersections.Add(found.Intersection);
                }
            }

            intersection = Intersections.FirstOrDefault(
                i => i.Directions.Any(kv => !kv.Value));
        }
    }

    private List<CompassDirection> AvailableDirections(Point2D point) {
        return Directions
            .Select(dir => (Direction: dir, Point: point.OffsetBy(dir.GetOffset())))
            .Where(dp => !OutOfBounds(dp.Point))
            .Where(dp => Map[dp.Point] != '#')
            .Select(dp => dp.Direction)
            .ToList();
    }

    private (Edge Edge, Intersection Intersection) FindEdge(Intersection intersection, CompassDirection direction)
    {
        var length = 0;
        var uphillSlopes = false;
        var curPoint = intersection.Point;
        var curDirections = new List<CompassDirection>() { direction };
        var curDirection = CompassDirection.SouthEast;

        while (curDirections.Count <= 2) {
            curDirection = curDirections.First(d => d != curDirection.Opposite());
            curPoint = curPoint.OffsetBy(curDirection.GetOffset());
            length++;
            uphillSlopes = uphillSlopes || Map[curPoint] switch {
                '<' when curDirection == CompassDirection.East => true,
                '^' when curDirection == CompassDirection.South => true,
                '>' when curDirection == CompassDirection.West => true,
                'v' when curDirection == CompassDirection.North => true,
                _ => false
            };
            curDirections = AvailableDirections(curPoint);

            if (curPoint == DestinationPoint || curPoint == InitialPoint) {
                break;
            }
        }

        var newEdge = new Edge(intersection.Point, curPoint, length, uphillSlopes);
        var newIntersection = new Intersection(curPoint);
        foreach (CompassDirection dir in curDirections) {
            newIntersection.Directions[dir] = false;
        }

        return (newEdge, newIntersection);
    }
}
