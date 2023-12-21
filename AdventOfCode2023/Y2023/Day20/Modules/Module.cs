using System.Collections.Immutable;

namespace AdventOfCode.Utils.Y2023.Day20;

public abstract class Module(string name)
{
    public readonly string Name = name;
    public ImmutableList<Module> Outputs { get; private set; } = [];

    public void AddOutput(Module module)
    {
        Outputs = Outputs.Add(module);
    }

    public abstract IEnumerable<(Module, Pulse)> Receive(Pulse pulse, Module from);
}
