using AdventOfCode.Utils;
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
        var directions = Directions;
        var network = Network;
        var startingNodes = network
            .Where(entry => entry.Key.EndsWith('A'))
            .Select(entry => entry.Value);

        var loopLengths = startingNodes.Select(node => {
            // Find first instance of end node
            var firstSteps = 0;
            while (!node.Name.EndsWith('Z')) {
                node = network[node.NodeNameInDirection(directions.Current)];
                directions.MoveNext();
                firstSteps++;
            }

            // Count distance back to the same end node
            var secondSteps = 1;
            node = network[node.NodeNameInDirection(directions.Current)];
            directions.MoveNext();

            while (!node.Name.EndsWith('Z')) {
                node = network[node.NodeNameInDirection(directions.Current)];
                directions.MoveNext();
                secondSteps++;
            }

            // Quick assertion that the loop length is the same as the initial xxA to xxZ tail!
            if (firstSteps != secondSteps) {
                throw new InvalidDataException("This implementation assumes the lead in (tail) and the loop length will be identical!");
            }

            return firstSteps;
        }).Select(steps => (long)steps);

        return StolenMathsFunctions.LCM(loopLengths);
    }
}
