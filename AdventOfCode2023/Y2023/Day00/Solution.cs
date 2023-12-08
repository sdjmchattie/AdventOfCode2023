namespace AdventOfCode.Y2023;

class Day00 {
    private string[]? inputContents;
    private string[] InputContents => inputContents ??=
        File.ReadAllLines($"Y2023/{this.GetType().Name}/input.txt");

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
