using AdventOfCode.Utils.Y2023.Day12;

namespace AdventOfCode.Y2023;

class Day12 {
    private string[]? inputContents;
    private string[] InputContents =>
        inputContents ??= File.ReadAllLines($"Y2023/{GetType().Name}/input.txt");

    private IEnumerable<SpringReport> SpringReports
    {
        get {
            return InputContents.Select(line => {
                var split = line.Split(' ');
                var report = split[0];
                var summary = split[1].Split(',').Select(int.Parse).ToArray();

                return new SpringReport(report, summary);
            });
        }
    }

    public object Part1()
    {
        return SpringReports.Aggregate(0, (acc, report) => acc + report.ValidInterpretationCount);
    }

    public object Part2()
    {
        var input = InputContents;
        return "Part 2 Solution";
    }
}
