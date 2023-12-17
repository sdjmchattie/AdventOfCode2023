namespace AdventOfCode.Utils.Y2023.Day17;

class City : DjikstraGrid
{
    public City(string[] input) : base(input) {  }
    public City(Grid2D other) : base(other) {  }

    override protected IEnumerable<PathPoint> NextPathPoints()
    {
        foreach (PathPoint pathPoint in base.NextPathPoints()) {
            var prevPath = CurrentPoint.previousPathPoint;
            var prevPrevPath = prevPath?.previousPathPoint;
            if (prevPath == null || prevPrevPath == null) {
                yield return pathPoint;
                continue;
            }

            var xs = new HashSet<int> {
                prevPrevPath.Point.X,
                prevPath.Point.X,
                CurrentPoint.Point.X,
                pathPoint.Point.X,
            };

            var ys = new HashSet<int> {
                prevPrevPath.Point.Y,
                prevPath.Point.Y,
                CurrentPoint.Point.Y,
                pathPoint.Point.Y,
            };

            if (xs.Count > 1 && ys.Count > 1) {
                yield return pathPoint;
            }
        }
    }
}
