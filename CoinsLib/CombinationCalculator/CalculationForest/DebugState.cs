using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace CoinsLib.CombinationCalculator.CalculationForest
{
    public class DebugState
    {
        public DebugState(CalculationNode n, int val, IList<(int noCoins,int noCombinations)> debug)
        {
            node = n;
            Value = val;
            CoinsAndCombos = debug;
        }
        public readonly CalculationNode node;
        public readonly int Value;
        public readonly IList<(int noCoins, int noCombinations)> CoinsAndCombos;
    }
}
