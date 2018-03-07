using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using CoinsLib.CombinationCalculator.Underlying;
using CoinsLib.Util;

namespace CoinsLib.CombinationCalculator
{
    /*
     *  quick combo calculator relies on fact that each
     *  Coin in the chain is a multiple of previous
     *  each time the lowest one 'ticks' it uses simple sum/multiplication to
     *  generate the number of combinations.
     *
     *  it doesn't work where any in chain are not multiples, i.e., HalfCrown - see UnevenFactors algo
     *
     */
    
    public class ComboCalculatorEvenFactors : ComboCalculatorBase
    {
        private int lowestFactor;
        private IEnumerable<int> units;
        private Int32 unitSum;
        
        public ComboCalculatorEvenFactors(Coin c) : base(c)
        {
            units = c.GenerateMyUnits().ToArray();
            lowestFactor = units.Last();
            unitSum = units.Sum();

        }

        /// <summary>
        /// Uses optimises calculation for TotalCoin combinations
        /// because we know we only have Units in our set of coins
        /// that are successive multiples - e.g., 8,4,2,1 ... 
        /// so can be optimized to avoid division. 
        /// only triggers when the lowest unit fires and
        /// when our startingPoint = unitSum = '15' in above example
        /// is reached. 
        /// Stores results in large array provided.  The implementation could be 
        /// better de-coupled ... but this is efficient. 
        /// </summary>
        /// <param name="source">value to calculate</param>
        /// <<param name="maximumCoins">max coins to generate</param>
        /// <param name="newNumberOfCoins"></param>
        /// <returns>Number of combinations found</returns>
        /// <exception cref="Exception">Exception if internal number of Tickers happens to be wrong ... shouldn;t happen</exception>
        public override Int64 Calculate(int source, int maximumCoins, Int64[] newNumberOfCoins)
        {
            Int64 cnt = 0;
            if (source >= unitSum && source % lowestFactor == 0)
            {
                var vals = ValuesAndMultiplesForEvenFactors.GenerateValuesAndMultiplesRecursive(coin, source).ToList();
                
                if (vals.Count() == 7)
                    cnt = CoinsForEvenFactors.CalculateTotalCoinsForEachComboAndReturnCount(newNumberOfCoins,maximumCoins,source,vals[0], vals[1], vals[2], vals[3], vals[4],vals[5], vals[6]);
                else if (vals.Count() == 6)
                    cnt = CoinsForEvenFactors.CalculateTotalCoinsForEachComboAndReturnCount(newNumberOfCoins,maximumCoins,source,vals[0], vals[1], vals[2], vals[3], vals[4],vals[5]);
                else if (vals.Count() == 5)
                    cnt = CoinsForEvenFactors.CalculateTotalCoinsForEachComboAndReturnCount(newNumberOfCoins,maximumCoins,source,vals[0], vals[1], vals[2], vals[3], vals[4]);
                else if (vals.Count() == 4)
                    cnt = CoinsForEvenFactors.CalculateTotalCoinsForEachComboAndReturnCount(newNumberOfCoins,maximumCoins,source,vals[0], vals[1], vals[2], vals[3]);
                else if (vals.Count() == 3)
                    cnt = CoinsForEvenFactors.CalculateTotalCoinsForEachComboAndReturnCount(newNumberOfCoins,maximumCoins,source,vals[0], vals[1], vals[2]);
                else if (vals.Count() == 2)
                    cnt = CoinsForEvenFactors.CalculateTotalCoinsForEachComboAndReturnCount(newNumberOfCoins,maximumCoins,source,vals[0], vals[1]);
                else if (vals.Count() == 1)
                    cnt = CoinsForEvenFactors.CalculateTotalCoinsForEachComboAndReturnCount(newNumberOfCoins,maximumCoins,source,vals[0]);
                else
                    throw new Exception($"Unexpected number of values initialized - {vals.Count} > 7");
                
            }

            return cnt;
        }
    }
}
