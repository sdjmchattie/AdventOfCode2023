namespace AdventOfCode.Utils.Y2023.Day09;

class Extrapolation(IEnumerable<int> sequence)
{
    public readonly IEnumerable<int> Sequence = sequence;

    public int Extrapolate(bool left) {
        var sequenceList = Sequence.ToList();
        var adjacentValues = sequenceList[..^1].Zip(sequenceList[1..]);
        var diffs = adjacentValues.Select(vals => vals.Second - vals.First);
        var newDiff = 0;

        if (!diffs.All(d => d == 0)) {
            var nextExtrapolation = new Extrapolation(diffs);
            newDiff = nextExtrapolation.Extrapolate(left);
        }

        return left ? sequenceList[0] - newDiff : sequenceList[^1] + newDiff;
    }
}
