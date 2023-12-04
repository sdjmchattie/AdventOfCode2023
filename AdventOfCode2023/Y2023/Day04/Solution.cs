using System.Text.RegularExpressions;

namespace AdventOfCode.Y2023;

readonly struct Card {
    public readonly int Id { get; init; }
    public IEnumerable<int> WinningNumbers { get; init; }
    public IEnumerable<int> PlayedNumbers { get; init; }
}

class Day04 {
    private IEnumerable<Card>? parsedInput;

    Regex lineRegex = new Regex(@"^Card\s+(\d+):\s+(.+)\s+\|\s+(.+)$");
    Regex numberRegex = new Regex(@"\d+");

    private Card ParseCard(string line) {
        var lineResult = lineRegex.Match(line);
        var id = int.Parse(lineResult.Groups[1].ToString());

        var winningNumbersSection = lineResult.Groups[2].ToString();
        var wnResults = numberRegex.Matches(winningNumbersSection);
        var winningNumbers = wnResults.Select(r => r.ToString()).Select(int.Parse);

        var playedNumbersSection = lineResult.Groups[3].ToString();
        var pnResults = numberRegex.Matches(playedNumbersSection);
        var playedNumbers = pnResults.Select(r => r.ToString()).Select(int.Parse);

        return new Card() {
            Id = id,
            WinningNumbers = winningNumbers,
            PlayedNumbers = playedNumbers
        };
    }

    private IEnumerable<Card> ParsedInput() {
        if (parsedInput != null) {
            return parsedInput!;
        }

        var inputLines = File.ReadAllLines("Y2023/Day04/input.txt");
        parsedInput = inputLines.Select(ParseCard).ToList();

        return parsedInput;
    }

    public object Part1()
    {
        var cards = ParsedInput();
        var cardValues = cards.Select(card => {
            var winCount = card.PlayedNumbers.Count(pn => card.WinningNumbers.Any(wn => wn == pn));

            return winCount == 0 ? 0 : Math.Pow(2, winCount - 1);
        });

        return cardValues.Sum();
    }

    public object Part2()
    {
        var input = ParsedInput();
        return "Part 2 Solution";
    }
}
