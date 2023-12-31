namespace AdventOfCode.Utils.Y2023.Day18;

class MemoryHungryTerrain
{
    private (int minX, int maxX, int minY, int maxY)? gridDimensions;
    private readonly IEnumerable<Instruction> Instructions;
    private readonly FloodFillGrid TerrainGrid;
    private readonly Point2D StartPoint;

    public MemoryHungryTerrain(IEnumerable<Instruction> instructions)
    {
        Instructions = instructions;
        var (minX, maxX, minY, maxY) = GetGridDimensions();
        TerrainGrid = new(GridInitializer(minX, maxX, minY, maxY));
        StartPoint = new(minX * -1, minY * -1);
    }

    public int ExcavatedArea
    {
        get {
            if (!TerrainGrid.Find('#').Any()) {
                Excavate();
                TerrainGrid.FloodFill();
            }

            return TerrainGrid.Find('#').Count();
        }
    }

    private static string[] GridInitializer(int minX, int maxX, int minY, int maxY)
    {
        var width = maxX - minX + 1;
        var height = maxY - minY + 1;

        return Enumerable.Repeat(new string('.', width), height).ToArray();
    }

    private (int minX, int maxX, int minY, int maxY) GetGridDimensions()
    {
        if (gridDimensions == null) {
            var x = 0;
            var y = 0;
            var minX = 0;
            var maxX = 0;
            var minY = 0;
            var maxY = 0;

            foreach (Instruction instruction in Instructions) {
                var offset = instruction.Direction.GetOffset();

                x += offset.X * instruction.Distance;
                y += offset.Y * instruction.Distance;

                minX = Math.Min(minX, x);
                maxX = Math.Max(maxX, x);
                minY = Math.Min(minY, y);
                maxY = Math.Max(maxY, y);
            }

            gridDimensions = (minX, maxX, minY, maxY);
        }

        return gridDimensions.Value;
    }

    private void Excavate()
    {
        var point = StartPoint;
        foreach (Instruction instruction in Instructions) {
            var offset = instruction.Direction.GetOffset();
            var points = new List<Point2D>();
            for (int i = 0; i < instruction.Distance; i++) {
                point = point.OffsetBy(offset);
                points.Add(point);
            }

            TerrainGrid.Draw(points);
        }
    }
}
