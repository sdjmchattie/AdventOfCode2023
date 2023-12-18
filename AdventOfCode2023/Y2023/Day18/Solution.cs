using System.Security.Cryptography;
using AdventOfCode.Utils;
using AdventOfCode.Utils.Y2023.Day18;

namespace AdventOfCode.Y2023;

class Day18 {
    private string[]? inputContents;
    private string[] InputContents =>
        inputContents ??= File.ReadAllLines($"Y2023/{GetType().Name}/input.txt");

    private Terrain Terrain => new(InputContents.Select(line => {
        var parts = line.Split(' ');
        var direction = parts[0] switch {
            "L" => CompassDirection.West,
            "U" => CompassDirection.North,
            "R" => CompassDirection.East,
            "D" => CompassDirection.South,
            _ => default,
        };
        var distance = int.Parse(parts[1]);
        var colourHex = parts[2][1..^1];

        return new DiggerInstruction(direction, distance, colourHex);
    }));

    public object Part1()
    {
        return Terrain.ExcavatedArea;
    }

    public object Part2()
    {
        var input = InputContents;
        return "Part 2 Solution";
    }
}
