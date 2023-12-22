using System.Collections.ObjectModel;

namespace AdventOfCode.Utils.Y2023.Day22;

public class Block {
    private static int NextId = 1;
    public readonly int Id;
    private readonly List<Point3D> _points;
    public ReadOnlyCollection<Point3D> Points;
    public List<int> SupportedBy = [];

    public Block(List<Point3D> points)
    {
        Id = NextId++;
        _points = points;
        Points = _points.AsReadOnly();
    }

    public Block(Block source, List<Point3D> points)
    {
        Id = source.Id;
        _points = points;
        Points = _points.AsReadOnly();
        SupportedBy = [.. source.SupportedBy];
    }

    public Block(Block source) : this(source, source._points) {  }

    public int LowestZValue => Points.MinBy(p => p.Z).Z;
}

public class BlockSimulator(string[] input)
{
    private readonly List<Block> Blocks = ParseInput(input);
    private readonly List<Block> CompactedBlocks = [];
    private readonly Dictionary<Block, int> BlockToNumberSupported = [];

    public void CompactBlocks()
    {
        CompactedBlocks.Clear();
        var blockQueue = new PriorityQueue<Block, int>(Blocks.Select(b => (b, b.LowestZValue)));
        while (blockQueue.Count > 0) {
            var block = new Block(blockQueue.Dequeue());
            if (block.LowestZValue == 1) {
                block.SupportedBy.Add(0);
                CompactedBlocks.Add(block);
                continue;
            }

            var newPoints = block.Points.Select(p => new Point3D(p.X, p.Y, p.Z - 1)).ToList();
            var supportingBlocks = CompactedBlocks.Where(b => b.Points.Any(p => newPoints.Contains(p)));
            if (supportingBlocks.Any()) {
                block.SupportedBy.AddRange(supportingBlocks.Select(sb => sb.Id));
                CompactedBlocks.Add(block);
                continue;
            }

            var movedBlock = new Block(block, newPoints);
            blockQueue.Enqueue(movedBlock, movedBlock.LowestZValue);
        }
    }

    public Dictionary<Block, int> BlockSupportCounts()
    {
        if (BlockToNumberSupported.Count != CompactedBlocks.Count) {
            BlockToNumberSupported.Clear();
            foreach (Block block in CompactedBlocks) {
                var unfallen = CompactedBlocks.Select(b => new Block(b)).ToList();
                var toRemove = new HashSet<int>(new int[] { block.Id });
                var removedCount = -1;  // So we don't count the initial block.
                while (toRemove.Count > 0) {
                    var removedBlockId = toRemove.First();
                    toRemove.Remove(removedBlockId);

                    var removedBlock = unfallen.First(b => b.Id == removedBlockId);
                    unfallen.Remove(removedBlock);
                    removedCount++;

                    unfallen.ForEach(cb => cb.SupportedBy.Remove(removedBlock.Id));

                    foreach (Block willFall in unfallen.Where(b => b.SupportedBy.Count == 0)) {
                        toRemove.Add(willFall.Id);
                    }
                }

                BlockToNumberSupported[block] = removedCount;
            }
        }

        return BlockToNumberSupported.ToDictionary();
    }

    private static IEnumerable<Point3D> GeneratePoints(int[] start, int[] end, int index)
    {
        var minValue = Math.Min(start[index], end[index]);
        var maxValue = Math.Max(start[index], end[index]);
        for (int i = minValue; i <= maxValue; i++) {
            yield return new Point3D(
                index == 0 ? i : start[0],
                index == 1 ? i : start[1],
                index == 2 ? i : start[2]
            );
        }
    }

    private static List<Block> ParseInput(string[] input)
    {
        return input.Select(line => {
            var coords = line.Split('~');
            var start = coords[0].Split(',').Select(int.Parse).ToArray();
            var end = coords[1].Split(',').Select(int.Parse).ToArray();
            var blocks = (start, end) switch {
                _ when start[0] != end[0] => GeneratePoints(start, end, 0),
                _ when start[1] != end[1] => GeneratePoints(start, end, 1),
                _ when start[2] != end[2] => GeneratePoints(start, end, 2),
                _ => new List<Point3D>() { new(start[0], start[1], start[2]) },
            };

            return new Block(blocks.ToList());
        }).ToList();
    }
}
