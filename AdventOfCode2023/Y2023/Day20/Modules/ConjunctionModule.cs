using Microsoft.VisualBasic;

namespace AdventOfCode.Utils.Y2023.Day20;

public class ConjunctionModule(string name) : Module(name)
{
    private readonly Dictionary<Module, Pulse> Inputs = [];

    public void AddInput(Module module)
    {
        Inputs[module] = Pulse.Low;
    }

    public override IEnumerable<(Module, Pulse)> Receive(Pulse pulse, Module from)
    {
        Inputs[from] = pulse;
        var allInputsHigh = Inputs.Values.All(p => p == Pulse.High);

        return Outputs.Select<Module, (Module, Pulse)>(m =>
            (m, allInputsHigh ? Pulse.Low : Pulse.High));
    }

    public override string ToString()
    {
        var inputPulses = string.Join(", ", Inputs.Select(kv => $"{kv.Key.Name}: {kv.Value}"));
        return $"{Name} -> {inputPulses}";
    }
}
