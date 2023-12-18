namespace AdventOfCode.Utils.Y2023.Day18;

class FloodFillGrid : Grid2D
{
    public FloodFillGrid(string[] input) : base(input) {  }
    public FloodFillGrid(Grid2D other) : base(other) {  }

    public void Draw(IEnumerable<Point> points)
    {
        foreach (Point point in points) {
            this[point] = '#';
        }
    }

    public void FloodFill()
    {
        var lastRow = string.Concat(this[PointsAlong(Height - 1, CompassDirection.East)]);
        var x = lastRow.IndexOf('#');
        var floodPoints = new HashSet<Point>() { new(x + 1, Height - 2) };
        while (floodPoints.Count != 0) {
            var current = floodPoints.First();
            this[current] = '#';
            floodPoints.Remove(current);

            foreach (Point p in NeighbourPoints(current).Where(n => this[n] == '.')) {
                floodPoints.Add(p);
            }
        }
    }
}
