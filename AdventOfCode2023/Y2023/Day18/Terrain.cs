namespace AdventOfCode.Utils.Y2023.Day18;

public record struct DiggerInstruction(CompassDirection Direction, int Distance, string ColourHex);

class Terrain
{
    private (int minX, int maxX, int minY, int maxY)? gridDimensions;
    private readonly IEnumerable<DiggerInstruction> Instructions;
    private readonly FloodFillGrid TerrainGrid;
    private readonly Point StartPoint;

    public Terrain(IEnumerable<DiggerInstruction> instructions)
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
                TerrainGrid.OutputGrid();
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

            foreach (DiggerInstruction instruction in Instructions) {
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
        foreach (DiggerInstruction instruction in Instructions) {
            var offset = instruction.Direction.GetOffset();
            var points = new List<Point>();
            for (int i = 0; i < instruction.Distance; i++) {
                point = point.OffsetBy(offset);
                points.Add(point);
            }

            TerrainGrid.Draw(points);
        }
    }
}
