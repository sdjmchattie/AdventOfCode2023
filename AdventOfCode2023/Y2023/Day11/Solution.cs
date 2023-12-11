using AdventOfCode.Utils.Y2023.Day11;

namespace AdventOfCode.Y2023;

class Day11 {
    private string[]? inputContents;
    private string[] InputContents =>
        inputContents ??= File.ReadAllLines($"Y2023/{GetType().Name}/input.txt");

    private Universe Universe => new(InputContents);

    public object Part1()
    {
        return Universe.DistanceBetweenGalaxies(1).Sum();
    }

    public object Part2()
    {
        return Universe.DistanceBetweenGalaxies(999999).Sum();
    }
}
