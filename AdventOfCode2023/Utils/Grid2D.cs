namespace AdventOfCode.Utils;

readonly struct Offset(int x, int y) {
    public readonly int X = x;
    public readonly int Y = y;
}

readonly struct Point(int x, int y) {
    public readonly int X = x;
    public readonly int Y = y;

    public Point OffsetBy(Offset offset) => new(offset.X + X, offset.Y + Y);
}

static class GridCompassExtensions {
    public static Offset GetOffset(this CompassDirection direction)
    {
        var offsets = new Dictionary<CompassDirection, Offset>()
            {
                { CompassDirection.North, new Offset(0, -1) },
                { CompassDirection.NorthEast, new Offset(1, -1) },
                { CompassDirection.East, new Offset(1, 0) },
                { CompassDirection.SouthEast, new Offset(1, 1) },
                { CompassDirection.South, new Offset(0, 1) },
                { CompassDirection.SouthWest, new Offset(-1, 1) },
                { CompassDirection.West, new Offset(-1, 0) },
                { CompassDirection.NorthWest, new Offset(-1, -1) },
            };

        return offsets[direction];
    }
}

class Grid2D(string[] input)
{
    private readonly List<List<char>> grid =
        input.Select(line => line.ToList()).ToList();

    public int Width => grid[0].Count;
    public int Height => grid.Count;

    public char this[int x, int y] => grid[y][x];
    public char this[Point point] => this[point.X, point.Y];
    public IEnumerable<char> this[IEnumerable<Point> points] =>
        points.Select(point => this[point]);

    public IEnumerable<Point> Find(char needle)
    {
        for (int x = 0; x < Width; x++) {
            for (int y = 0; y < Height; y++) {
                if (this[x, y] == needle) { yield return new Point(x, y); }
            }
        }
    }

    public IEnumerable<char> Neighbours(Point point)
    {
        var minX = Math.Max(0, point.X - 1);
        var maxX = Math.Min(Width - 1, point.X + 1);
        var minY = Math.Max(0, point.Y - 1);
        var maxY = Math.Min(Height - 1, point.Y + 1);

        for (int curX = minX; curX <= maxX; curX++) {
            for (int curY = minY; curY <= maxY; curY++) {
                if (curX == point.X && curY == point.Y) { continue; }
                yield return this[curX, curY];
            }
        }
    }

    public char? Neighbour(Point point, CompassDirection direction)
    {
        var outPoint = point.OffsetBy(direction.GetOffset());
        return PointOutOfBounds(outPoint) ? null : this[outPoint];
    }

    public IEnumerable<Point> PointsTowards(Point point, CompassDirection direction)
    {
        var newPoint = point;
        var offset = direction.GetOffset();
        while (true) {
            newPoint = newPoint.OffsetBy(offset);
            if (PointOutOfBounds(newPoint)) {
                yield break;
            }

            yield return newPoint;
        }
    }

    private bool PointOutOfBounds(Point point) =>
        point.X < 0 || point.X >= Width || point.Y < 0 || point.Y >= Height;
}
