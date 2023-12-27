
namespace AdventOfCode.Utils.Y2023.Day17;

public class CityDataSource(string[] input) : DjikstraGridDataSource(input)
{
    public class CityNode(
        int distance, Point2D point, CompassDirection entryDirection, int straightCount
    ) : Node(distance, point)
    {
        public readonly CompassDirection EntryDirection = entryDirection;
        public readonly int StraightCount = straightCount;

        public bool Equals(CityNode? other)
        {
            return base.Equals(other) &&
                EntryDirection == other.EntryDirection &&
                StraightCount == other.StraightCount;
        }
        public override bool Equals(object? obj) => Equals(obj as CityNode);

        public override int GetHashCode() =>
            HashCode.Combine(base.GetHashCode(), EntryDirection, StraightCount);
    }

    public override CityNode InitialNode() => new(0, InitialPoint, CompassDirection.SouthEast, 0);

    public int MinimumMovement = 0;

    public int MaximumMovement = int.MaxValue;

    public override IEnumerable<CityNode> NextNodes(Node currentNode)
    {
        var cityNode = (CityNode)currentNode;
        foreach (CompassDirection newDirection in Directions) {
            var newPoint = cityNode.Point.OffsetBy(newDirection.GetOffset());

            if (OutOfBounds(newPoint)) { continue; }

            var newDistance = cityNode.Distance + Distance(currentNode.Point, newPoint);
            var newHistory = new List<Point2D>([.. currentNode.History, newPoint]);

            if (newDirection == cityNode.EntryDirection) {
                if (cityNode.StraightCount < MaximumMovement && (
                    newPoint != DestinationPoint ||
                    cityNode.StraightCount >= MinimumMovement
                )) {
                    yield return new CityNode(
                        newDistance, newPoint, newDirection, cityNode.StraightCount + 1
                    ) { History = newHistory };
                }
            } else if (newDirection != cityNode.EntryDirection.Opposite() &&
                (
                    newPoint != DestinationPoint ||
                    MinimumMovement <= 1
                ) && (
                    cityNode.StraightCount >= MinimumMovement ||
                    cityNode.Point == InitialPoint
                )) {
                    yield return new CityNode(
                        newDistance, newPoint, newDirection, 1
                    ) { History = newHistory };
            }
        }
    }
}
