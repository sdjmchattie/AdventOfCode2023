namespace AdventOfCode.Utils.Y2023.Day19;

class Evaluator(ICollection<Workflow> workflows, ICollection<Part> parts)
{
    private readonly ICollection<Workflow> Workflows = workflows;
    private readonly ICollection<Part> Parts = parts;
    private IEnumerable<Part> AcceptedParts = [];

    private bool IsPartAcceptable(Part part)
    {
        var wfName = "in";
        while (true) {
            var workflow = Workflows.First(wf => wf.Name == wfName);
            wfName = workflow.Steps.First(step => {
                switch (step.Operator) {
                    case Operator.Always:
                        return true;
                    case Operator.LessThan:
                        var lessVar = typeof(Part).GetProperty(char.ToString(step.VariableName));
                        return (int)(lessVar?.GetValue(part) ?? 0) < step.Value;
                    case Operator.MoreThan:
                        var moreVar = typeof(Part).GetProperty(char.ToString(step.VariableName));
                        return (int)(moreVar?.GetValue(part) ?? 0) > step.Value;
                    default:
                        return false;
                }
            }).DestinationStep;

            if (wfName == "A") { return true; }
            if (wfName == "R") { return false; }
        }
    }

    public int Score
    {
        get {
            AcceptedParts = Parts.Where(IsPartAcceptable);
            return AcceptedParts.Aggregate(0, (acc, part) => acc + part.X + part.M + part.A + part.S);
        }
    }
}
