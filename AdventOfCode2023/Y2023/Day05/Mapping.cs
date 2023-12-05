namespace AdventOfCode.Y2023;

class Mapping(long destinationStart, long sourceStart, long rangeLength)
{
    public readonly long sourceStart = sourceStart;
    public readonly long sourceEnd = sourceStart + rangeLength - 1;
    public readonly long offset = destinationStart - sourceStart;

    public bool ContainsSource(long source) => source >= sourceStart && source <= sourceEnd;
    public long MapToDestination(long source) => source + offset;
}
