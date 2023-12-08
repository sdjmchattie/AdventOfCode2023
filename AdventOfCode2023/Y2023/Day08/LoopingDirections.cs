using System.Collections;

namespace AdventOfCode.Utils.Y2023.Day08;

enum Direction : int
{
    Left, Right
}

class LoopingDirections : IEnumerator<Direction>
{
    public readonly string Sequence;
    private int position = 0;

    public LoopingDirections(string sequence)
    {
        if (sequence.Any(s => s != 'L' && s != 'R')) {
            throw new ArgumentException(
                "The sequence can only consist of L and R characters",
                nameof(sequence)
            );
        }

        Sequence = sequence;
    }

    public Direction Current => Sequence[position] == 'L' ? Direction.Left : Direction.Right;

    object IEnumerator.Current => Current;

    public void Dispose() { } // Nothing to dispose.

    public bool MoveNext()
    {
        position++;

        if (position >= Sequence.Length) {
            Reset();
        }

        return true;
    }

    public void Reset()
    {
        position = 0;
    }
}
