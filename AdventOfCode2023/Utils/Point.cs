namespace AdventOfCode.Utils;

public readonly record struct Point2D(int X, int Y) {
    public Point2D OffsetBy(Offset offset) => new(offset.X + X, offset.Y + Y);
}

public readonly record struct Point3D(int X, int Y, int Z);
