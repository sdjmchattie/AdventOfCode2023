namespace AdventOfCode.Y2023;

class Day00 {
    private string[]? fileContents;

    private string[] FileContents()
    {
        fileContents ??= File.ReadAllLines("Y2023/Day00/input.txt");

        return fileContents;
    }

    public object Part1()
    {
        var input = FileContents();
        return "Part 1 Solution";
    }

    public object Part2()
    {
        var input = FileContents();
        return "Part 2 Solution";
    }
}
