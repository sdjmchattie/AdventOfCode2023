using AdventOfCode.Utils;
using AdventOfCode.Utils.Y2023.Day18;

namespace AdventOfCode.Y2023;

class Day18 {
    private string[]? inputContents;
    private string[] InputContents =>
        inputContents ??= File.ReadAllLines($"Y2023/{GetType().Name}/input.txt");

    private TerrainPart1 TerrainPart1 => new(InputContents.Select(line => {
        var parts = line.Split(' ');
        var direction = parts[0] switch {
            "L" => CompassDirection.West,
            "U" => CompassDirection.North,
            "R" => CompassDirection.East,
            "D" => CompassDirection.South,
            _ => default,
        };
        var distance = int.Parse(parts[1]);

        return new Instruction(direction, distance);
    }));

    private TerrainPart2 TerrainPart2 => new(InputContents.Select(line => {
        var parts = line.Split(' ');
        var direction = parts[0] switch {
            "L" => CompassDirection.West,
            "U" => CompassDirection.North,
            "R" => CompassDirection.East,
            "D" => CompassDirection.South,
            _ => default,
        };
        var distance = int.Parse(parts[1]);

        return new Instruction(direction, distance);
    }));

    public object Part1()
    {
        return TerrainPart1.ExcavatedArea;
    }

    public object Part2()
    {
        return TerrainPart2.ExcavatedArea;
    }
}
