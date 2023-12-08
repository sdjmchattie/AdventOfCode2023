using AdventOfCode.Utils.Y2023.Day08;

namespace AdventOfCode.Y2023;

class Day08 {
    private string[]? inputContents;
    private string[] InputContents => inputContents ??=
        File.ReadAllLines($"Y2023/{this.GetType().Name}/input.txt");
    private LoopingDirections Directions => new(InputContents[0]);
    private Dictionary<string, Node> Network
    {
        get {
            return InputContents[2..]
                .Select(line => line
                    .Replace(" ", "")
                    .Replace("(", "")
                    .Replace(")", "")
                    .Split("="))
                .Select(names => new Node(names[0], names[1].Split(",")))
                .ToDictionary(node => node.Name);
        }
    }

    public object Part1()
    {
        var directions = Directions;
        var network = Network;
        var currentNode = network["AAA"];
        var steps = 0;

        while (currentNode.Name != "ZZZ") {
            currentNode = network[currentNode.NodeNameInDirection(directions.Current)];
            directions.MoveNext();
            steps++;
        }

        return steps;
    }

    public object Part2()
    {
        var input = InputContents;
        return "Part 2 Solution";
    }
}
