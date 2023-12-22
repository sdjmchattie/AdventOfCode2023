namespace AdventOfCode.Utils.Y2023.Day18;

class Terrain
{
    private readonly IEnumerable<Instruction> Instructions;
    private readonly HashSet<Point2D> BorderPoints;
    private long ExcavationCount = 0L;

    public Terrain(IEnumerable<Instruction> instructions)
    {
        Instructions = instructions;
        BorderPoints = ParseBorders();
    }

    public long ExcavatedArea
    {
        get {
            if (ExcavationCount == 0L) {
                Excavate();
            }

            return ExcavationCount;
        }
    }

    private HashSet<Point2D> ParseBorders()
    {
        var borders = new HashSet<Point2D>();
        var x = 0;
        var y = 0;

        foreach (Instruction instruction in Instructions) {
            var offset = instruction.Direction.GetOffset();

            for (int i = 0; i < instruction.Distance; i++) {
                x += offset.X;
                y += offset.Y;

                borders.Add(new(x, y));
            }
        }

        return borders;
    }

    private void Excavate()
    {
        var xStart = BorderPoints.MinBy(p => p.X).X - 3;
        ExcavationCount = BorderPoints.GroupBy(p => p.Y).Aggregate(0L, (lineAcc, line) => {
            var y = line.Key;
            var prevX = xStart;
            var inBlock = false;
            var blockStart = xStart;
            var insideBorder = false;

            var lineFill = line.Select(p => p.X).Order().Aggregate(0L, (fillAcc, curX) => {
                var fill = 1;
                var diff = curX - prevX;

                if (diff == 1) {
                    // This point is the next in a block.
                    if (!inBlock) {
                        inBlock = true;
                        blockStart = prevX;
                    }
                } else {
                    if (inBlock) {
                        // Check whether the block swapped inside/outside border.
                        inBlock = false;
                        var uShapeBorder =
                            BorderPoints.Contains(new(blockStart, y - 1)) &&
                            BorderPoints.Contains(new(prevX, y - 1));
                        var nShapeBorder =
                            BorderPoints.Contains(new(blockStart, y + 1)) &&
                            BorderPoints.Contains(new(prevX, y + 1));
                        if (uShapeBorder || nShapeBorder) {
                            // Undo the change we made to inside border at the start of the block.
                            insideBorder = !insideBorder;
                        }
                    }

                    if (insideBorder) {
                        fill += diff - 1;
                    }

                    if (!inBlock) {
                        insideBorder = !insideBorder;
                    }
                }

                prevX = curX;
                return fillAcc + fill;
            });

            return lineAcc + lineFill;
        });
    }
}
