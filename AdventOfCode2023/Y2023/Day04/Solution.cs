using System.Text.RegularExpressions;

namespace AdventOfCode.Y2023;

class Card {
    public int Copies { get; set; }
    public int Id { get; init; }
    public IEnumerable<int> WinningNumbers { get; init; } = new List<int>();
    public IEnumerable<int> PlayedNumbers { get; init; } = new List<int>();
    public int WinCount
    {
        get {
            var self = this;
            return PlayedNumbers.Count(pn => self.WinningNumbers.Contains(pn));
        }
    }
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
            Copies = 1,
            Id = id,
            WinningNumbers = winningNumbers,
            PlayedNumbers = playedNumbers
        };
    }

    private IEnumerable<Card> ParsedInput() {
        if (parsedInput != null) {
            return parsedInput!;
        }

        var inputLines = File.ReadAllLines($"Y2023/{GetType().Name}/input.txt");
        parsedInput = inputLines.Select(ParseCard).ToList();

        return parsedInput;
    }

    public object Part1()
    {
        var cards = ParsedInput();
        var cardValues = cards.Select(card => {
            return card.WinCount == 0 ? 0 : Math.Pow(2, card.WinCount - 1);
        });

        return cardValues.Sum();
    }

    public object Part2()
    {
        var cards = ParsedInput().ToList();

        for (int idx = 0; idx < cards.Count; idx++) {
            var card = cards[idx];
            for (int j = 0; j < card.WinCount; j++) {
                var latterIndex = idx + j + 1;
                if (latterIndex < cards.Count) {
                    cards[latterIndex].Copies += card.Copies;
                }
            }
        }

        return cards.Select(card => card.Copies).Sum();
    }
}
