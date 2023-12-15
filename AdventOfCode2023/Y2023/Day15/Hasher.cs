namespace AdventOfCode.Utils.Y2023.Day15;

class Hasher(string input) {
    readonly string Input = input;

    public byte Hash => Input.Aggregate((byte)0, (acc, c) => (byte)((acc + c) * 17));
}
