namespace AdventOfCode.Utils.Y2023.Day11;

class Pattern(string[] input) : Grid2D(input) {
    public int? MirrorColumn
    {
        get {
            for (int index = 1; index < Width; index++) {
                var i = index - 1;
                var j = index;
                var maxOffset = Math.Min(i, Width - 1 - j);
                var mirrored = true;

                for (int offset = 0; offset <= maxOffset; offset++) {
                    var leftValues = this[PointsAlong(i - offset, Axis.Vertical)];
                    var rightValues = this[PointsAlong(j + offset, Axis.Vertical)];
                    if (!leftValues.SequenceEqual(rightValues)) {
                        mirrored = false;
                    }
                }

                if (mirrored) { return index; }
            }

            return null;
        }
    }

    public int? MirrorRow
    {
        get {
            for (int index = 1; index < Height; index++) {
                var i = index - 1;
                var j = index;
                var maxOffset = Math.Min(i, Height - 1 - j);
                var mirrored = true;

                for (int offset = 0; offset <= maxOffset; offset++) {
                    var upperValues = this[PointsAlong(i - offset, Axis.Horizontal)];
                    var lowerValues = this[PointsAlong(j + offset, Axis.Horizontal)];
                    if (!upperValues.SequenceEqual(lowerValues)) {
                        mirrored = false;
                    }
                }

                if (mirrored) { return index; }
            }

            return null;
        }
    }
}
