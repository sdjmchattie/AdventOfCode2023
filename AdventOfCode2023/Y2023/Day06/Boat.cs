using System.Dynamic;

namespace AdventOfCode.Utils.Y2023.Day06;

class Boat {
    public long Time { get; init; }
    public long Distance { get; init; }

    public long WinCount()
    {
        var winCount = 0;
        for (long t = 0; t < Time; t++) {
            winCount += (Time - t) * t > Distance ? 1 : 0;
        }

        return winCount;
    }
}
