namespace AdventOfCode.Utils.Y2023.Day07;

enum Type {
    HighCard, OnePair, TwoPair, ThreeOfAKind, FullHouse, FourOfAKind, FiveOfAKind
}

class Hand : IComparable<Hand>
{
    public string Notation { get; }
    public int Bid { get; }
    public bool JacksAreWild { get; }

    public List<Card> Cards => Notation.Select(card => new Card(card, JacksAreWild)).ToList();
    public Type Type {
        get {
            var cardCounts = CardCounts();
            var jokers = cardCounts.GetValueOrDefault(Value.Joker);
            var nonJokers = cardCounts.Where(e => e.Key != Value.Joker).ToDictionary();
            return nonJokers switch
            {
                var _ when jokers == 5 => Type.FiveOfAKind,
                var cc when cc.Any(e => e.Value + jokers == 5) => Type.FiveOfAKind,
                var cc when cc.Any(e => e.Value + jokers == 4) => Type.FourOfAKind,
                var cc when cc.Any(e => e.Value + jokers == 3) => cc.Count == 2 ? Type.FullHouse : Type.ThreeOfAKind,
                var cc when cc.Any(e => e.Value + jokers == 2) => cc.Count == 3 ? Type.TwoPair : Type.OnePair,
                _ => Type.HighCard,
            };
        }
    }

    public Hand(string notation, int bid, bool jacksAreWild)
    {
        if (notation.Length != 5) {
            throw new ArgumentException("must be exactly 5 characters long", nameof(notation));
        }

        Notation = notation;
        Bid = bid;
        JacksAreWild = jacksAreWild;
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
