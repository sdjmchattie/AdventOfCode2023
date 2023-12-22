using System.Text.RegularExpressions;

namespace AdventOfCode.Y2023;

struct CubeSet {
    public int red;
    public int green;
    public int blue;
}

struct Game {
    public int id;
    public IList<CubeSet> cubeSets;
}

class Day02 {
    private IList<Game>? parsedInput;
    Regex lineRegex = new Regex(@"^Game (\d+):(.+)$");
    Regex redRegex = new Regex(@"(\d+) red");
    Regex greenRegex = new Regex(@"(\d+) green");
    Regex blueRegex = new Regex(@"(\d+) blue");

    private CubeSet ParseCubeSet(string desc) {
        var red = 0;
        var green = 0;
        var blue = 0;

        var redResult = redRegex.Match(desc);
        if (redResult.Success) {
            red = int.Parse(redResult.Groups[1].ToString());
        }

        var greenResult = greenRegex.Match(desc);
        if (greenResult.Success) {
            green = int.Parse(greenResult.Groups[1].ToString());
        }

        var blueResult = blueRegex.Match(desc);
        if (blueResult.Success) {
            blue = int.Parse(blueResult.Groups[1].ToString());
        }

        return new CubeSet() { red = red, green = green, blue = blue };
    }

    private Game ParseGame(string line) {
        var lineResult = lineRegex.Match(line);
        var gameId = int.Parse(lineResult.Groups[1].ToString());
        var cubeSetDescs = lineResult.Groups[2].ToString().Split(";");
        var cubeSets = cubeSetDescs.Select(ParseCubeSet).ToList();

        return new Game() { id = gameId, cubeSets = cubeSets };
    }

    private IList<Game> ParsedInput() {
        if (parsedInput != null) {
            return parsedInput!;
        }

        var inputLines = File.ReadAllLines($"Y2023/{GetType().Name}/input.txt");
        parsedInput = inputLines.Select(ParseGame).ToList();

        return parsedInput;
    }

    public object Part1()
    {
        var games = ParsedInput();

        var validGamesIdSum = games
            .Where(game => game.cubeSets
                .All(cs => cs.red <= 12 && cs.green <= 13 && cs.blue <= 14)
            )
            .Sum(game => game.id);

        return validGamesIdSum;
    }

    public object Part2()
    {
        var games = ParsedInput();

        var sumOfPowerOfMinimumCubesPerGame = games
            .Select(game => {
                var red = game.cubeSets.Max(cs => cs.red);
                var green = game.cubeSets.Max(cs => cs.green);
                var blue = game.cubeSets.Max(cs => cs.blue);
                return red * green * blue;
            })
            .Sum();

        return sumOfPowerOfMinimumCubesPerGame;
    }
}
