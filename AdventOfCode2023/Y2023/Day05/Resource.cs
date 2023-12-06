namespace AdventOfCode.Utils.Y2023.Day05;

class Resource(long idStart, long rangeLength)
{
    public readonly long idStart = idStart;
    public readonly long idEnd = idStart + rangeLength - 1;

    public static Resource ByRangeLength(long idStart, long rangeLength) =>
        new(idStart, rangeLength);

    public static Resource ByEndpoints(long idStart, long idEnd) =>
        new(idStart, idEnd - idStart + 1);
}
