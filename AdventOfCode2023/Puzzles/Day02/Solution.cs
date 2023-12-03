namespace AdventOfCode.Y2023;

class Day02 {
    private IDictionary<int, IEnumerable<IDictionary<string, int>>>? parsedInput;

    private IDictionary<int, IEnumerable<IDictionary<string, int>>> ParsedInput() {
        if (parsedInput != null) {
            return parsedInput!;
        }

        var inputLines = File.ReadAllLines("Puzzles/Day02/input.txt");
        parsedInput = new Dictionary<int, IEnumerable<IDictionary<string, int>>>();

        return parsedInput;
    }

    public object Part1()
    {
        var games = ParsedInput();
        return "Part 1 Solution";
    }

    public object Part2()
    {
        var games = ParsedInput();
        return "Part 2 Solution";
    }
}
