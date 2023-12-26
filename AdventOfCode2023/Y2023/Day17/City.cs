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

    public City(Grid2D map) : base(map) {  }
    public City(City other) : base(other) {  }

    protected override CityNode InitialCurrentNode =>
        new(0, new(InitialPoint.X, InitialPoint.Y), CompassDirection.SouthEast, 0);

    protected override IEnumerable<CityNode> NextNodes(Node currentNode)
    {
        var cityNode = (CityNode)currentNode;

        foreach (CompassDirection newDirection in Directions) {
            var newPoint = cityNode.Point.OffsetBy(newDirection.GetOffset());

            if (Map.PointOutOfBounds(newPoint)) { continue; }

            var newDistance = cityNode.Distance + int.Parse(Map[newPoint].ToString());

            if (newDirection == cityNode.EntryDirection) {
                if (cityNode.StraightCount < 3) {
                    yield return new CityNode(
                        newDistance, newPoint, newDirection,
                        cityNode.StraightCount + 1
                    );
                }
            } else if (newDirection != cityNode.EntryDirection.Opposite()) {
                yield return new CityNode(newDistance, newPoint, newDirection, 1);
            }
        }
    }
}
