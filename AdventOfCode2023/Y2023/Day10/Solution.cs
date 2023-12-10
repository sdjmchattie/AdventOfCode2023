using AdventOfCode.Utils.Y2023.Day10;

namespace AdventOfCode.Y2023;

class Day10 {
    private string[]? inputContents;
    private string[] InputContents =>
        inputContents ??= File.ReadAllLines($"Y2023/{GetType().Name}/input.txt");

    private PipeMap PipeMap => new PipeMap(InputContents);

    public object Part1()
    {
        return PipeMap.Length / 2;
    }

    public object Part2()
    {
        return PipeMap.InnerGroundCount;
    }
}
