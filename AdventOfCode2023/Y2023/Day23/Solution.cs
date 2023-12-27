using AdventOfCode.Utils.Y2023.Day23;

namespace AdventOfCode.Y2023;

class Day23 {
    private string[]? _inputContents;
    private string[] InputContents =>
        _inputContents ??= File.ReadAllLines($"Y2023/{GetType().Name}/input.txt");

    private MazeDataSource? _dataSource;
    private MazeDataSource DataSource => _dataSource ??= new(InputContents)
        {
            InitialPoint = new(1,0),
            DestinationPoint = new(InputContents[0].Length - 2, InputContents.Length - 1)
        };

    private Maze Maze => new(DataSource);

    public object Part1()
    {
        return Maze.RouteLength;
    }

    public object Part2()
    {
        DataSource.CanClimbSlopes = true;
        return Maze.RouteLength;
    }
}
