using AdventOfCode.Utils.Y2023.Day08;

namespace AdventOfCode.Utils.Y2023.Day14;

class Platform : Grid2D {
    public Platform(string[] input) : base(input) {  }
    public Platform(Platform other) : base(other) {  }

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

    public void Tilt(CompassDirection direction) {
        var maxDimension = PointsAlong(0, direction.Opposite()).Count();

        for (int i = 0; i < maxDimension; i++) {
            var points = PointsAlong(i, direction.Opposite()).ToList();
            var newStrip = Condense(this[points]).ToList();

            for (int j = 0; j < newStrip.Count; j++) {
                this[points[j]] = newStrip[j];
            }
        }
    }

    public int GetNorthLoad()
    {
        var load = 0;
        for (int x = 0; x < Width; x++) {
            var strip = this[PointsAlong(x, CompassDirection.South)];
            var loadValue = strip.Count();

            foreach (char c in strip) {
                if (c == 'O') {
                    load += loadValue;
                }
                loadValue--;
            }
        }

        return load;
    }
}
