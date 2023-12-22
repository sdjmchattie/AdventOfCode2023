using AdventOfCode.Utils.Y2023.Day22;

namespace AdventOfCode.Y2023;

class Day22 {
    private string[]? inputContents;
    private string[] InputContents =>
        inputContents ??= File.ReadAllLines($"Y2023/{GetType().Name}/input.txt");

    private BlockSimulator? _blockSimulator;
    private BlockSimulator BlockSimulator =>
        _blockSimulator ??= new BlockSimulator(InputContents);

    public object Part1()
    {
        BlockSimulator.CompactBlocks();
        return BlockSimulator.BlockSupportCounts().Values.Count(c => c == 0);
    }

    public object Part2()
    {
        BlockSimulator.CompactBlocks();
        return BlockSimulator.BlockSupportCounts().Values.Sum();
    }
}
