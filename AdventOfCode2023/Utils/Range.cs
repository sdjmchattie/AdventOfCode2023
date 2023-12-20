namespace AdventOfCode.Utils;

public readonly record struct Range(int Start, int End) {
    public readonly int Length => End - Start + 1;
}
