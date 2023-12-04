namespace AdventOfCode.Y2023;

class Day03 {
    private EngineSchematic? parsedInput;

    private EngineSchematic ParsedInput() {
        if (parsedInput != null) {
            return parsedInput!;
        }

        var inputFile = File.ReadAllLines("Y2023/Day03/input.txt");
        parsedInput = new EngineSchematic(inputFile);

        return parsedInput;
    }

    public object Part1()
    {
        var schematic = ParsedInput();
        return schematic.PartNumbers.Sum();
    }

    public object Part2()
    {
        var schematic = ParsedInput();
        return "Part 2 Solution";
    }
}
