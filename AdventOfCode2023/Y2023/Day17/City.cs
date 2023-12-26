namespace AdventOfCode.Utils.Y2023.Day17;

public class City : Djikstra
{
    protected class CityNode(
        Point2D point, CompassDirection entryDirection, int straightCount
    ) : Node(point)
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
        new(new(InitialPoint.X, InitialPoint.Y), CompassDirection.SouthEast, 0);

    protected override IEnumerable<CityNode> NextNodes()
    {
        var cityNode = (CityNode)CurrentNode;

        foreach (Point2D point in AdjacentPoints()) {
            if (Map.PointOutOfBounds(point)) { continue; }

            var direction = (point.X - cityNode.Point.X, point.Y - cityNode.Point.Y) switch {
                (var dx, var _) when dx < 0 => CompassDirection.West,
                (var dx, var _) when dx > 0 => CompassDirection.East,
                (var _, var dy) when dy < 0 => CompassDirection.North,
                (var _, var dy) when dy > 0 => CompassDirection.South,
                (_, _) => default,
            };

            var sameDirection = cityNode.EntryDirection == direction;
            var reverseDirection = cityNode.EntryDirection.Opposite() == direction;
            var straightCount = sameDirection ? cityNode.StraightCount + 1 : 1;
            if (reverseDirection || straightCount > 3) { continue; }

            CityNode unvisited = new(point, direction, straightCount);
            if (Visited.Contains(unvisited)) { continue; }

            unvisited.History.AddRange(CurrentNode.History);
            unvisited.History.Add(point);
            yield return unvisited;
        }
    }
}
