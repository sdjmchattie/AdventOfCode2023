using AdventOfCode.Utils;

readonly struct SchematicNumber
{
    public readonly int Number { get; init; }
    public readonly int MinX { get; init; }
    public readonly int MaxX { get; init; }
    public readonly int Y { get; init; }
}

class EngineSchematic(string[] input) : Grid2D(input) {
    private static bool IsDigit(char c) => c >= 48 && c <= 57;

    private IEnumerable<SchematicNumber> GetSchematicNumbers()
    {
        for (int y = 0; y < Height; y++) {
            bool prevIsDigit = false;
            string curNumber = "";
            int curMinX = 0;

            for (int x = 0; x < Width; x++) {
                var curChar = this[x, y];
                if (!IsDigit(curChar) || x == Width - 1) {
                    if (IsDigit(curChar)) { curNumber += curChar; }
                    if (prevIsDigit) {
                        yield return new SchematicNumber {
                            Number = int.Parse(curNumber),
                            MinX = curMinX,
                            MaxX = IsDigit(curChar) ? x : x - 1,
                            Y = y
                        };
                    }
                    prevIsDigit = false;
                } else {
                    if (!prevIsDigit) {
                        curNumber = "";
                        curMinX = x;
                    }

                    curNumber += curChar;
                    prevIsDigit = true;
                }
            }
        }
    }

    private IEnumerable<SchematicNumber> Parts
    {
        get {
            return GetSchematicNumbers().Where(schematicNumber => {
                var allNeighbours = new List<char>();
                for (int x = schematicNumber.MinX; x <= schematicNumber.MaxX; x++) {
                    allNeighbours.AddRange(Neighbours(new Point2D(x, schematicNumber.Y)));
                }

                return allNeighbours.Any(neighbour => !IsDigit(neighbour) && neighbour != '.');
            });
        }
    }

    public IEnumerable<int> PartNumbers
    {
        get { return Parts.Select(partNumbers => partNumbers.Number); }
    }

    public IEnumerable<int> GearRatios
    {
        get {
            for (int y = 0; y < Height; y++) {
                for (int x = 0; x < Width; x++) {
                    if (this[x, y] == '*') {
                        var adjacentParts = Parts.Where(part => {
                            var aboveOrBelow = part.Y == y - 1 || part.Y == y + 1;
                            var sameLine = part.Y == y;
                            return (sameLine && (part.MinX == x + 1 || part.MaxX == x - 1))
                                || (aboveOrBelow && part.MinX <= x + 1 && part.MaxX >= x - 1);
                        });
                        if (adjacentParts.Count() == 2) {
                            yield return adjacentParts.First().Number * adjacentParts.Last().Number;
                        }
                    }
                }
            }
        }
    }
}
