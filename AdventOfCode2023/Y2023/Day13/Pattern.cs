namespace AdventOfCode.Utils.Y2023.Day13;

class Pattern(string[] input) : Grid2D(input) {
    private IEnumerable<int> MirrorColumns(int variations)
    {
        for (int index = 1; index < Width; index++) {
            var i = index - 1;
            var j = index;
            var maxOffset = Math.Min(i, Width - 1 - j);
            var variationCountdown = variations;

            for (int offset = 0; offset <= maxOffset; offset++) {
                var leftValues = this[PointsAlong(i - offset, Axis.Vertical)];
                var rightValues = this[PointsAlong(j + offset, Axis.Vertical)];
                variationCountdown -= leftValues.Zip(rightValues)
                    .Count(chars => chars.First != chars.Second);
            }

            if (variationCountdown == 0) { yield return index; }
        }
    }

    private IEnumerable<int> MirrorRows(int variations)
    {
        for (int index = 1; index < Height; index++) {
            var i = index - 1;
            var j = index;
            var maxOffset = Math.Min(i, Height - 1 - j);
            var variationCountdown = variations;

            for (int offset = 0; offset <= maxOffset; offset++) {
                var upperValues = this[PointsAlong(i - offset, Axis.Horizontal)];
                var lowerValues = this[PointsAlong(j + offset, Axis.Horizontal)];
                variationCountdown -= upperValues.Zip(lowerValues)
                    .Count(chars => chars.First != chars.Second);
            }

            if (variationCountdown == 0) { yield return index; }
        }
    }

    public IEnumerable<(int index, Axis axis)> FindMirrors(int variations)
    {
        var rows = MirrorRows(variations).Select(mirror => (mirror, Axis.Horizontal));
        var cols = MirrorColumns(variations).Select(mirror => (mirror, Axis.Vertical));

        return rows.Concat(cols);
    }
}
