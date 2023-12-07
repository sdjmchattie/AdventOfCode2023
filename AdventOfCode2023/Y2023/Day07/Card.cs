namespace AdventOfCode.Utils.Y2023.Day07;

enum Value {
    Joker = 1, Two, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Jack, Queen, King, Ace
}

class Card (char notation, bool jacksAreWild) : IComparable<Card>
{
    public readonly char Notation = notation;
    public readonly bool JacksAreWild = jacksAreWild;
    public Value Value {
        get {
            return Notation switch
            {
                'T' => Value.Ten,
                'J' => JacksAreWild ? Value.Joker : Value.Jack,
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
