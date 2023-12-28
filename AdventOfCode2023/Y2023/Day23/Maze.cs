namespace AdventOfCode.Utils.Y2023.Day23;

public class Maze(MazeDataSource dataSource) : GridDjikstra(dataSource)
{
    protected new readonly List<GridDjikstraDataSource.Node> Visited = [];

    public override int OptimalRouteLength
    {
        get {
            if (Visited.Count == 0) { ApplySearch(); }

            var destinationNodes = Visited.Where(v => v.Point == DataSource.DestinationPoint);
            return destinationNodes.Select(n => n.Distance).Max();
        }
    }

    public override void OutputPath()
    {
        if (Visited.Count == 0) { ApplySearch(); }

        var destinationNodes = Visited.Where(v => v.Point == DataSource.DestinationPoint);
        var destinationNode = destinationNodes.MaxBy(n => n.Distance) ?? new(default, default);
        for (int y = 0; y < DataSource.Height(); y++) {
            for (int x = 0; x < DataSource.Width(); x++) {
                Console.Write(destinationNode.History.Contains(new(x, y)) ? '#' : '.');
            }
            Console.WriteLine();
        }
    }

    protected override void ApplySearch()
    {
        while (Unvisited.Count > 0) {
            var currentNode = Unvisited.Dequeue();

            foreach (GridDjikstraDataSource.Node nextNode in DataSource.NextNodes(currentNode)) {
                Unvisited.Enqueue(nextNode, nextNode.Distance);
            }

            Visited.Add(currentNode);
        }
    }
}
