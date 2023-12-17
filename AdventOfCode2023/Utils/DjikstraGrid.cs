namespace AdventOfCode.Utils;

class DjikstraGrid : Grid2D
{
    protected class PathPoint(Point point)
    {
        public readonly Point Point = point;
        public PathPoint? previousPathPoint;
        public int Distance = int.MaxValue;
    }

    private Point initialPoint = new(0, 0);
    public Point InitialPoint
    {
        get { return initialPoint; }
        set {
            if (initialPoint == value) { return; }
            initialPoint = value;
            Reset();
        }
    }

    private Point destinationPoint = new(0, 0);
    public Point DestinationPoint
    {
        get { return destinationPoint; }
        set {
            if (destinationPoint == value) { return; }
            destinationPoint = value;
            Reset();
        }
    }

    protected PathPoint CurrentPoint = new(new(0, 0)) { Distance = 0 };
    private readonly HashSet<PathPoint> Unvisited = [];
    private readonly HashSet<PathPoint> Visited = [];

    public DjikstraGrid(string[] input) : base(input) {
        DestinationPoint = new(Width - 1, Height - 1);
    }

    public DjikstraGrid(Grid2D other) : base(other) {
        DestinationPoint = new(Width - 1, Height - 1);
    }

    public int ShortestRouteLength
    {
        get {
            if (Visited.Count < 2) { ApplySearch(); }
            return CurrentPoint.Distance;
        }
    }

    private void Reset()
    {
        CurrentPoint = new(new(InitialPoint.X, InitialPoint.Y)) { Distance = 0 };
        Visited.Clear();
        Unvisited.Clear();

        for (int x = 0; x < Width; x++) {
            for (int y = 0; y < Height; y++) {
                Unvisited.Add(new(new(x, y)));
            }
        }
    }

    private IEnumerable<Point> AdjacentPoints()
    {
        yield return new Point(CurrentPoint.Point.X - 1, CurrentPoint.Point.Y);
        yield return new Point(CurrentPoint.Point.X + 1, CurrentPoint.Point.Y);
        yield return new Point(CurrentPoint.Point.X, CurrentPoint.Point.Y - 1);
        yield return new Point(CurrentPoint.Point.X, CurrentPoint.Point.Y + 1);
    }

    virtual protected IEnumerable<PathPoint> NextPathPoints()
    {
        foreach (Point adjacentPoint in AdjacentPoints()) {
            if (Unvisited.Any(pp => pp.Point == adjacentPoint)) {
                yield return Unvisited.First(pp => pp.Point == adjacentPoint);
            }
        }
    }

    private void ApplySearch()
    {
        while (CurrentPoint.Point != DestinationPoint) {
            foreach (PathPoint pathPoint in NextPathPoints()) {
                var newDistance = CurrentPoint.Distance + this[pathPoint.Point];
                if (newDistance < pathPoint.Distance) {
                    pathPoint.Distance = newDistance;
                    pathPoint.previousPathPoint = CurrentPoint;
                }
            }

            Visited.Add(CurrentPoint);
            Unvisited.Remove(CurrentPoint);
            CurrentPoint = Unvisited.MinBy(pp => pp.Distance) ?? new(new(0, 0));
        }
    }
}
