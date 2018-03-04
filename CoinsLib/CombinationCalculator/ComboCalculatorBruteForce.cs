using System;
using System.Collections.Generic;
using System.Text;
using CoinsLib.Util;

namespace CoinsLib.CombinationCalculator
{
    /*
     * where there is a Coin which cannot be optimized
     * because each successive coin is not a straightforward
     * multiple of previous - i.e, for Florins (2s + 6d) then we need a brute force method
     * to calculate all combinations 'from scratch' when any number 'ticks'
     */
    public class ComboCalculatorBruteForce : ComboCalculatorBase
    {
        public ComboCalculatorBruteForce(Coin c) : base(c)
        {

        }
        public override void Increment(Action<int> newNumberOfCombinations)
        {
            base.Increment();
            QuickCoinsCalculator.BruteForceCombinations(coin, externalValue).ForEach(newNumberOfCombinations);
        }


    }
}
