using AdventOfCode.Utils;
using AdventOfCode.Utils.Y2023.Day13;

namespace AdventOfCode.Y2023;

class Day13 {
    private string[]? inputContents;
    private string[] InputContents =>
        inputContents ??= File.ReadAllLines($"Y2023/{GetType().Name}/input.txt");

    private IEnumerable<string[]> PatternBlocks
    {
        get {
            var block = new List<string>();

            foreach (string line in InputContents) {
                if (line.Length > 0) {
                    block.Add(line);
                } else {
                    yield return block.ToArray();
                    block.Clear();
                }
            }

            yield return block.ToArray();
        }
    }

    private List<Pattern> Patterns =>
        PatternBlocks.Select(block => new Pattern(block)).ToList();

    private int SumMirrors(int variations)
    {
        var mirrors = Patterns
            .Select(pattern => pattern.FindMirrors(variations));
        var colSum = mirrors
            .Where(mirror => mirror.First().axis == Axis.Vertical)
            .Select(mirror => mirror.First().index)
            .Sum();
        var rowSum = mirrors
            .Where(mirror => mirror.First().axis == Axis.Horizontal)
            .Select(mirror => mirror.First().index)
            .Sum();

        return colSum + rowSum * 100;
    }

    public object Part1()
    {
        return SumMirrors(0);
    }

    public object Part2()
    {
        return SumMirrors(1);
    }
}
