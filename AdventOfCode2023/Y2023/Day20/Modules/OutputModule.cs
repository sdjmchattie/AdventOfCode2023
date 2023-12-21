
namespace AdventOfCode.Utils.Y2023.Day20;

public class OutputModule(string name) : Module(name)
{
    public override IEnumerable<(Module, Pulse)> Receive(Pulse pulse, Module from)
    {
        // Do nothing with outputs.
        return [];
    }
}
