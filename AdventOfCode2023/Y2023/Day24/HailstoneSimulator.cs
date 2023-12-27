namespace AdventOfCode.Utils.Y2023.Day24;

public class HailstoneSimulator(string[] input)
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
                    var (time, x, y) = FindCollision(HailStones[i], HailStones[j]);
                    if (time >= 0 &&
                        x >= 200000000000000D && x <= 400000000000000D &&
                        y >= 200000000000000D && y <= 400000000000000D) {
                            count++;
                        }
                }
            }

            return count;
        }
    }

    private static (int Time, double X, double Y) FindCollision(
        HailStone first, HailStone second)
    {
        return (1, 200000000000000D, 200000000000000D);
    }

    private record struct HailStone(long PX, long PY, long PZ, long VX, long VY, long VZ);
}
