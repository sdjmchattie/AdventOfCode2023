namespace AdventOfCode.Utils.Y2023.Day10;

static class PipeExtensions {
    public static IEnumerable<CompassDirection> ConnectedDirections(this char pipeChar) {
        return new List<CompassDirection>();
    }
}

class PipeMap : Grid2D {
    private readonly Point startPoint;
    private readonly char startChar;
    private IList<Point>? pipePoints;

    public PipeMap(string[] input) : base(input)
    {
        startPoint = Find('S').First();
        var startDirs = new List<CompassDirection> {
                CompassDirection.North, CompassDirection.East,
                CompassDirection.South, CompassDirection.West,
            }
            .Select(dir => (Direction: dir, Pipe: Neighbour(startPoint, dir)))
            .Where(dirPipe => dirPipe.Pipe != null)
            .Where(dirPipe => NextDir(dirPipe.Pipe ?? '.', dirPipe.Direction.Opposite()) != null)
            .Select(dirPipe => dirPipe.Direction);
        startChar = ConnectedDirections.Keys
            .Where(key => startDirs.All(d => ConnectedDirections[key].Contains(d)))
            .First();
    }

    private IList<Point> PipePoints
    {
        get {
            if (pipePoints == null) {
                pipePoints = new List<Point> { startPoint };
                CompassDirection? possDirection = ConnectedDirections[startChar].First();
                var nextDirection = possDirection ?? CompassDirection.North;
                var nextPoint = startPoint.OffsetBy(nextDirection.GetOffset());
                var nextPipe = this[nextPoint];
                while (nextPipe != 'S') {
                    pipePoints.Add(nextPoint);
                    possDirection = NextDir(nextPipe, nextDirection.Opposite());
                    nextDirection = possDirection ?? CompassDirection.North;
                    nextPoint = nextPoint.OffsetBy(nextDirection.GetOffset());
                    nextPipe = this[nextPoint];
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

    public int Length => PipePoints.Count;

    public int InnerGroundCount {
        get {
            var groundPoints = new List<Point>();
            for (int x = 0; x < Width; x++) {
                for (int y = 0; y < Height; y++) {
                    var point = new Point(x, y);
                    if (!PipePoints.Contains(point)) {
                        groundPoints.Add(point);
                    }
                }
            }

            return groundPoints
                .Select(ground => PointsTowards(ground, CompassDirection.East))
                .Select(mapPoints => PipePoints.Intersect(mapPoints))
                .Select(pipePoints => pipePoints.OrderBy(point => point.X))
                .Select(pipePoints => this[pipePoints])
                .Count(pipeChars => {
                    var actualChars = pipeChars.Select(c => c == 'S' ? startChar : c);
                    var verticalCount = actualChars.Count(c => c == '|');
                    var angles = actualChars.Where(c => c == 'L' || c == 'F' || c == '7' || c == 'J').ToList();
                    var angleCount = 0;
                    for (int i = 0; i < angles.Count; i += 2) {
                        var angleString = $"{angles[i]}{angles[i + 1]}";
                        if (angleString == "L7" || angleString == "FJ") { angleCount++; }
                    }

                    return (verticalCount + angleCount) % 2 == 1;
                });
        }
    }

    private static CompassDirection? NextDir(char piece, CompassDirection excluding)
    {
        var connectedDirections = ConnectedDirections[piece];
        if (!connectedDirections.Contains(excluding)) {
            return null;
        }

        return ConnectedDirections[piece].First(dir => dir != excluding);
    }
}
