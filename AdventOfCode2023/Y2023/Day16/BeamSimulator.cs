namespace AdventOfCode.Utils.Y2023.Day16;

class BeamSimulator {
    private record struct Beam(Point2D Point, CompassDirection Direction);

    private readonly Grid2D Mirrors;
    private EnergyGrid Beams;
    public int EnergizedCount => Beams.Find('#').Count();
    public int Width => Mirrors.Width;
    public int Height => Mirrors.Height;

    public BeamSimulator(string[] mirrors)
    {
        Mirrors = new(mirrors);
        Beams = NewBeams();
    }

    public void RunSimulation(Point2D startingPoint, CompassDirection startingDirection)
    {
        Beams = NewBeams();

        var unresolvedBeams = new Queue<Beam>();
        unresolvedBeams.Enqueue(new(startingPoint, startingDirection));

        var resolvedBeams = new HashSet<Beam>();

        while (unresolvedBeams.Count > 0) {
            var beam = unresolvedBeams.Dequeue();
            if (resolvedBeams.Contains(beam)) {
                continue;
            }
            resolvedBeams.Add(beam);

            var nextPoints = Mirrors.PointsTowards(beam.Point, beam.Direction).ToList();
            var mirrors = Mirrors[nextPoints];
            var spaces = mirrors.TakeWhile(m => m == '.').Count();

            if (spaces == nextPoints.Count) {
                Beams.Energize(nextPoints.Take(spaces));
            } else {
                Beams.Energize(nextPoints.Take(spaces + 1));

                var mirrorPoint = nextPoints[spaces];
                var mirror = Mirrors[mirrorPoint];
                foreach (CompassDirection direction in
                    ReflectedDirections[mirror][beam.Direction]) {

                    unresolvedBeams.Enqueue(new Beam(mirrorPoint, direction));
                }
            }
        }
    }

    private EnergyGrid NewBeams()
    {
        var grid = new List<string> ();
        for (int y = 0; y < Mirrors.Height; y++) {
            grid.Add(new String('.', Mirrors.Width));
        }

        return new([.. grid]);
    }

    private static Dictionary<char, Dictionary<CompassDirection, CompassDirection[]>> ReflectedDirections =>
        new() {
            {'|', new() {
                { CompassDirection.North, [CompassDirection.North]},
                { CompassDirection.South, [CompassDirection.South]},
                { CompassDirection.East, [CompassDirection.North, CompassDirection.South]},
                { CompassDirection.West, [CompassDirection.North, CompassDirection.South]},
            }},
            {'-', new() {
                { CompassDirection.North, [CompassDirection.East, CompassDirection.West]},
                { CompassDirection.South, [CompassDirection.East, CompassDirection.West]},
                { CompassDirection.East, [CompassDirection.East]},
                { CompassDirection.West, [CompassDirection.West]},
            }},
            {'/', new() {
                { CompassDirection.North, [CompassDirection.East]},
                { CompassDirection.South, [CompassDirection.West]},
                { CompassDirection.East, [CompassDirection.North]},
                { CompassDirection.West, [CompassDirection.South]},
            }},
            {'\\', new() {
                { CompassDirection.North, [CompassDirection.West]},
                { CompassDirection.South, [CompassDirection.East]},
                { CompassDirection.East, [CompassDirection.South]},
                { CompassDirection.West, [CompassDirection.North]},
            }},
        };
}
