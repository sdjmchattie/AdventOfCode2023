using AdventOfCode.Utils.Y2023.Day17;

namespace AdventOfCode.Y2023;

class Day17 {
    private string[]? inputContents;
    private string[] InputContents =>
        inputContents ??= File.ReadAllLines($"Y2023/{GetType().Name}/input.txt");

    private City? city;
    private City City => city ??= new(InputContents);

    public object Part1()
    {
        City.OutputPath();
        return City.ShortestRouteLength;
    }

    public object Part2()
    {
        return "Part 2 Solution";
    }
}
