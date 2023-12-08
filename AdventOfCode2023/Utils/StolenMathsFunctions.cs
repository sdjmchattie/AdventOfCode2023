namespace AdventOfCode.Utils;

class StolenMathsFunctions {
    // As stolen from https://stackoverflow.com/a/20824923/772095 because I'm too
    // lazy to work out how to implement this.

    public static long GCF(long a, long b)
    {
        while (b != 0)
        {
            var temp = b;
            b = a % b;
            a = temp;
        }

        return a;
    }

    public static long LCM(long a, long b) => a / GCF(a, b) * b;
    public static long LCM(IEnumerable<long> factors) => factors.Aggregate(1L, LCM);
}
