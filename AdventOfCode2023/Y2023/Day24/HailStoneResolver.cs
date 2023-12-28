namespace AdventOfCode.Utils.Y2023.Day24;

public class HailStoneResolver(string[] input)
{
    private List<HailStone> HailStones = input
            .Select(line => line.Split(" @ "))
            .Select(line => line[0].Split(", ").Concat(line[1].Split(", ")))
            .Select(line => line.Select(long.Parse).ToArray())
            .Select(v => new HailStone(v[0], v[1], v[2], v[3], v[4], v[5]))
            .ToList();

    public int CollidingStonesCount
    {
        get {
            var count = 0;

            for (int i = 0; i < HailStones.Count - 1; i++) {
                for (int j = i + 1; j < HailStones.Count; j++) {
                    var first = HailStones[i];
                    var second = HailStones[j];

                    var point = FindCollisionPoint(first, second);
                    if (point == null) { continue; }
                    var (x, y) = point.Value;

                    if (Math.Sign(x - first.PX) != Math.Sign(first.VX) ||
                        Math.Sign(x - second.PX) != Math.Sign(second.VX)) {
                        continue;
                    }

                    if (x >= 200000000000000D && x <= 400000000000000D &&
                        y >= 200000000000000D && y <= 400000000000000D) {
                            count++;
                        }
                }
            }

            return count;
        }
    }

    private static (double X, double Y)? FindCollisionPoint(
        HailStone first, HailStone second)
    {
        if (first.M() == second.M()) { return null; }

        var x = (first.C() - second.C()) / (second.M() - first.M());
        var y = first.M() * x + first.C();

        return (x, y);
    }

    private record struct HailStone(long PX, long PY, long PZ, long VX, long VY, long VZ)
    {
        public readonly double M() => VY / (double)VX;
        public readonly double C() => PY - M() * PX;
    }
}
