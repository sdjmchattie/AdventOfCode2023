using System.Collections.Frozen;

namespace AdventOfCode.Utils.Y2023.Day20;

public class ConjunctionModule(string name) : Module(name)
{
    private readonly Dictionary<Module, Pulse> _inputs = [];
    public FrozenDictionary<Module, Pulse> Inputs => _inputs.ToFrozenDictionary();

    public void AddInput(Module module)
    {
        _inputs[module] = Pulse.Low;
    }

    public override IEnumerable<(Module, Pulse)> Receive(Pulse pulse, Module from)
    {
        _inputs[from] = pulse;
        var allInputsHigh = _inputs.Values.All(p => p == Pulse.High);

        return Outputs.Select<Module, (Module, Pulse)>(m =>
            (m, allInputsHigh ? Pulse.Low : Pulse.High));
    }

    public override string ToString()
    {
        var inputPulses = string.Join(", ", Inputs.Select(kv => $"{kv.Key.Name}: {kv.Value}"));
        return $"{Name} -> {inputPulses}";
    }
}
