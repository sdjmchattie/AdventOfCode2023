using AdventOfCode.Utils.Y2023.Day12;

namespace AdventOfCode.Y2023;

// Solution for part 2 basically stolen from https://github.com/tmbarker/advent-of-code/blob/main/Solutions/Y2023/D12/Solution.cs
// It was more than I could work out how to do!

class Day12 {
    private readonly record struct Report(string Pattern, int[] Runs);
    private string[]? inputContents;
    private string[] InputContents =>
        inputContents ??= File.ReadAllLines($"Y2023/{GetType().Name}/input.txt");

    private IEnumerable<Report> SmallSpringReports
    {
        get {
            return InputContents.Select(line => {
                var split = line.Split(' ');
                var pattern = split[0];
                var runs = split[1].Split(',').Select(int.Parse).ToArray();

                return new Report(pattern, runs);
            });
        }
    }

    private IEnumerable<Report> LargeSpringReports
    {
        get {
            return InputContents.Select(line => {
                var split = line.Split(' ');
                var pattern = string.Join("?", Enumerable.Repeat(split[0], 5));
                var runsOnce = split[1].Split(',').Select(int.Parse);
                var runs = Enumerable.Repeat(runsOnce, 5)
                    .SelectMany(i => i)
                    .ToArray();

                return new Report(pattern, runs);
            });
        }
    }

    public object Part1()
    {
        return SmallSpringReports.Aggregate(0L, (acc, report) => acc + CountArrangements(report));
    }

    public object Part2()
    {
        return LargeSpringReports.Aggregate(0L, (acc, report) => acc + CountArrangements(report));
    }

    private static long CountArrangements(Report report)
    {
        var memo = new Dictionary<MatchState, long>();
        var initial = MatchState.Initial(report.Pattern);

        return CountArrangements(r: report, s: initial, m: memo);
    }

    private static long CountArrangements(Report r, MatchState s, Dictionary<MatchState, long> m)
    {
        if (m.TryGetValue(s, out var cached))
        {
            return cached;
        }

        if (s.Si == r.Pattern.Length)
        {
            var tailRunValid = !s.Ir || s.Rl == r.Runs[s.Ri];
            var matchSuccess = tailRunValid && s.Ri + 1 == r.Runs.Length;

            m[s] = matchSuccess ? s.N : 0L;
            return m[s];
        }

        switch (c: s.C, inRun: s.Ir)
        {
            case (c: '.', inRun: true)  when s.Rl != r.Runs[s.Ri]:
            case (c: '#', inRun: true)  when s.Rl >= r.Runs[s.Ri]:
            case (c: '#', inRun: false) when s.Ri + 1 >= r.Runs.Length:
                m[s] = 0L;
                break;
            case (c: '?', inRun: _):
                m[s] = CountArrangements(r, s: s.Replace('.'), m) + CountArrangements(r, s: s.Replace('#'), m);
                break;
            case (c: _, inRun: _):
                m[s] = CountArrangements(r, s: s.Advance(), m);
                break;
        }

        return m[s];
    }
}
