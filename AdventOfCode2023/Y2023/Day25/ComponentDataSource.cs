namespace AdventOfCode.Utils.Y2023.Day25;

public class ComponentDataSource : DjikstraDataSource
{
    private readonly Dictionary<string, HashSet<string>> Edges = [];

    public void SetConnections(IEnumerable<(string Src, string Dest)> connections)
    {
        Edges.Clear();

        foreach ((var Src, var Dest) in connections) {
            var srcEdges = Edges.GetValueOrDefault(Src, []);
            srcEdges.Add(Dest);
            Edges[Src] = srcEdges;

            var destEdges = Edges.GetValueOrDefault(Dest, []);
            destEdges.Add(Src);
            Edges[Dest] = destEdges;
        }
    }

    public override IEnumerable<Node> NextNodes(Node currentNode)
    {
        foreach (string dest in Edges[currentNode.Identifier]) {
            yield return new Node(currentNode.Distance + 1, dest)
                { History = [.. currentNode.History, dest] };
        }
    }
}
