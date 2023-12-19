using AdventOfCode.Utils.Y2023.Day19;

namespace AdventOfCode.Y2023;

class Day19 {
    private string[]? inputContents;
    private string[] InputContents =>
        inputContents ??= File.ReadAllLines($"Y2023/{GetType().Name}/input.txt");

    private List<Part>? parts;
    private List<Part> Parts
    {
        get {
            parts ??= InputContents
                .SkipWhile(line => !line.StartsWith('{'))
                .Select(line => {
                    var variables = line[1..^1].Split(',');
                    var x = int.Parse(variables.First(v => v[0] == 'x')[2..]);
                    var m = int.Parse(variables.First(v => v[0] == 'm')[2..]);
                    var a = int.Parse(variables.First(v => v[0] == 'a')[2..]);
                    var s = int.Parse(variables.First(v => v[0] == 's')[2..]);

                    return new Part(x, m, a, s);
                })
                .ToList();

            return parts;
        }
    }

    private List<Workflow>? workflows;
    private List<Workflow> Workflows
    {
        get {
            workflows ??= InputContents
                .TakeWhile(line => line != "")
                .Select(line => {
                    var parts = line.Split('{');
                    var name = parts[0];
                    var stepDefinitions = parts[1][..^1].Split(',');
                    var steps = stepDefinitions.Select(definition => {
                        if (definition.Contains(':')) {
                            var colonParts = definition.Split(':');
                            var destStep = colonParts[1];
                            var varName = char.ToUpper(colonParts[0][0]);
                            var op = colonParts[0][1] == '<' ? Operator.LessThan : Operator.MoreThan;
                            var value = int.Parse(colonParts[0][2..]);

                            return new Step(varName, op, value, destStep);
                        } else {
                            return new Step(' ', Operator.Always, 0, definition);
                        }
                    })
                    .ToList();

                    return new Workflow(name, steps);
                })
                .ToList();

            return workflows;
        }
    }


    public object Part1()
    {
        var evaluator = new Evaluator(Workflows, Parts);
        return evaluator.Score;
    }

    public object Part2()
    {
        var input = InputContents;
        return "Part 2 Solution";
    }
}
