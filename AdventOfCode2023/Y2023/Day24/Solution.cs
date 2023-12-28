using AdventOfCode.Utils.Y2023.Day24;

namespace AdventOfCode.Y2023;

class Day24 {
    private string[]? _inputContents;
    private string[] InputContents =>
        _inputContents ??= File.ReadAllLines($"Y2023/{GetType().Name}/input.txt");

    private HailStoneResolver? _resolver;
    private HailStoneResolver Resolver => _resolver ??= new(InputContents);

    public object Part1()
    {
        return Resolver.CollidingStonesCount;
    }

    public object Part2()
    {
        var input = InputContents;
        return "Part 2 Solution";
    }
}
