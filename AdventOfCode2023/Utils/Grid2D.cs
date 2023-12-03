namespace AdventOfCode.Utils;

class Grid2D {
    private IList<IList<char>> grid;

    public Grid2D(string[] input) {
        grid = new List<IList<char>>() { new List<char>() };
    }
}
