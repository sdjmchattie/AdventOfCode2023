using AdventOfCode.Utils;
using AdventOfCode.Utils.Y2023.Day14;

namespace AdventOfCode.Y2023;

class Day14 {
    private string[]? inputContents;
    private string[] InputContents =>
        inputContents ??= File.ReadAllLines($"Y2023/{GetType().Name}/input.txt");

    public object Part1()
    {
        var platform = new Platform(InputContents);
        platform.Tilt(CompassDirection.North);

        return platform.GetNorthLoad();
    }

    public object Part2()
    {
        var platformList = new List<Platform>();
        var platform = new Platform(InputContents);

        while(true) {
            platform.Tilt(CompassDirection.North);
            platform.Tilt(CompassDirection.West);
            platform.Tilt(CompassDirection.South);
            platform.Tilt(CompassDirection.East);

            foreach (Platform p in platformList) {
                if (platform.Equals(p)) {
                    var loopStart = platformList.FindIndex(p => p.Equals(platform));
                    var loopLength = platformList.Count - loopStart;
                    Console.WriteLine(loopStart);
                    var neededPlatform = platformList[loopStart - 1 + (1000000000 - loopStart) % loopLength];
                    return neededPlatform.GetNorthLoad();
                }
            }

            platformList.Add(new Platform(platform));
        }
    }
}
