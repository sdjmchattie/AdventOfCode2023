namespace AdventOfCode.Y2023;

class Day23 {
    private string[]? _inputContents;
    private string[] InputContents =>
        _inputContents ??= File.ReadAllLines($"Y2023/{GetType().Name}/example.txt");

    public object Part1()
    {
        var input = InputContents;
        return "Part 1 Solution";
    }

    public object Part2()
    {
        var input = InputContents;
        return "Part 2 Solution";
    }
}
