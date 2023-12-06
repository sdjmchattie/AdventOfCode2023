namespace AdventOfCode.Utils.Y2023.Day05;

class Mapping(long destinationStart, long sourceStart, long rangeLength)
{
    public readonly long sourceStart = sourceStart;
    public readonly long sourceEnd = sourceStart + rangeLength - 1;
    public readonly long offset = destinationStart - sourceStart;

    public bool CoversResource(Resource resource) => resource.idStart <= sourceEnd && resource.idEnd >= sourceStart;
    public long MapToDestination(long source) => source + offset;
}
