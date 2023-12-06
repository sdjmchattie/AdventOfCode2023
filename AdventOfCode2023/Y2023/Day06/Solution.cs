using System.Collections.Immutable;
using AdventOfCode.Utils.Y2023.Day06;

namespace AdventOfCode.Y2023;

class Day06 {
    private string[]? fileContents;

    private string[] FileContents()
    {
        fileContents ??= File.ReadAllLines("Y2023/Day06/input.txt");

        return fileContents;
    }

    private IEnumerable<Boat> Boats
    {
        get {
            var times = FileContents()[0].Split(" ").Where(e => e != string.Empty).Skip(1).Select(int.Parse);
            var distances = FileContents()[1].Split(" ").Where(e => e != string.Empty).Skip(1).Select(int.Parse);

            return times.Zip(distances).Select(values => new Boat() {Time = values.First, Distance = values.Second});
        }
    }

    public object Part1()
    {
        var boats = Boats;
        return boats.Aggregate(1, (acc, boat) => acc * boat.WinCount());
    }

    public object Part2()
    {
        var boats = Boats;
        return "Part 2 Solution";
    }
}
