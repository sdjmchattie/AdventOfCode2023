using AdventOfCode.Utils;

namespace AdventOfCode.Y2023;

class Day17 {
    private string[]? inputContents;
    private string[] InputContents =>
        inputContents ??= File.ReadAllLines($"Y2023/{GetType().Name}/input.txt");

    private OwnCity? city;
    private OwnCity City => city ??= new(new Grid2D(InputContents));

    public object Part1()
    {
        City.OutputPath();
        return City.RouteLength;
    }

    public object Part2()
    {
        return "Part 2 Solution";
    }
}
