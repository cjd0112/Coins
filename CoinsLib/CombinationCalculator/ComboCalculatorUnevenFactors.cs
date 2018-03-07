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
     * to calculate all combinations 'from scratch' when any number is triggered
     * - see description of CoinsForUnevenFactors
     */
    public class ComboCalculatorUnevenFactors : ComboCalculatorBase
    {
        private int lowestFactor;
        private IList<int> units;
        private Int32 unitSum;

        public ComboCalculatorUnevenFactors(Coin c) : base(c)
        {
            units = c.GenerateMyUnits().ToArray();
            lowestFactor = units.Last();
            unitSum = units.Sum();
        }
        /// <summary>
        /// Recalculates when any of the underlying units is 'triggered'
        /// i.e., is a multiple of the value. 
        /// </summary>
        /// <param name="source">source value to calc</param>
        /// <param name="newNumberOfCoins">tally for holding combo results</param>
        /// <returns>number of combinations found for this result</returns>
        /// <exception cref="Exception"></exception>
        public override Int64 Calculate(int source, int maximumCoins, Int64[] newNumberOfCoins)
        {
            Int64 cnt = 0;

            // do calc if any multiples are triggered
            if (units.Any(x => source % x == 0))
            {
                if (units.Count == 7)
                    cnt = CoinsForUnevenFactors.CalculateTotalCoinsForEachComboAndReturnCount(newNumberOfCoins,maximumCoins,
                        source, units[0], units[1], units[2],
                        units[3], units[4], units[5], units[6]);
                else if (units.Count == 6)
                    cnt = CoinsForUnevenFactors.CalculateTotalCoinsForEachComboAndReturnCount(newNumberOfCoins,maximumCoins,
                        source, units[0], units[1], units[2],
                        units[3], units[4], units[5]);
                else if (units.Count == 5)
                    cnt = CoinsForUnevenFactors.CalculateTotalCoinsForEachComboAndReturnCount(newNumberOfCoins,maximumCoins,
                        source, units[0], units[1], units[2],
                        units[3], units[4]);
                else if (units.Count == 4)
                    cnt = CoinsForUnevenFactors.CalculateTotalCoinsForEachComboAndReturnCount(newNumberOfCoins,maximumCoins,
                        source, units[0], units[1], units[2],
                        units[3]);
                else if (units.Count == 3)
                    cnt = CoinsForUnevenFactors.CalculateTotalCoinsForEachComboAndReturnCount(newNumberOfCoins,maximumCoins,
                        source, units[0], units[1], units[2]);
                else if (units.Count == 2)
                    cnt = CoinsForUnevenFactors.CalculateTotalCoinsForEachComboAndReturnCount(newNumberOfCoins,maximumCoins,
                        source, units[0], units[1]);
                else if (units.Count == 1)
                    cnt = CoinsForUnevenFactors.CalculateTotalCoinsForEachComboAndReturnCount(newNumberOfCoins,maximumCoins,
                        source, units[0]);
                else
                    throw new Exception($"Unexpected number of values initialized - {units.Count} > 7");
                
            }
            return cnt;

        }


    }
}
