using AdventOfCode.Utils.Y2023.Day07;

namespace AdventOfCode.Y2023;

class Day07 {
    private string[]? inputContents;
    private string[] InputContents => inputContents ??= File.ReadAllLines($"Y2023/{this.GetType().Name}/input.txt");

    private List<Hand> Hands => InputContents
        .Select(line => line.Split(" "))
        .Select(parts => new Hand(parts[0], int.Parse(parts[1])))
        .ToList();

    public object Part1()
    {
        var i = 0;
        return Hands.Order().ToList().Aggregate(0, (acc, e) => acc + e.Winnings(++i));
    }

    public object Part2()
    {
        var input = InputContents;
        return "Part 2 Solution";
    }
}
