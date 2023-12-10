namespace AdventOfCode.Utils.Y2023.Day10;

static class PipeExtensions {
    public static IEnumerable<CompassDirection> ConnectedDirections(this char pipeChar) {
        return new List<CompassDirection>();
    }
}

class PipeMap(string[] input) : Grid2D(input) {
    private IList<Point>? pipePoints;

    private IList<Point> PipePoints
    {
        get {
            if (pipePoints == null) {
                pipePoints = new List<Point>();
                var startPoint = Find('S');
                if (startPoint != null) {
                    pipePoints.Add(startPoint.Value);
                    CompassDirection? nextDirection = new List<CompassDirection> {
                            CompassDirection.North, CompassDirection.East,
                            CompassDirection.South, CompassDirection.West,
                        }
                        .Select(dir => (Direction: dir, Pipe: Neighbour(startPoint.Value, dir)))
                        .Where(dirPipe => dirPipe.Pipe != null)
                        .Where(dirPipe => NextDir(dirPipe.Pipe.Value, dirPipe.Direction.Opposite()) != null)
                        .Select(dirPipe => dirPipe.Direction)
                        .First();

                    var nextPoint = startPoint.Value.Offset(nextDirection.Value.GetOffset());
                    var nextPipe = this[nextPoint];
                    while (nextPipe != 'S') {
                        pipePoints.Add(nextPoint);
                        nextDirection = NextDir(nextPipe, nextDirection.Value.Opposite());
                        nextPoint = nextPoint.Offset(nextDirection.Value.GetOffset());
                        nextPipe = this[nextPoint];
                    }
                }
            }

            return pipePoints;
        }
    }

    private static Dictionary<char, CompassDirection[]> ConnectedDirections =>
        new() {
            {'|', [CompassDirection.North, CompassDirection.South]},
            {'-', [CompassDirection.West, CompassDirection.East]},
            {'L', [CompassDirection.North, CompassDirection.East]},
            {'J', [CompassDirection.North, CompassDirection.West]},
            {'7', [CompassDirection.West, CompassDirection.South]},
            {'F', [CompassDirection.East, CompassDirection.South]},
        };

    private static CompassDirection? NextDir(char piece, CompassDirection excluding)
    {
        var connectedDirections = ConnectedDirections[piece];
        if (!connectedDirections.Contains(excluding)) {
            return null;
        }

        Console.WriteLine($"Excluding: {excluding}");
        return ConnectedDirections[piece].First(dir => dir != excluding);
    }

    public int Length => PipePoints.Count;
}
