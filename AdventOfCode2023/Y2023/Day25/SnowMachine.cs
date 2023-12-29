namespace AdventOfCode.Utils.Y2023.Day25;

public class SnowMachine
{
    private readonly List<string> Components;
    private readonly List<(string Src, string Dest)> Connections;
    private readonly ComponentDataSource DataSource;
    private readonly Djikstra Djikstra;

    public SnowMachine(string[] input)
    {
        Components = input
            .Select(line => line.Split(": "))
            .SelectMany(line =>
                line[1].Split(" ").Concat([line[0]]))
            .ToHashSet()
            .ToList();

        Connections = input
            .Select(line => line.Split(": "))
            .SelectMany(line => {
                return line[1].Split(" ").Select(end => (line[0], end));
            })
            .ToList();

        DataSource = new();
        Djikstra = new(DataSource);
    }

    public (int partitionA, int partitionB) PartitionSizes()
    {
        var reducedConnections = new List<(string Src, string Dest)>(Connections);
        for (int i = 0; i < 3; i++) {
            var (src, dest) = MostUsedEdge(reducedConnections);
            Console.WriteLine($"Removing {src} <-> {dest}");
            reducedConnections = reducedConnections
                .Where(c =>
                    (c.Src != src && c.Src != dest) ||
                    (c.Dest != src && c.Dest != dest))
                .ToList();
        }

        var queue = new Queue<string>([Components.First()]);
        var observed = new HashSet<string>();
        while (queue.Count > 0) {
            var src = queue.Dequeue();
            if (observed.Contains(src)) { continue; }
            observed.Add(src);

            reducedConnections
                .Where(c => c.Src == src || c.Dest == src)
                .ToList()
                .ForEach(c => queue.Enqueue(c.Src == src ? c.Dest : c.Src));
        }

        return (observed.Count, Components.Count - observed.Count);
    }

    private (string src, string dest) MostUsedEdge(List<(string Src, string Dest)> connections)
    {
        var edgeCounts = new Dictionary<(string, string), int>();

        for (int i = 0; i < Components.Count - 1; i++) {
            if ((i + 1) % 100 == 0) {
                Console.WriteLine($"{i + 1}/{Components.Count - 1}");
            }

            for (int j = i + 1; j < Components.Count; j++) {
                DataSource.InitialIdentifier = Components[i];
                DataSource.DestinationIdentifier = Components[j];
                DataSource.SetConnections(connections);
                Djikstra.Reset();
                var nodeHistory = Djikstra.OptimalRouteHistory.ToList();
                for (int k = 0; k < nodeHistory.Count - 1; k++) {
                    var connection = connections.First(c =>
                        (c.Src == nodeHistory[k] && c.Dest == nodeHistory[k + 1]) ||
                        (c.Src == nodeHistory[k + 1] && c.Dest == nodeHistory[k]));
                    var count = edgeCounts.GetValueOrDefault(connection, 0);
                    edgeCounts[connection] = ++count;
                }
            }
        }

        return edgeCounts.MaxBy(ec => ec.Value).Key;
    }
}
