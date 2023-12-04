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
        for (int y = 0; y < this.Width; y++) {
            bool prevIsDigit = false;
            string curNumber = "";
            int curMinX = 0;

            for (int x = 0; x < this.Width; x++) {
                var curChar = this[x, y];
                if (!IsDigit(curChar) || x == this.Width - 1) {
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

    public IEnumerable<int> PartNumbers
    {
        get
        {
            var partNumbers = this.GetSchematicNumbers().Where(schematicNumber => {
                var allNeighbours = new List<char>();
                for (int x = schematicNumber.MinX; x <= schematicNumber.MaxX; x++) {
                    allNeighbours.AddRange(this.Neighbours(x, schematicNumber.Y));
                }

                return allNeighbours.Any(neighbour => !IsDigit(neighbour) && neighbour != '.');
            });

            return partNumbers.Select(partNumbers => partNumbers.Number);
        }
    }
}
