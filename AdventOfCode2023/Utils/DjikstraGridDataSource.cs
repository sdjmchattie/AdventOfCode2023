namespace AdventOfCode.Utils;

public class DjikstraGridDataSource(string[] input) : Grid2D(input), IDjikstraDataSource
{
    int IDjikstraDataSource.Height() => Height;
    int IDjikstraDataSource.Width() => Width;
    public int Distance(Point2D from, Point2D to) => int.Parse(this[to].ToString());
}
