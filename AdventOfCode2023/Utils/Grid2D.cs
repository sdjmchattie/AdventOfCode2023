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

class Grid2D : IEquatable<Grid2D>
{
    private readonly List<List<char>> grid;

    public Grid2D(string[] input)
    {
        grid =
        input.Select(line => line.ToList()).ToList();
    }

    public Grid2D(Grid2D other) {
        grid = other.grid.Select(row => row.Select(c => c).ToList()).ToList();
    }

    public int Width => grid[0].Count;
    public int Height => grid.Count;

    public char this[int x, int y]
    {
        get { return grid[y][x]; }
        protected set { grid[y][x] = value; }
    }

    public char this[Point point]
    {
        get { return this[point.X, point.Y]; }
        protected set { this[point.X, point.Y] = value; }
    }

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

    public IEnumerable<Point> PointsAlong(int index, CompassDirection direction)
    {
        var maxIndex = direction == CompassDirection.North ||
            direction == CompassDirection.South ? Height : Width;
        if (index < 0 || index >= maxIndex) {
            throw new ArgumentOutOfRangeException(nameof(index), "Index must be within the bounds of the grid.");
        }

        return PointsTowards(PointOutsideGrid(index, direction), direction);
    }

    protected Point PointOutsideGrid(int index, CompassDirection direction)
    {
        return direction switch
        {
            CompassDirection.North => new Point(index, Height),
            CompassDirection.South => new Point(index, -1),
            CompassDirection.West => new Point(Width, index),
            CompassDirection.East => new Point(-1, index),
            _ => default,
        };
    }

    private bool PointOutOfBounds(Point point) =>
        point.X < 0 || point.X >= Width || point.Y < 0 || point.Y >= Height;

    public bool Equals(Grid2D? other)
    {
        if (other == null || grid.Count != other.grid.Count) {
            return false;
        }

        for (int i = 0; i < grid.Count; i++) {
            if (!grid[i].SequenceEqual(other.grid[i])) {
                return false;
            }
        }

        return true;
    }
}
