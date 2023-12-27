
namespace AdventOfCode.Utils.Y2023.Day23;

public class MazeDataSource(string[] input) : DjikstraGridDataSource(input)
{
    public bool CanClimbSlopes = false;

    public override int Width() => Map.Width;

    public override int Height() => Map.Height;

    public override int Distance(Point2D from, Point2D to) =>
        (Map[from], Map[to]) switch {
            (_, var tc) when CanClimbSlopes && tc != '#' => 1,
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

    public override IEnumerable<Node> NextNodes(Node currentNode)
    {
        foreach (CompassDirection newDirection in Directions) {
            var newPoint = currentNode.Point.OffsetBy(newDirection.GetOffset());

            if (OutOfBounds(newPoint)) { continue; }
            if (currentNode.History.Contains(newPoint)) { continue; }

            var distance = Distance(currentNode.Point, newPoint);
            if (distance < 0) { continue; }

            yield return new Node(currentNode.Distance + distance, newPoint)
                { History = [.. currentNode.History, newPoint] };
        }
    }
}
