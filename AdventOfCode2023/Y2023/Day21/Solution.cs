using AdventOfCode.Utils.Y2023.Day21;

namespace AdventOfCode.Y2023;

class Day21 {
    private string[]? inputContents;
    private string[] InputContents =>
        inputContents ??= File.ReadAllLines($"Y2023/{GetType().Name}/example.txt");

    public object Part1()
    {
        var garden = new Garden(InputContents);
        garden.TakeSteps(64);
        return garden.ReachablePointCount;
    }

    public object Part2()
    {
        var garden = new Garden(InputContents);
        garden.TakeSteps(1000);
        return garden.ReachablePointCount;
    }
}
