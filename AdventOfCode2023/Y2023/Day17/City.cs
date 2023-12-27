namespace AdventOfCode.Utils.Y2023.Day17;

public class City : Djikstra
{
    protected class CityNode(
        int distance, Point2D point, CompassDirection entryDirection, int straightCount
    ) : Node(distance, point)
    {
        public readonly CompassDirection EntryDirection = entryDirection;
        public readonly int StraightCount = straightCount;

        public bool Equals(CityNode? other) =>
            base.Equals(other) &&
            EntryDirection == other.EntryDirection &&
            StraightCount == other.StraightCount;

        public override bool Equals(object? obj) => Equals(obj as CityNode);

        public override int GetHashCode() =>
            HashCode.Combine(base.GetHashCode(), EntryDirection, StraightCount);
    }

    public City(IDjikstraDataSource dataSource) : base(dataSource) {  }
    public City(City other) : base(other) {  }

    private int minimumMovement = 0;
    public int MinimumMovement
    {
        get { return minimumMovement; }
        set {
            if (minimumMovement == value) { return; }
            minimumMovement = value;
            Reset();
        }
    }

    private int maximumMovement = int.MaxValue;
    public int MaximumMovement
    {
        get { return maximumMovement; }
        set {
            if (maximumMovement == value) { return; }
            maximumMovement = value;
            Reset();
        }
    }

    protected override CityNode InitialCurrentNode =>
        new(0, new(InitialPoint.X, InitialPoint.Y), CompassDirection.SouthEast, 0);

    protected override IEnumerable<CityNode> NextNodes(Node currentNode)
    {
        var cityNode = (CityNode)currentNode;

        foreach (CompassDirection newDirection in Directions) {
            var newPoint = cityNode.Point.OffsetBy(newDirection.GetOffset());

            if (OutOfBounds(newPoint)) { continue; }

            var newDistance = cityNode.Distance + DataSource.Distance(currentNode.Point, newPoint);

            if (newDirection == cityNode.EntryDirection) {
                if (cityNode.StraightCount < MaximumMovement && (
                    newPoint != DestinationPoint ||
                    cityNode.StraightCount >= MinimumMovement
                )) {
                    yield return new CityNode(
                        newDistance, newPoint, newDirection,
                        cityNode.StraightCount + 1
                    );
                }
            } else if (newDirection != cityNode.EntryDirection.Opposite() &&
                newPoint != DestinationPoint &&
                (
                    cityNode.StraightCount >= MinimumMovement ||
                    cityNode.Equals(InitialCurrentNode)
                )) {
                    yield return new CityNode(newDistance, newPoint, newDirection, 1);
            }
        }
    }
}
