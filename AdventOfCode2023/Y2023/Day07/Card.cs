namespace AdventOfCode.Utils.Y2023.Day07;

enum Value {
    Two = 2, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Jack, Queen, King, Ace
}

class Card (char notation) : IComparable<Card>
{
    public readonly char Notation = notation;
    public Value Value {
        get {
            return Notation switch
            {
                'T' => Value.Ten,
                'J' => Value.Jack,
                'Q' => Value.Queen,
                'K' => Value.King,
                'A' => Value.Ace,
                var val => (Value)int.Parse(val.ToString()),
            };
        }
    }

    public int CompareTo(Card? other)
    {
        if (other == null) {
            return 1;
        }

        return Value.CompareTo(other.Value);
    }
}
