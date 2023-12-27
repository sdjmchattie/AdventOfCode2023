namespace AdventOfCode.Utils.Y2023.Day23;

public class Maze : Djikstra
{
    public Maze(IDjikstraDataSource dataSource) : base(dataSource) {  }
    public Maze(Maze other) : base(other) {  }

    public override int RouteLength
    {
        get {
            if (Visited.Count == 0) { ApplySearch(); }

            var destinationNodes = Visited.Where(v => v.Point == DestinationPoint);
            return int.MaxValue / 2 - destinationNodes.Select(n => n.Distance).Min();
        }
    }

    public override void OutputPath()
    {
        if (Visited.Count == 0) { ApplySearch(); }

        var destinationNodes = Visited.Where(v => v.Point == DestinationPoint);
        var destinationNode = destinationNodes.MinBy(n => n.Distance) ?? new(default, default);
        for (int y = 0; y < DataSource.Height(); y++) {
            for (int x = 0; x < DataSource.Width(); x++) {
                Console.Write(destinationNode.History.Contains(new(x, y)) ? '#' : '.');
            }
            Console.WriteLine();
        }
    }

    protected override Node InitialCurrentNode =>
        new(int.MaxValue / 2, new(InitialPoint.X, InitialPoint.Y));

    protected override IEnumerable<Node> NextNodes(Node currentNode)
    {
        foreach (CompassDirection newDirection in Directions) {
            var newPoint = currentNode.Point.OffsetBy(newDirection.GetOffset());

            if (OutOfBounds(newPoint)) { continue; }
            if (currentNode.History.Contains(newPoint)) { continue; }

            var distance = DataSource.Distance(currentNode.Point, newPoint);
            if (distance < 0) { continue; }

            yield return new Node(currentNode.Distance - distance, newPoint);
        }
    }

    protected override void ApplySearch()
    {
        while (Unvisited.Count > 0) {
            var currentNode = Unvisited.Dequeue();

            foreach (Node nextNode in NextNodes(currentNode)) {
                nextNode.History.AddRange(currentNode.History);
                nextNode.History.Add(nextNode.Point);
                Unvisited.Enqueue(nextNode, nextNode.Distance);
            }

            Visited.Add(currentNode);
        }
    }
}
