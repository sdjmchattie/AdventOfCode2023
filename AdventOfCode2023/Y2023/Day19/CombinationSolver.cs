namespace AdventOfCode.Utils.Y2023.Day19;

class CombinationSolver(ICollection<Workflow> workflows)
{
    private readonly ICollection<Workflow> Workflows = workflows;
    private record struct RangedPart(string WorkflowName, IDictionary<char, Range> Ranges)
    {
        public readonly RangedPart Copy()
        {
            return new RangedPart(WorkflowName, Ranges.ToDictionary());
        }

        public readonly void OutputPart()
        {
            Console.WriteLine($"Next workflow: {WorkflowName}; Ranges: {string.Join(", ", Ranges.Select(kv => $"{kv.Key} => {kv.Value}"))}");
        }
    };
    private List<RangedPart> QueuedParts = [];
    private List<RangedPart> AcceptedParts = [];

    public long CombinationsCount
    {
        get {
            Solve();
            return AcceptedParts.Aggregate(0L, (partAcc, part) => {
                return partAcc + part.Ranges.Aggregate(1L, (rangeAcc, keyValue) => rangeAcc * keyValue.Value.Length);
            });
        }
    }

    private static (RangedPart resolved, RangedPart unresolved) ProcessStep(
        Step step, RangedPart part
    )
    {
        var range = part.Ranges.TryGetValue(step.VariableName, out Range value) ? value : default;
        var resolvedPart = default(RangedPart);
        var unresolvedPart = default(RangedPart);

        switch (step.Operator) {
            case Operator.Always:
                resolvedPart = part.Copy();
                resolvedPart.WorkflowName = step.DestinationStep;
                break;
            case Operator.LessThan:
                if (range.End < step.Value) {
                    resolvedPart = part.Copy();
                    resolvedPart.WorkflowName = step.DestinationStep;
                } else if (range.Start >= step.Value) {
                    unresolvedPart = part.Copy();
                } else {
                    resolvedPart = part.Copy();
                    resolvedPart.WorkflowName = step.DestinationStep;
                    resolvedPart.Ranges[step.VariableName] = new Range(range.Start, step.Value - 1);

                    unresolvedPart = part.Copy();
                    unresolvedPart.Ranges[step.VariableName] = new Range(step.Value, range.End);
                }
                break;
            case Operator.MoreThan:
                if (range.End <= step.Value) {
                    unresolvedPart = part.Copy();
                } else if (range.Start > step.Value) {
                    resolvedPart = part.Copy();
                    resolvedPart.WorkflowName = step.DestinationStep;
                } else {
                    resolvedPart = part.Copy();
                    resolvedPart.WorkflowName = step.DestinationStep;
                    resolvedPart.Ranges[step.VariableName] = new Range(step.Value + 1, range.End);

                    unresolvedPart = part.Copy();
                    unresolvedPart.Ranges[step.VariableName] = new Range(range.Start, step.Value);
                }
                break;
        }

        return (resolvedPart, unresolvedPart);
    }

    private void ProcessPart(RangedPart part)
    {
        var unresolvedPart = part.Copy();
        var workflow = Workflows.First(wf => wf.Name == part.WorkflowName);
        var resolvedParts = new List<RangedPart>();

        foreach (Step step in workflow.Steps) {
            (var resolvedPart, unresolvedPart) = ProcessStep(step, unresolvedPart);

            if (resolvedPart != default) {
                resolvedParts.Add(resolvedPart);
            }

            if (unresolvedPart == default) {
                break;
            }
        }

        AcceptedParts.AddRange(resolvedParts.Where(p => p.WorkflowName == "A"));
        QueuedParts.AddRange(resolvedParts.Where(p => p.WorkflowName != "A" && p.WorkflowName != "R"));
    }

    private void Solve()
    {
        AcceptedParts = [];
        QueuedParts = [new("in", new Dictionary<char, Range>() {
            {'X', new(1, 4000)},
            {'M', new(1, 4000)},
            {'A', new(1, 4000)},
            {'S', new(1, 4000)}
        })];
        while (QueuedParts.Count > 0) {
            var part = QueuedParts.First();
            QueuedParts.Remove(part);
            ProcessPart(part);
        }
    }
}
