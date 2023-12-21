using System.Collections.Immutable;
using Microsoft.VisualBasic;

namespace AdventOfCode.Utils.Y2023.Day20;

public class MachineInitializer(HashSet<Module> modules)
{
    public long LowPulseCount { get; private set; } = 0L;
    public long HighPulseCount { get; private set; } = 0L;
    private ImmutableHashSet<Module> Modules = [.. modules];
    private readonly ButtonModule ButtonModule = new();
    private BroadcastModule BroadcastModule = (BroadcastModule)modules.First(m => m.Name == "broadcaster");

    public void PushButton(int times = 1, bool verbose = false)
    {
        for (int i = 0; i < times; i++) {
            var unresolvedPulses = new Queue<(Module from, Module to, Pulse pulse)>();
            unresolvedPulses.Enqueue((ButtonModule, BroadcastModule, Pulse.Low));

            while (unresolvedPulses.Count > 0) {
                var (from, to, pulse) = unresolvedPulses.Dequeue();

                if (verbose) { Console.WriteLine($"{from.Name} -{pulse}-> {to.Name}"); }

                if (pulse == Pulse.Low) {
                    LowPulseCount++;
                } else {
                    HighPulseCount++;
                }

                var newPulses = to.Receive(pulse, from);
                foreach ((Module newModule, Pulse newPulse) in newPulses) {
                    unresolvedPulses.Enqueue((to, newModule, newPulse));
                }
            }

            if (verbose) { Console.WriteLine(); }
        }
    }
}
