using AdventOfCode.Utils.Y2023.Day23;

namespace AdventOfCode.Y2023;

class Day23 {
    private string[]? _inputContents;
    private string[] InputContents =>
        _inputContents ??= File.ReadAllLines($"Y2023/{GetType().Name}/input.txt");

    private Maze? maze;
    private Maze Maze
    {
        get {
            if (maze == null) {
                var dataSource = new MazeDataSource(InputContents);
                maze = new(dataSource)
                {
                    InitialPoint = new(1, 0),
                    DestinationPoint = new(dataSource.Width - 2, dataSource.Height - 1)
                };
            }

            return maze;
        }
    }

    public object Part1()
    {
        Maze.OutputPath();
        return Maze.RouteLength;
    }

    public object Part2()
    {
        var input = InputContents;
        return "Part 2 Solution";
    }
}
