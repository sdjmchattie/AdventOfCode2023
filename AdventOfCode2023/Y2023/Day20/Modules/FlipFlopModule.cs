namespace AdventOfCode.Utils.Y2023.Day20;

public class FlipFlopModule(string name) : Module(name)
{
    private bool State = false;

    public override IEnumerable<(Module, Pulse)> Receive(Pulse pulse, Module from)
    {
        if (pulse == Pulse.High) {
            return [];
        }

        State = !State;

        return Outputs.Select<Module, (Module, Pulse)>(m => (m, State ? Pulse.High : Pulse.Low));
    }

    public override string ToString()
    {
        var stateName = State ? "on" : "off";
        return $"{Name} -> Is {stateName}.";
    }
}
