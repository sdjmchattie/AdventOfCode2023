
namespace AdventOfCode.Utils;

public class DjikstraGridDataSource(string[] input) : DjikstraDataSource
{
    protected readonly Grid2D Map = new(input);

    public override int Width() => Map.Width;

    public override int Height() => Map.Height;

    public override int Distance(Point2D from, Point2D to) => int.Parse(Map[to].ToString());

    protected bool OutOfBounds(Point2D point)
    {
        return point.X < 0 || point.X >= Width() ||
            point.Y < 0 || point.Y >= Height();
    }

    public override IEnumerable<Node> NextNodes(Node currentNode)
    {
        foreach (CompassDirection newDirection in Directions) {
            var newPoint = currentNode.Point.OffsetBy(newDirection.GetOffset());

            if (OutOfBounds(newPoint)) { continue; }

            var newDistance = currentNode.Distance + Distance(currentNode.Point, newPoint);

            yield return new Node(newDistance, newPoint)
                { History = [.. currentNode.History, newPoint] };
        }
    }
}
