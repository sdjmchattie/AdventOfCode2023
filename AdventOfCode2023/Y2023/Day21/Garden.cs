namespace AdventOfCode.Utils.Y2023.Day21;

public class Garden : Grid2D
{
    private readonly HashSet<Point2D> ReachablePoints = [];

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
            foreach (Point2D point in current) {
                foreach (Point2D adjacent in AdjacentPoints(point)) {
                    ReachablePoints.Add(adjacent);
                }
            }
        }
    }

    private bool IsValid(Point2D point)
    {
        var normalisedX = point.X % Width;
        var normalisedY = point.Y % Height;

        if (normalisedX < 0) { normalisedX += Width; }
        if (normalisedY < 0) { normalisedY += Height; }

        return this[normalisedX, normalisedY] != '#';
    }

    private IEnumerable<Point2D> AdjacentPoints(Point2D point)
    {
        var adjacent = new List<Point2D>() {
            point.OffsetBy(CompassDirection.North.GetOffset()),
            point.OffsetBy(CompassDirection.East.GetOffset()),
            point.OffsetBy(CompassDirection.South.GetOffset()),
            point.OffsetBy(CompassDirection.West.GetOffset()),
        };

        return adjacent.Where(IsValid);
    }
}
