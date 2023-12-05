using System.Collections.Immutable;

namespace AdventOfCode.Y2023;

readonly struct SeedLocationPair(long seed, long location)
{
    public readonly long seed = seed;
    public readonly long location = location;
}

class Almanac(
    IImmutableList<long> seeds,
    IImmutableDictionary<string, IEnumerable<Mapping>> allMappings
)
{
    public readonly IImmutableList<long> seeds = seeds;
    private readonly IImmutableDictionary<string, IEnumerable<Mapping>> allMappings = allMappings;

    public IEnumerable<SeedLocationPair> DetermineLocations()
    {
        return seeds.Select(seed => {
            var sourceName = "seed";
            var sourceValue = seed;
            while (sourceName != "location") {
                var mapName = allMappings.Keys.First(name => name.StartsWith(sourceName));
                var mappings = allMappings[mapName];
                var mapping = mappings.FirstOrDefault(m => m.ContainsSource(sourceValue));
                if (mapping != null) {
                    sourceValue = mapping.MapToDestination(sourceValue);
                }
                sourceName = mapName.Split("-").Last();
            }
            return new SeedLocationPair(seed, sourceValue);
        });
    }
}
