namespace AdventOfCode.Utils.Y2023.Day21;

public class Garden : Grid2D
{
    private readonly HashSet<Point> ReachablePoints = [];

    public Garden(string[] input) : base(input)
    {
        ReachablePoints.Add(Find('S').First());
    }

    public int ReachablePointCount => ReachablePoints.Count;

    public void TakeSteps(int number = 1)
    {
        for (int i = 0; i < number; i++) {
            var current = ReachablePoints.Select(i => i).ToHashSet();
            ReachablePoints.Clear();
            foreach (Point point in current) {
                foreach (Point adjacent in AdjacentPoints(point)) {
                    ReachablePoints.Add(adjacent);
                }
            }
        }
    }

    private IEnumerable<Point> AdjacentPoints(Point point)
    {
        var adjacent = new List<Point>() {
            point.OffsetBy(CompassDirection.North.GetOffset()),
            point.OffsetBy(CompassDirection.East.GetOffset()),
            point.OffsetBy(CompassDirection.South.GetOffset()),
            point.OffsetBy(CompassDirection.West.GetOffset()),
        };

        return adjacent.Where(point => !PointOutOfBounds(point) && this[point] != '#');
    }
}
