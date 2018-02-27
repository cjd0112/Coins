using System.Collections.Generic;

namespace CoinsLib.Values
{
    public class ValuePartitioner
    {
        public static IEnumerable<(int lhs, int rhs)> PossibleWaysToDivideValueInTwo(int val)
        {
            for (int i = 1; i < val; i++)
            {
                yield return (i, val - i);
            }
        }
    }
}
