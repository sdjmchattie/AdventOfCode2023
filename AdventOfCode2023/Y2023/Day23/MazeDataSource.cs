namespace AdventOfCode.Utils.Y2023.Day23;

public class MazeDataSource(string[] input) : Grid2D(input), IDjikstraDataSource
{
    int IDjikstraDataSource.Height() => Height;
    int IDjikstraDataSource.Width() => Width;
    public int Distance(Point2D from, Point2D to) =>
        (this[from], this[to]) switch {
            (var fc, _) when fc == '<' && to.X < from.X => 1,
            (var fc, _) when fc == '^' && to.Y < from.Y => 1,
            (var fc, _) when fc == '>' && to.X > from.X => 1,
            (var fc, _) when fc == 'v' && to.Y > from.Y => 1,
            (_, var tc) when tc == '<' && to.X < from.X => 1,
            (_, var tc) when tc == '^' && to.Y < from.Y => 1,
            (_, var tc) when tc == '>' && to.X > from.X => 1,
            (_, var tc) when tc == 'v' && to.Y > from.Y => 1,
            (_, var tc) when tc == '.' => 1,
            _ => -1
        };
}
