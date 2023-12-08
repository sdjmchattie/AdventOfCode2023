namespace AdventOfCode.Utils.Y2023.Day08;

readonly struct Node(string name, string[] paths)
{
    public readonly string Name = name;
    private readonly string[] Paths = paths;
    public readonly string NodeNameInDirection(Direction direction) => Paths[(int)direction];
}
