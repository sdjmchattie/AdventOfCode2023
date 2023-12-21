
namespace AdventOfCode.Utils.Y2023.Day20;

public class ButtonModule() : Module("button")
{
    public override IEnumerable<(Module, Pulse)> Receive(Pulse pulse, Module from)
    {
        throw new InvalidOperationException("The button module is not intended to receive pulses.");
    }
}
