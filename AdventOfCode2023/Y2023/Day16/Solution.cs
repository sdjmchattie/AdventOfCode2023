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
        simulator.RunSimulation(new Point2D(-1, 0), CompassDirection.East);
        return simulator.EnergizedCount;
    }

    public object Part2()
    {
        var maxEnergize = 0;
        var simulator = BeamSimulator;

        for (int x = 0; x < simulator.Width; x++) {
            simulator.RunSimulation(new Point2D(x, -1), CompassDirection.South);
            maxEnergize = Math.Max(maxEnergize, simulator.EnergizedCount);

            simulator.RunSimulation(new Point2D(x, simulator.Height), CompassDirection.North);
            maxEnergize = Math.Max(maxEnergize, simulator.EnergizedCount);
        }

        for (int y = 0; y < simulator.Height; y++) {
            simulator.RunSimulation(new Point2D(-1, y), CompassDirection.East);
            maxEnergize = Math.Max(maxEnergize, simulator.EnergizedCount);

            simulator.RunSimulation(new Point2D(simulator.Width, y), CompassDirection.West);
            maxEnergize = Math.Max(maxEnergize, simulator.EnergizedCount);
        }

        return maxEnergize;
    }
}
