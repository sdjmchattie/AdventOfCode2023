using System.Collections.Immutable;

namespace AdventOfCode.Utils.Y2023.Day20;

public class MachineInitializer(HashSet<Module> modules)
{
    public long LowPulseCount { get; private set; } = 0L;
    public long HighPulseCount { get; private set; } = 0L;
    private ImmutableHashSet<Module> Modules = [.. modules];
    private readonly ButtonModule ButtonModule = new();
    private BroadcastModule BroadcastModule = (BroadcastModule)modules.First(m => m.Name == "broadcaster");
    private Dictionary<string, Dictionary<Pulse, int>> PulseCounts = [];

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

                var currentPulseCounts = PulseCounts.GetValueOrDefault(
                    to.Name,
                    new() {{Pulse.Low, 0}, {Pulse.High, 0}}
                );
                currentPulseCounts[pulse] += 1;
                PulseCounts[to.Name] = currentPulseCounts;

                var newPulses = to.Receive(pulse, from);
                foreach ((Module newModule, Pulse newPulse) in newPulses) {
                    unresolvedPulses.Enqueue((to, newModule, newPulse));
                }
            }

            if (verbose) { Console.WriteLine(); }
        }
    }

    public List<int> FindPulses(string moduleName, Pulse pulseType, int repeats, bool verbose = false)
    {
        var buttonCounts = Enumerable.Repeat(0, repeats).ToList();
        for (int i = 0; i < repeats; i++) {
            while (PulseCounts.GetValueOrDefault(
                moduleName,
                new() {{Pulse.Low, 0}, {Pulse.High, 0}}
            )[pulseType] == i) {
                PushButton(1, verbose);
                buttonCounts[i]++;
            }
        }

        return buttonCounts;
    }
}
