using AdventOfCode.Utils;
using AdventOfCode.Utils.Y2023.Day18;

namespace AdventOfCode.Y2023;

class Day18 {
    private string[]? inputContents;
    private string[] InputContents =>
        inputContents ??= File.ReadAllLines($"Y2023/{GetType().Name}/input.txt");

    private IEnumerable<Instruction> Part1Instructions => InputContents.Select(line => {
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
    });

    private IEnumerable<Instruction> Part2Instructions => InputContents.Select(line => {
        var parts = line.Split(' ');
        var direction = parts[2][^2] switch {
            '0' => CompassDirection.East,
            '1' => CompassDirection.South,
            '2' => CompassDirection.West,
            '3' => CompassDirection.North,
            _ => default,
        };
        var distance = Convert.ToInt32(parts[2][2..^2], 16);

        return new Instruction(direction, distance);
    });

    public object Part1()
    {
        var terrain = new Terrain(Part1Instructions);
        return terrain.ExcavatedArea;
    }

    public object Part2()
    {
        var terrain = new Terrain(Part2Instructions);
        return terrain.ExcavatedArea;
    }
}
