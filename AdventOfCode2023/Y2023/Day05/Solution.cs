using System.Collections.Immutable;
using System.Data;
using AdventOfCode.Utils.Y2023.Day05;

namespace AdventOfCode.Y2023;

class Day05 {
    private string[]? fileContents;
    private IImmutableDictionary<string, IEnumerable<Mapping>>? mappings;

    private string[] FileContents()
    {
        fileContents ??= File.ReadAllLines($"Y2023/{GetType().Name}/input.txt");

        return fileContents;
    }

    private ImmutableList<Resource> LiteralSeeds()
    {
        return FileContents()[0]
            .Split(" ")[1..]
            .Select(num => Resource.ByEndpoints(long.Parse(num), long.Parse(num)))
            .ToImmutableList();
    }

    private ImmutableList<Resource> RangedSeeds()
    {
        var seedValues = FileContents()[0]
            .Split(" ")[1..]
            .Select(long.Parse)
            .ToArray();

        var seeds = new List<Resource>();
        for (int i = 0; i < seedValues.Length; i += 2) {
            seeds.Add(Resource.ByRangeLength(seedValues[i], seedValues[i+1]));
        }

        return seeds.ToImmutableList();
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
        var almanac = new Almanac(LiteralSeeds(), Mappings());
        var seedsToDestinations = almanac.DetermineLocations();
        return seedsToDestinations.Select(res => res.idStart).Min();
    }

    public object Part2()
    {
        var almanac = new Almanac(RangedSeeds(), Mappings());
        var seedsToDestinations = almanac.DetermineLocations();
        return seedsToDestinations.Select(res => res.idStart).Min();
    }
}
