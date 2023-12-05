namespace AdventOfCode.Y2023;

class Day00 {
    private string[]? parsedInput;

    private string[] ParsedInput()
    {
        parsedInput ??= File.ReadAllLines("Y2023/Day00/input.txt");

        return parsedInput;
    }

    public object Part1()
    {
        var input = ParsedInput();
        return "Part 1 Solution";
    }

    public object Part2()
    {
        var input = ParsedInput();
        return "Part 2 Solution";
    }
}
