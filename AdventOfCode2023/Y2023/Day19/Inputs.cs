namespace AdventOfCode.Utils.Y2023.Day19;

public enum Operator { Always, LessThan, MoreThan };
public record struct Part(int X, int M, int A, int S);
public record struct Step(char VariableName, Operator Operator, int Value, string DestinationStep);
public record struct Workflow(string Name, IList<Step> Steps);
