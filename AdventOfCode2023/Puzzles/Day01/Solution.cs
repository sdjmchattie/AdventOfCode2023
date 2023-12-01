using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using AdventOfCode.Utils;
using Microsoft.VisualBasic;

namespace AdventOfCode.Y2023;

class Day01 {
    List<int>? calibrationValues;

    private void ParseInput() {
        if (calibrationValues is not null) {
            return;
        }

        var inputLines = File.ReadAllLines("Puzzles/Day01/input.txt");
        var rxLeft = new Regex(@"^\D*(\d)");
        var rxRight = new Regex(@"(\d)\D*$");
        calibrationValues = inputLines
            .Select<string, IEnumerable<Match>>(line => [rxLeft.Match(line), rxRight.Match(line)])
            .Select(matches => matches.First().Groups[1].ToString() + matches.Last().Groups[1].ToString())
            .Select(int.Parse)
            .ToList();
    }

    public string Part1()
    {
        ParseInput();
        return calibrationValues!.Sum().ToString();
    }

    public string Part2()
    {
        ParseInput();
        return "Hello World!";
    }
}
