namespace AdventOfCode.Utils.Y2023.Day16;

class EnergyGrid : Grid2D
{
    public EnergyGrid(string[] input) : base(input) {  }
    public EnergyGrid(Grid2D other) : base(other) {  }

    public void Energize(IEnumerable<Point> points)
    {
        foreach (Point point in points) {
            this[point] = '#';
        }
    }
}
