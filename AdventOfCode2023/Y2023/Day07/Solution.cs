using AdventOfCode.Utils.Y2023.Day07;

namespace AdventOfCode.Y2023;

class Day07 {
    private string[]? inputContents;
    private string[] InputContents => inputContents ??= File.ReadAllLines($"Y2023/{this.GetType().Name}/input.txt");

    private List<Hand> Hands(bool jacksAreWild) {
        return InputContents
            .Select(line => line.Split(" "))
            .Select(parts => new Hand(parts[0], int.Parse(parts[1]), jacksAreWild))
            .ToList();
    }

    public object Part1()
    {
        var i = 0;
        return Hands(jacksAreWild: false)
            .Order()
            .ToList()
            .Aggregate(0, (acc, e) => acc + e.Winnings(++i));
    }

    public object Part2()
    {
        var i = 0;
        return Hands(jacksAreWild: true)
            .Order()
            .ToList()
            .Aggregate(0, (acc, e) => acc + e.Winnings(++i));
    }
}
