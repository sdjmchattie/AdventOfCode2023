using AdventOfCode.Utils.Y2023.Day14;

namespace AdventOfCode.Y2023;

class Day14 {
    private string[]? inputContents;
    private string[] InputContents =>
        inputContents ??= File.ReadAllLines($"Y2023/{GetType().Name}/input.txt");

    public object Part1()
    {
        var platform = new Platform(InputContents);
        return platform.LoadWhenTippedNorth();
    }

    public object Part2()
    {
        var input = InputContents;
        return "Part 2 Solution";
    }
}
