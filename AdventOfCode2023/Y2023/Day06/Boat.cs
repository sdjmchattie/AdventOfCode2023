using System.Dynamic;

namespace AdventOfCode.Utils.Y2023.Day06;

class Boat {
    public int Time { get; init; }
    public int Distance { get; init; }

    public int WinCount() =>
        Enumerable.Range(0, Time).Count(t => (Time - t) * t > Distance);
}
