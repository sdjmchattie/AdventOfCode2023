namespace AdventOfCode.Utils.Y2023.Day20;

public class BroadcastModule(string name) : Module(name)
{
    public override IEnumerable<(Module, Pulse)> Receive(Pulse pulse, Module from)
    {
        return Receive(pulse);
    }

    public IEnumerable<(Module, Pulse)> Receive(Pulse pulse) =>
        Outputs.Select<Module, (Module, Pulse)>(m => (m, pulse));

    public override string ToString()
    {
        return $"{Name} -> No state.";
    }
}
