using AdventOfCode.Utils.Y2023.Day15;

namespace AdventOfCode.Y2023;

class Day15 {
    private string[]? inputContents;
    private string[] InputContents =>
        inputContents ??= File.ReadAllLines($"Y2023/{GetType().Name}/input.txt");
    private string[] Steps =>
        InputContents[0].Split(',');

    public object Part1()
    {
        var input = InputContents;
        return Steps.Aggregate(0, (acc, step) => acc + LensArray.Hash(step));
    }

    public object Part2()
    {
        var lensArray = new LensArray();
        lensArray.FollowSteps(Steps);
        return lensArray.FocussingPower;
    }
}
