namespace AdventOfCode.Utils.Y2023.Day11;

class Universe(string[] input) : Grid2D(input) {
    private IEnumerable<int> ExpandingRows
    {
        get {
            for (int i = 0; i < Height; i++) {
                var chars = this[PointsAlong(i, CompassDirection.East)];
                if (chars.All(c => c == '.')) {
                    yield return i;
                }
            }
        }
    }

    private IEnumerable<int> ExpandingColumns
    {
        get {
            for (int i = 0; i < Width; i++) {
                var chars = this[PointsAlong(i, CompassDirection.South)];
                if (chars.All(c => c == '.')) {
                    yield return i;
                }
            }
        }
    }

    public IEnumerable<long> DistanceBetweenGalaxies(int expansion)
    {
        var expandingRows = ExpandingRows.ToList();
        var expandingColumns = ExpandingColumns.ToList();
        var galaxyPoints = Find('#').ToList();
        for (int i = 0; i < galaxyPoints.Count; i++) {
            for (int j = i; j < galaxyPoints.Count; j++) {
                if (i == j) { continue; }
                var first = galaxyPoints[i];
                var second = galaxyPoints[j];
                var minX = Math.Min(first.X, second.X);
                var maxX = Math.Max(first.X, second.X);
                var minY = Math.Min(first.Y, second.Y);
                var maxY = Math.Max(first.Y, second.Y);
                var expandedColumns = expandingColumns.Where(col => col > minX && col < maxX);
                var expandedRows = expandingRows.Where(row => row > minY && row < maxY);
                yield return maxX - minX + maxY - minY + (expandedColumns.Count() + expandedRows.Count()) * expansion;
            }
        }
    }
}
