using AdventOfCode.Utils.Y2023.Day11;

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

    public object Part1()
    {
        var mirrorColumnSum = Patterns
            .Select(pattern => pattern.MirrorColumn ?? 0)
            .Sum();

        var mirrorRowSum = Patterns
            .Select(pattern => pattern.MirrorRow ?? 0)
            .Sum();

        return mirrorColumnSum + mirrorRowSum * 100;
    }

    public object Part2()
    {
        var input = InputContents;
        return "Part 2 Solution";
    }
}
