namespace AdventOfCode.Utils.Y2023.Day12;

public readonly struct MatchState : IEquatable<MatchState>
{
    private readonly string _p;

    /// <summary>
    /// How many different valid arrangements are congruent with the pattern up to this point in the match
    /// </summary>
    public long N { get; }
    /// <summary>
    /// The token to consume
    /// </summary>
    public char C { get; }
    /// <summary>
    /// Are we currently in a '#' run
    /// </summary>
    public bool Ir { get; }
    /// <summary>
    /// The current index in the pattern string
    /// </summary>
    public int Si { get; }
    /// <summary>
    /// The current index in the 'runs' collection
    /// </summary>
    public int Ri { get; }
    /// <summary>
    /// The current run length
    /// </summary>
    public int Rl { get; }

    private MatchState(string p, long n, char c, bool ir, int si, int ri, int rl)
    {
        _p = p;
        N = n;
        C = c;
        Ir = ir;
        Si = si;
        Ri = ri;
        Rl = rl;
    }

    public static MatchState Initial(string pattern)
    {
        return new MatchState(p: pattern, n: 1, c: pattern[0], ir: false, si: 0, ri: -1, rl: 0);
    }

    public MatchState Replace(char c)
    {
        return new MatchState(p: _p, n: N, c: c, ir: Ir, si: Si, ri: Ri, rl: Rl);
    }

    public MatchState Advance()
    {
        return C == '.'
            ? AdvanceWorking()
            : AdvanceDamaged();
    }

    private MatchState AdvanceWorking()
    {
        var c = GetNextTokenOrDefault();
        return new MatchState(p: _p, n: N, c: c, ir: false, si: Si + 1, ri: Ri, rl: 0);
    }

    private MatchState AdvanceDamaged()
    {
        var c = GetNextTokenOrDefault();
        return Ir
            ? new MatchState(p: _p, n: N, c: c, ir: true, si: Si + 1, ri: Ri, rl: Rl + 1)
            : new MatchState(p: _p, n: N, c: c, ir: true, si: Si + 1, ri: Ri + 1, rl: 1);
    }

    private char GetNextTokenOrDefault()
    {
        var index = Si + 1;
        var valid = index >= 0 && index < _p.Length;

        return valid
            ? _p[index]
            : '\0';
    }

    public bool Equals(MatchState other)
    {
        return
            N == other.N &&
            C == other.C &&
            Ir == other.Ir &&
            Si == other.Si &&
            Ri == other.Ri &&
            Rl == other.Rl;
    }

    public override bool Equals(object? obj)
    {
        return obj is MatchState other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(N, C, Ir, Si, Ri, Rl);
    }

    public static bool operator ==(MatchState left, MatchState right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(MatchState left, MatchState right)
    {
        return !left.Equals(right);
    }
}
