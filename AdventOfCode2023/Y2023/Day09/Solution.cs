using AdventOfCode.Utils.Y2023.Day09;

namespace AdventOfCode.Y2023;

class Day09 {
    private string[]? inputContents;
    private string[] InputContents =>
        inputContents ??= File.ReadAllLines($"Y2023/{GetType().Name}/input.txt");

    private IEnumerable<Extrapolation> Extrapolations =>
        InputContents.Select(line => new Extrapolation(line.Split(" ").Select(int.Parse)));

    public object Part1()
    {
        return Extrapolations.Select(ext => ext.Extrapolate(left: false)).Sum();
    }

    public object Part2()
    {
        return Extrapolations.Select(ext => ext.Extrapolate(left: true)).Sum();
    }
}
