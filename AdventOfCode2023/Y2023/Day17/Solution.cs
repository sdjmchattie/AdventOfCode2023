using AdventOfCode.Utils;
using AdventOfCode.Utils.Y2023.Day17;

namespace AdventOfCode.Y2023;

class Day17 {
    private string[]? inputContents;
    private string[] InputContents =>
        inputContents ??= File.ReadAllLines($"Y2023/{GetType().Name}/input.txt");

    private CityDataSource? _dataSource;
    private CityDataSource DataSource => _dataSource ??= new(InputContents)
    {
        InitialPoint = new(0,0),
        DestinationPoint = new(InputContents[0].Length - 1, InputContents.Length - 1)
    };

    private GridDjikstra? city;
    private GridDjikstra City => city ??= new(DataSource);

    public object Part1()
    {
        DataSource.MaximumMovement = 3;
        City.Reset();
        return City.OptimalRouteLength;
    }

    public object Part2()
    {
        DataSource.MinimumMovement = 4;
        DataSource.MaximumMovement = 10;
        City.Reset();
        return City.OptimalRouteLength;
    }
}
