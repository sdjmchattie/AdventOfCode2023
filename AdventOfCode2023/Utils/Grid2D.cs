namespace AdventOfCode.Utils;

class Grid2D(string[] input)
{
    private readonly List<List<char>> grid =
        input.Select(line => line.ToList()).ToList();

    public int Width
    {
        get { return grid[0].Count; }
    }

    public int Height
    {
        get { return grid.Count; }
    }

    public char this[int x, int y]
    {
        get { return grid[y][x]; }
    }

    public IEnumerable<char> Neighbours(int x, int y)
    {
        int minX = Math.Max(0, x - 1);
        int maxX = Math.Min(this.Width - 1, x + 1);
        int minY = Math.Max(0, y - 1);
        int maxY = Math.Min(this.Height - 1, y + 1);

        for (int curX = minX; curX <= maxX; curX++) {
            for (int curY = minY; curY <= maxY; curY++) {
                if (curX == x && curY == y) { continue; }
                yield return this[curX, curY];
            }
        }
    }
}
