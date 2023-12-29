using AdventOfCode.Utils.Y2023.Day25;

namespace AdventOfCode.Y2023;

class Day25 {
    private string[]? _inputContents;
    private string[] InputContents =>
        _inputContents ??= File.ReadAllLines($"Y2023/{GetType().Name}/input.txt");

    private SnowMachine? _snowMachine;
    private SnowMachine SnowMachine => _snowMachine ??= new(InputContents);

    public object Part1()
    {
        var (partA, partB) = SnowMachine.PartitionSizes();
        return partA * partB;
    }

    public object Part2()
    {
        var input = InputContents;
        return "Part 2 Solution";
    }
}
