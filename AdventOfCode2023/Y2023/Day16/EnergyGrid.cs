namespace AdventOfCode.Utils.Y2023.Day16;

class EnergyGrid : Grid2D
{
    public EnergyGrid(string[] input) : base(input) {  }
    public EnergyGrid(Grid2D other) : base(other) {  }

    public void Energize(IEnumerable<Point2D> points)
    {
        foreach (Point2D point in points) {
            this[point] = '#';
        }
    }
}
