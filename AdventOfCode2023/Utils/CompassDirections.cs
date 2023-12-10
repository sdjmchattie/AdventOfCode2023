namespace AdventOfCode.Utils;

enum CompassDirection {
    North, NorthEast, East, SouthEast, South, SouthWest, West, NorthWest
}

static class CompassDirectionExtensions
{
    public static CompassDirection Opposite(this CompassDirection direction)
    {
        var opposites = new Dictionary<CompassDirection, CompassDirection>()
            {
                { CompassDirection.North, CompassDirection.South },
                { CompassDirection.NorthEast, CompassDirection.SouthWest },
                { CompassDirection.East, CompassDirection.West },
                { CompassDirection.SouthEast, CompassDirection.NorthWest },
                { CompassDirection.South, CompassDirection.North },
                { CompassDirection.SouthWest, CompassDirection.NorthEast },
                { CompassDirection.West, CompassDirection.East },
                { CompassDirection.NorthWest, CompassDirection.SouthEast },
            };

        return opposites[direction];
    }
}
