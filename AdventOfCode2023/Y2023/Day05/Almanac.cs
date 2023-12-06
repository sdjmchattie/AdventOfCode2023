using System.Collections.Immutable;

namespace AdventOfCode.Utils.Y2023.Day05;

class Almanac(
    IImmutableList<Resource> seeds,
    IImmutableDictionary<string, IEnumerable<Mapping>> allMappings
)
{
    public readonly IImmutableList<Resource> seeds = seeds;
    private readonly IImmutableDictionary<string, IEnumerable<Mapping>> allMappings = allMappings;

    private List<Resource> NewResources(Resource resource, IEnumerable<Mapping> mappings)
    {
        var coverage = mappings
            .Where(mapping => mapping.CoversResource(resource))
            .OrderBy(mapping => mapping.sourceStart)
            .ToList();

        if (coverage.Count == 0) {
            return [resource];
        }

        var newResources = new List<Resource>();
        var curEnd = resource.idStart - 1;

        newResources.AddRange(coverage.SelectMany(mapping => {
            var resources = new List<Resource>();
            if (mapping.sourceStart > curEnd + 1) {
                resources.Add(Resource.ByEndpoints(curEnd + 1, mapping.sourceStart - 1));
            }

            var rangeStart = mapping.MapToDestination(Math.Max(resource.idStart, mapping.sourceStart));
            var rangeEnd = mapping.MapToDestination(Math.Min(resource.idEnd, mapping.sourceEnd));
            resources.Add(Resource.ByEndpoints(rangeStart, rangeEnd));

            curEnd = mapping.sourceEnd;

            return resources;
        }));

        if (curEnd < resource.idEnd) {
            newResources.Add(Resource.ByEndpoints(curEnd + 1, resource.idEnd));
        }

        return newResources;
    }

    public IEnumerable<Resource> DetermineLocations()
    {
        var resources = seeds.ToList();
        var sourceName = "seed";

        while (sourceName != "location") {
            var mapName = allMappings.Keys
                .First(name => name.StartsWith(sourceName));
            var mappings = allMappings[mapName];
            resources = resources
                .SelectMany(res => NewResources(res, mappings))
                .ToList();
            sourceName = mapName.Split("-").Last();
        }

        return resources;
    }
}
