using System;
using System.Collections.Generic;
using System.Text;
using CoinsLib.CombinationCalculator.Cache;
using CoinsLib.CombinationCalculator.Underlying;
using CoinsLib.Util;

namespace CoinsLib.CombinationCalculator
{
    /// <summary>
    /// A ComboCalculator class good for regression testing
    /// calculates combinations using slow recursive method.
    /// re-calculates on every value ... 
    /// </summary>
    public class ComboCalculatorRecursive : ComboCalculatorBase
    {
        public ComboCalculatorRecursive(Coin c) : base(c)
        {

        }
        public override Int64 Calculate(int source, int maximumCoins, Int64[] newNumberOfCoins)
        {
            Int64 cnt = 0;
            foreach (var c in CoinsByRecursion.TotalCoinsForEachCombinationForValue(coin, source))
            {
                newNumberOfCoins[c]++;
                cnt++;
            }

            return cnt;

        }


    }
}
