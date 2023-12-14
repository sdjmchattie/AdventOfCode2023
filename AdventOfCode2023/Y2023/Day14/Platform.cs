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

    public void Tip(CompassDirection direction) {
        var axis = direction == CompassDirection.North ||
            direction == CompassDirection.South ?
            Axis.Vertical : Axis.Horizontal;
        var flip = direction == CompassDirection.South ||
            direction == CompassDirection.East;
        var maxDimension = axis == Axis.Vertical ? Width : Height;

        for (int i = 0; i < maxDimension; i++) {
            var points = PointsAlong(i, axis);

            if (flip) {
                points = points.Reverse();
            }

            var newStrip = Condense(this[points]).ToList();

            for (int j = 0; j < newStrip.Count; j++) {
                var k = j;
                if (flip) {
                    k = maxDimension - j - 1;
                }

                if (axis == Axis.Horizontal) {
                    this[k, i] = newStrip[j];
                } else {
                    this[i, k] = newStrip[j];
                }
            }
        }
    }

    public int GetNorthLoad()
    {
        var load = 0;
        for (int x = 0; x < Width; x++) {
            var strip = this[PointsAlong(x, Axis.Vertical)];
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
