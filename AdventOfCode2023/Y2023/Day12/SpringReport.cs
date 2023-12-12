
using System.Text.RegularExpressions;

namespace AdventOfCode.Utils.Y2023.Day12;

class SpringReport(string report, int[] summary) {
    private readonly string Report = report;
    private Regex ValidPattern
    {
        get {
            var pattern = @"^\.*";

            foreach (var brokenCount in summary[..^1]) {
                pattern += @"#{" + brokenCount + @"}\.+";
            }

            pattern += @"#{" + summary[^1] + @"}\.*$";

            return new(pattern);
        }
    }

    private static List<string> Permutations(string report)
    {
        var permutations = new List<string>();
        var index = report.IndexOf('?');

        if (index == -1) {
            permutations.Add(report);
        } else {
            permutations.AddRange(Permutations($"{report[0..index]}.{report[(index+1)..]}"));
            permutations.AddRange(Permutations($"{report[0..index]}#{report[(index+1)..]}"));
        }

        return permutations;
    }

    private bool IsValid(string permutation) => ValidPattern.IsMatch(permutation);

    public int ValidInterpretationCount
    {
        get {
            return Permutations(Report).Count(IsValid);
        }
    }
}
