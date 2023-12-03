using System.Text.RegularExpressions;

namespace AdventOfCode.Y2023;

class Day01 {
    private IEnumerable<int> ParseInputPart1() {
        var inputLines = File.ReadAllLines("Puzzles/Day01/input.txt");
        var rxLeft = new Regex(@"^\D*(\d)");
        var rxRight = new Regex(@"(\d)\D*$");
        return inputLines
            .Select<string, IEnumerable<Match>>(line => [rxLeft.Match(line), rxRight.Match(line)])
            .Select(matches => matches.First().Groups[1].ToString() + matches.Last().Groups[1].ToString())
            .Select(int.Parse);
    }

    public string Part1()
    {
        var input = ParseInputPart1();
        return input.Sum().ToString();
    }

    private string NumericWordsToDigits(string numericWords) {
        var output = "";
        var regex = new Regex(@"(?=(\d|one|two|three|four|five|six|seven|eight|nine))");
        var matches = regex.Matches(numericWords);
        var sortedMatches = matches.OrderBy(e => e.Index);

        foreach (Match match in sortedMatches) {
            output += match.Groups[1].ToString();
        }

        return output
            .Replace("one", "1")
            .Replace("two", "2")
            .Replace("three", "3")
            .Replace("four", "4")
            .Replace("five", "5")
            .Replace("six", "6")
            .Replace("seven", "7")
            .Replace("eight", "8")
            .Replace("nine", "9");
    }

    private IEnumerable<int> ParseInputPart2() {
        var inputLines = File.ReadAllLines("Puzzles/Day01/input.txt");
        var rxLeft = new Regex(@"^\D*(\d)");
        var rxRight = new Regex(@"(\d)\D*$");
        return inputLines
            .Select(NumericWordsToDigits)
            .Select<string, IEnumerable<Match>>(line => [rxLeft.Match(line), rxRight.Match(line)])
            .Select(matches => matches.First().Groups[1].ToString() + matches.Last().Groups[1].ToString())
            .Select(int.Parse);
    }

    public string Part2()
    {
        var input = ParseInputPart2();
        return input.Sum().ToString();
    }
}
