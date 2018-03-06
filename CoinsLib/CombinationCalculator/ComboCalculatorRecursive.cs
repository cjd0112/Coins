using System;
using System.Collections.Generic;
using System.Text;
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
        public override Int64 Increment(ref Int64[] newNumberOfCoins)
        {
            base.Increment();
            Int64 cnt = 0;
            foreach (var c in CoinsByRecursion.TotalCoinsForEachCombinationForValue(coin, externalValue))
            {
                newNumberOfCoins[c] += c;
                cnt++;
            }

            return cnt;

        }


    }
}
