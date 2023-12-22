namespace AdventOfCode.Utils.Y2023.Day17;

class City : DjikstraGrid
{
    protected class CityNodeState(
        Point2D point, CompassDirection entryDirection, int straightCount
    ) : NodeState(point)
    {
        public readonly CompassDirection EntryDirection = entryDirection;
        public readonly int StraightCount = straightCount;

        public bool Equals(CityNodeState? other) =>
            base.Equals(other) &&
            EntryDirection == other.EntryDirection &&
            StraightCount == other.StraightCount;

        public override bool Equals(object? obj) => Equals(obj as CityNodeState);

        public override int GetHashCode() =>
            HashCode.Combine(base.GetHashCode(), EntryDirection, StraightCount);
    }

    protected class CityNode(CityNodeState state) : Node(state) {
        public new CityNodeState State => (CityNodeState)base.State;
    }

    public City(string[] input) : base(input) {  }
    public City(Grid2D other) : base(other) {  }

    protected override CityNode InitialCurrentNode =>
        new(new(new(InitialPoint.X, InitialPoint.Y), default, 0)) { Distance = 0 };

    protected override IEnumerable<CityNode> NextNodes()
    {
        var currentPoint = CurrentNode.State.Point;
        foreach (Point2D point in AdjacentPoints()) {
            if (PointOutOfBounds(point)) { continue; }
            var direction = (point.X - currentPoint.X, point.Y - currentPoint.Y) switch {
                (var dx, var _) when dx < 0 => CompassDirection.West,
                (var dx, var _) when dx > 0 => CompassDirection.East,
                (var _, var dy) when dy < 0 => CompassDirection.North,
                (var _, var dy) when dy > 0 => CompassDirection.South,
                (_, _) => default,
            };

            var currentState = ((CityNode)CurrentNode).State;
            var sameDirection = currentState.EntryDirection == direction;
            var reverseDirection = currentState.EntryDirection.Opposite() == direction;
            var straightCount = sameDirection ? currentState.StraightCount + 1 : 1;
            if (reverseDirection || (sameDirection && straightCount > 3)) {
                continue;
            }

            var newState = new CityNodeState(point, direction, straightCount);

            if (!Visited.Any(pp => pp.State.Equals(newState))) {
                var unvisited = Unvisited.Cast<CityNode>().FirstOrDefault(pp => pp.State.Equals(newState));
                if (unvisited == null) {
                    unvisited = new(newState);
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
}
