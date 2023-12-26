using AdventOfCode.Utils;
using AdventOfCode.Utils.Y2023.Day17;

namespace AdventOfCode.Y2023;

class Day17 {
    private string[]? inputContents;
    private string[] InputContents =>
        inputContents ??= File.ReadAllLines($"Y2023/{GetType().Name}/input.txt");

    private City? city;
    private City City => city ??= new(new Grid2D(InputContents));

    public object Part1()
    {
        City.MaximumMovement = 3;
        return City.RouteLength;
    }

    public object Part2()
    {
        City.MinimumMovement = 4;
        City.MaximumMovement = 10;
        return City.RouteLength;
    }
}
