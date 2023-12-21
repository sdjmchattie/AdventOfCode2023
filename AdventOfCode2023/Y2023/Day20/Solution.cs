using AdventOfCode.Utils.Y2023.Day20;

namespace AdventOfCode.Y2023;

class Day20 {
    private string[]? inputContents;
    private string[] InputContents =>
        inputContents ??= File.ReadAllLines($"Y2023/{GetType().Name}/input.txt");

    private HashSet<Module>? modules;
    private HashSet<Module> Modules
    {
        get {
            if (modules == null) {
                // Create modules.
                modules = InputContents.Select(line => {
                    var inOut = line.Split(" -> ");
                    var name = inOut[0];

                    return (Module)((name[0], name[1..]) switch {
                        ('%', var actualName) => new FlipFlopModule(actualName),
                        ('&', var actualName) => new ConjunctionModule(actualName),
                        _ => new BroadcastModule(name),
                    });
                }).ToHashSet();

                // Define outputs.
                foreach (string line in InputContents) {
                    var inOut = line.Split(" -> ");
                    var name = inOut[0];
                    var outs = inOut[1];
                    var outNames = outs.Split(", ");

                    // Add any modules that are just for output
                    foreach (string outputName in outNames.Where(outName => !modules.Any(m => m.Name == outName))) {
                        modules.Add(new OutputModule(outputName));
                    }

                    if (name.StartsWith('%') || name.StartsWith('&')) {
                        name = name[1..];
                    }

                    var module = modules.First(m => m.Name == name);
                    foreach (Module output in modules.Where(m => outNames.Contains(m.Name))) {
                        module.AddOutput(output);
                    }
                };

                // Define ConjunctionModule inputs.
                foreach (ConjunctionModule module in modules.Where(m => m is ConjunctionModule).Cast<ConjunctionModule>()) {
                    var sourceModules = modules.Where(m => m.Outputs.Contains(module));
                    foreach (Module sourceModule in sourceModules) {
                        module.AddInput(sourceModule);
                    }
                }
            }

            return modules;
        }
    }

    public object Part1()
    {
        var machineInitializer = new MachineInitializer(Modules);
        machineInitializer.PushButton(1000);
        return machineInitializer.LowPulseCount * machineInitializer.HighPulseCount;
    }

    public object Part2()
    {
        var input = InputContents;
        return "Part 2 Solution";
    }
}
