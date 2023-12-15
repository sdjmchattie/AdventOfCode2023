namespace AdventOfCode.Utils.Y2023.Day15;

record struct Lens(string Label, byte FocalLength);

class LensArray {
    private readonly Dictionary<byte, List<Lens>> Boxes = [];

    public LensArray() {
        for (int i = 0; i <= 256; i++) {
            Boxes[(byte)i] = [];
        }
    }

    public static byte Hash(string input) =>
        input.Aggregate((byte)0, (acc, c) => (byte)((acc + c) * 17));

    public void FollowSteps(string[] steps)
    {
        foreach (string step in steps) {
            if (step.EndsWith('-')) {
                var label = step[..^1];
                var box = Boxes[Hash(label)];
                var lens = box.FirstOrDefault(lens => lens.Label == label);
                box.Remove(lens);
            } else {
                var stepParts = step.Split('=');
                var label = stepParts[0];
                var focalLength = byte.Parse(stepParts[1]);
                var box = Boxes[Hash(label)];
                var lens = new Lens(label, focalLength);
                var existingIndex = box.FindIndex(lens => lens.Label == label);
                if (existingIndex >= 0) {
                    box.RemoveAt(existingIndex);
                    box.Insert(existingIndex, lens);
                } else {
                    box.Add(lens);
                }
            }
        }
    }

    public int FocussingPower
    {
        get {
            var power = 0;
            foreach (KeyValuePair<byte, List<Lens>> box in Boxes) {
                var boxPower = 0;
                var i = 1;
                foreach (Lens lens in box.Value) {
                    boxPower += (box.Key + 1) * i * lens.FocalLength;
                    i++;
                }

                power += boxPower;
            }

            return power;
        }
    }
}
