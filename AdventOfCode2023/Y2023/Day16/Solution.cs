using AdventOfCode.Utils;
using AdventOfCode.Utils.Y2023.Day16;

namespace AdventOfCode.Y2023;

class Day16 {
    private string[]? inputContents;
    private string[] InputContents =>
        inputContents ??= File.ReadAllLines($"Y2023/{GetType().Name}/input.txt");
    private BeamSimulator? beamSimulator;
    private BeamSimulator BeamSimulator =>
        beamSimulator ??= new BeamSimulator(InputContents);

    public object Part1()
    {
        var simulator = BeamSimulator;
        simulator.RunSimulation(new Point(-1, 0), CompassDirection.East);
        return simulator.EnergizedCount;
    }

    public object Part2()
    {
        var input = InputContents;
        return "Part 2 Solution";
    }
}
