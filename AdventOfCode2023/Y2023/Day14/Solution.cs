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
        platform.Tip(CompassDirection.North);

        return platform.GetNorthLoad();
    }

    public object Part2()
    {
        var platformList = new List<Platform>();
        var platform = new Platform(InputContents);
        var loopStart = 0;
        var i = 0;

        while(true) {
            platform.Tip(CompassDirection.North);
            platform.Tip(CompassDirection.West);
            platform.Tip(CompassDirection.South);
            platform.Tip(CompassDirection.East);

            foreach (Platform p in platformList) {
                if (platform.Equals(p)) {
                    if (loopStart == 0) {
                        loopStart = i;
                    } else {
                        var loopLength = i - loopStart;
                        var neededPlatform = platformList[(1000000000 - loopStart - 1) % loopLength];
                        return neededPlatform.GetNorthLoad();
                    }

                    platformList.Clear();
                    break;
                }
            }

            i++;
            platformList.Add(new Platform(platform));
        }
    }
}
