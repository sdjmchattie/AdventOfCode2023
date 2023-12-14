namespace AdventOfCode.Utils.Y2023.Day14;

class Platform(string[] input) : Grid2D(input) {
    private static IEnumerable<char> Condense(IEnumerable<char> strip)
    {
        var lastBlock = -1;

        while (lastBlock < strip.Count()) {
            var section = strip.Skip(lastBlock + 1).TakeWhile(e => e != '#');

            var boulders = section.Count(e => e == 'O');
            for (int i = 0; i < boulders; i++) {
                yield return 'O';
            }

            var spaces = section.Count(e => e == '.');
            for (int i = 0; i < spaces; i++) {
                yield return '.';
            }

            lastBlock += section.Count() + 1;
            if (lastBlock < strip.Count()) {
                yield return '#';
            }
        }
    }

    private static int Load(IEnumerable<char> strip)
    {
        var load = 0;
        var loadValue = strip.Count();
        foreach (char c in strip) {
            if (c == 'O') {
                load += loadValue;
            }
            loadValue--;
        }

        return load;
    }

    public int LoadWhenTippedNorth()
    {
        var load = 0;
        for (int x = 0; x < Width; x++) {
            load += Load(Condense(this[PointsAlong(x, Axis.Vertical)]));
        }

        return load;
    }
}
