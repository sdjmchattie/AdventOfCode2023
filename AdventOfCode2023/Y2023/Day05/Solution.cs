using System.Collections.Immutable;
using System.Data;

namespace AdventOfCode.Y2023;

class Day05 {
    private string[]? fileContents;
    private IImmutableList<long>? seeds;
    private IImmutableDictionary<string, IEnumerable<Mapping>>? mappings;

    private string[] FileContents()
    {
        fileContents ??= File.ReadAllLines("Y2023/Day05/input.txt");

        return fileContents;
    }

    private IImmutableList<long> Seeds()
    {
        if (seeds == null) {
            seeds = FileContents()[0]
                .Split(" ")[1..]
                .Select(long.Parse)
                .ToImmutableList();
        }

        return seeds;
    }

    private IImmutableDictionary<string, IEnumerable<Mapping>> Mappings()
    {
        if (mappings == null) {
            var fileContents = FileContents();
            var tempDict = new Dictionary<string, IEnumerable<Mapping>>();
            var currentMapName = "";
            var currentMappings = new List<Mapping>();
            for (int i = 2; i <= fileContents.Length; i++) {
                if (i == fileContents.Length) {
                    tempDict.Add(currentMapName, currentMappings.ToImmutableList());
                    break;
                }
                var line = fileContents[i];
                if (line == string.Empty) {
                    tempDict.Add(currentMapName, currentMappings.ToImmutableList());
                    currentMappings.Clear();
                } else if (line.EndsWith("map:")) {
                    currentMapName = line.Split(" ").First();
                } else {
                    var values = line.Split(" ").Select(long.Parse).ToArray();
                    currentMappings.Add(new Mapping(values[0], values[1], values[2]));
                }
            }

            mappings = tempDict.ToImmutableDictionary();
        }

        return mappings;
    }

    public object Part1()
    {
        var almanac = new Almanac(Seeds(), Mappings());
        var seedsToDestinations = almanac.DetermineLocations();
        return seedsToDestinations.MinBy(std => std.location).location;
    }

    public object Part2()
    {
        // var input = ParsedInput();
        return "Part 2 Solution";
    }
}
