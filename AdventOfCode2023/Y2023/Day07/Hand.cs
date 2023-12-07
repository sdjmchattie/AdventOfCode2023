namespace AdventOfCode.Utils.Y2023.Day07;

enum Type {
    HighCard, OnePair, TwoPair, ThreeOfAKind, FullHouse, FourOfAKind, FiveOfAKind
}

class Hand : IComparable<Hand>
{
    public string Notation { get; }
    public int Bid { get; }

    public List<Card> Cards => Notation.Select(card => new Card(card)).ToList();
    public Type Type {
        get {
            var cardCounts = CardCounts();
            return cardCounts switch
            {
                var _ when cardCounts.Any(e => e.Value == 5) => Type.FiveOfAKind,
                var _ when cardCounts.Any(e => e.Value == 4) => Type.FourOfAKind,
                var counts when cardCounts.Any(e => e.Value == 3) => counts.Count == 2 ? Type.FullHouse : Type.ThreeOfAKind,
                var counts when cardCounts.Any(e => e.Value == 2) => counts.Count == 3 ? Type.TwoPair : Type.OnePair,
                _ => Type.HighCard,
            };
        }
    }

    public Hand(string notation, int bid)
    {
        if (notation.Length != 5) {
            throw new ArgumentException("must be exactly 5 characters long", nameof(notation));
        }

        Notation = notation;
        Bid = bid;
    }

    public int Winnings(int rank) => Bid * rank;

    public int CompareTo(Hand? other)
    {
        if (other == null) {
            return 1;
        }

        var typeComparison = Type.CompareTo(other.Type);
        if (typeComparison != 0) {
            return typeComparison;
        }

        for (int i = 0; i < Cards.Count; i++) {
            var cardComparison = Cards[i].CompareTo(other.Cards[i]);
            if (cardComparison != 0) {
                return cardComparison;
            }
        }

        return 0;
    }

    private Dictionary<Value, int> CardCounts()
    {
        return Cards
            .Select(card => card.Value)
            .Aggregate(new Dictionary<Value, int>(), (acc, key) => {
                acc[key] = acc.GetValueOrDefault(key, 0) + 1;
                return acc;
            });
    }
}
