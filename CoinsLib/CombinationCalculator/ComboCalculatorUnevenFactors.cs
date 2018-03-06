using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoinsLib.CombinationCalculator.Underlying;
using CoinsLib.Util;

namespace CoinsLib.CombinationCalculator
{
    /*
     * where there is a Coin which cannot be optimized
     * because each successive coin is not a straightforward
     * multiple of previous - i.e, for HalfCrown (2s + 6d) then we need a brute force method
     * to calculate all combinations 'from scratch' when any number 'ticks'
     * - see description of CoinsForUnevenFactors
     */
    public class ComboCalculatorUnevenFactors : ComboCalculatorBase
    {
        public ComboCalculatorUnevenFactors(Coin c) : base(c)
        {

        }
        /// <summary>
        /// Recalculates when any of the underlying units is 'triggered'
        /// i.e., is a multiple of the value. 
        /// </summary>
        /// <param name="newNumberOfCoins">tally for holding combo results</param>
        /// <returns>number of combinations found for this result</returns>
        /// <exception cref="Exception"></exception>
        public override Int64 Increment(ref Int64[] newNumberOfCoins)
        {
            base.Increment();
            Int64 cnt = 0;

            if (vals.Any(x => x.Increment()))
            {
                if (vals.Count == 7)
                    cnt = CoinsForUnevenFactors.CalculateTotalCoinsForEachComboAndReturnCount(ref newNumberOfCoins,
                        externalValue, vals[0].GetMultiple(), vals[1].GetMultiple(), vals[2].GetMultiple(),
                        vals[3].GetMultiple(), vals[4].GetMultiple(), vals[5].GetMultiple(), vals[6].GetMultiple());
                else if (vals.Count == 6)
                    cnt = CoinsForUnevenFactors.CalculateTotalCoinsForEachComboAndReturnCount(ref newNumberOfCoins,
                        externalValue, vals[0].GetMultiple(), vals[1].GetMultiple(), vals[2].GetMultiple(),
                        vals[3].GetMultiple(), vals[4].GetMultiple(), vals[5].GetMultiple());
                else if (vals.Count == 5)
                    cnt = CoinsForUnevenFactors.CalculateTotalCoinsForEachComboAndReturnCount(ref newNumberOfCoins,
                        externalValue, vals[0].GetMultiple(), vals[1].GetMultiple(), vals[2].GetMultiple(),
                        vals[3].GetMultiple(), vals[4].GetMultiple());
                else if (vals.Count == 4)
                    cnt = CoinsForUnevenFactors.CalculateTotalCoinsForEachComboAndReturnCount(ref newNumberOfCoins,
                        externalValue, vals[0].GetMultiple(), vals[1].GetMultiple(), vals[2].GetMultiple(),
                        vals[3].GetMultiple());
                else if (vals.Count == 3)
                    cnt = CoinsForUnevenFactors.CalculateTotalCoinsForEachComboAndReturnCount(ref newNumberOfCoins,
                        externalValue, vals[0].GetMultiple(), vals[1].GetMultiple(), vals[2].GetMultiple());
                else if (vals.Count == 2)
                    cnt = CoinsForUnevenFactors.CalculateTotalCoinsForEachComboAndReturnCount(ref newNumberOfCoins,
                        externalValue, vals[0].GetMultiple(), vals[1].GetMultiple());
                else if (vals.Count == 1)
                    cnt = CoinsForUnevenFactors.CalculateTotalCoinsForEachComboAndReturnCount(ref newNumberOfCoins,
                        externalValue, vals[0].GetMultiple());
                else
                    throw new Exception($"Unexpected number of values initialized - {vals.Count} > 7");
            }
            return cnt;

        }


    }
}
