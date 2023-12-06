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

    private IEnumerable<Boat> MultipleBoats
    {
        get {
            var times = FileContents()[0].Split(" ").Where(e => e != string.Empty).Skip(1).Select(int.Parse);
            var distances = FileContents()[1].Split(" ").Where(e => e != string.Empty).Skip(1).Select(int.Parse);

            return times.Zip(distances).Select(values => new Boat() {Time = values.First, Distance = values.Second});
        }
    }

    private Boat OneBoat
    {
        get {
            var time = string.Join("", FileContents()[0].Split(" ").Where(e => e != string.Empty).Skip(1));
            var distance = string.Join("", FileContents()[1].Split(" ").Where(e => e != string.Empty).Skip(1));

            return new Boat() {Time = long.Parse(time), Distance = long.Parse(distance)};
        }
    }

    public object Part1()
    {
        var boats = MultipleBoats;
        return boats.Aggregate(1L, (acc, boat) => acc * boat.WinCount());
    }

    public object Part2()
    {
        var boat = OneBoat;
        return boat.WinCount();
    }
}
