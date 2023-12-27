using AdventOfCode.Utils.Y2023.Day24;

namespace AdventOfCode.Y2023;

class Day24 {
    private string[]? _inputContents;
    private string[] InputContents =>
        _inputContents ??= File.ReadAllLines($"Y2023/{GetType().Name}/input.txt");

    private HailstoneSimulator? _simulator;
    private HailstoneSimulator Simulator => _simulator ??= new(InputContents);

    public object Part1()
    {
        return Simulator.CollidingStonesCount;
    }

    public object Part2()
    {
        var input = InputContents;
        return "Part 2 Solution";
    }
}
