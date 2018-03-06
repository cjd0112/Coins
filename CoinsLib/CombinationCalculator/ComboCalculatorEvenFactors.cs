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
     *  it keeps track of every coin when it 'hits' and each time
     *  the lowest one 'ticks' it uses simple sums to
     *  generate the number of combinations.
     *
     *  it doesn't work where any in chain are not multiples, i.e., Florin - see ComboCalculatorBruteForce
     *
     *  It relies on the caller to increment its state - and that is reflected in
     * the interface
     */
    public class ComboCalculatorEvenFactors : ComboCalculatorBase
    {
        public ComboCalculatorEvenFactors(Coin c) : base(c)
        {

        }

        /// <summary>
        /// Uses optimises calculation for TotalCoin combinations
        /// because we know we only have Units in our set of coins
        /// that are successive multiples - e.g., 8,4,2,1 ... 
        /// so can be optimized to avoid division. 
        /// only triggers when the lowest unit fires. 
        /// 
        /// Increments higher NumberCombinationsTickers first
        /// and then increments the lowest ... 
        /// 
        /// Stores results in large array provided.  The implementation could be 
        /// better de-coupled ... but this is efficient. 
        /// </summary>
        /// <param name="newNumberOfCoins"></param>
        /// <returns>Number of combinations found</returns>
        /// <exception cref="Exception">Exception if internal number of Tickers happens to be wrong ... shouldn;t happen</exception>
        public override Int64 Increment(ref Int64[] newNumberOfCoins)
        {
            base.Increment();
            
            Int64 cnt = 0;                

            // increment all except our last (lowest) item
            for (int i = 0; i < vals.Count-1; i++)
            {
                vals[i].Increment();
            }

            // increment our first item - we know for ComboQuick - that whenever lowest triggers
            // we need to send a new set of combinations
            if (vals.Last().Increment())
            {
                if (vals.Count == 7)
                    cnt = CoinsForEvenFactors.CalculateTotalCoinsForEachComboAndReturnCount(ref newNumberOfCoins,externalValue,vals[0].VM(), vals[1].VM(), vals[2].VM(), vals[3].VM(), vals[4].VM(),vals[5].VM(), vals[6].VM());
                else if (vals.Count == 6)
                    cnt = CoinsForEvenFactors.CalculateTotalCoinsForEachComboAndReturnCount(ref newNumberOfCoins,externalValue,vals[0].VM(), vals[1].VM(), vals[2].VM(), vals[3].VM(), vals[4].VM(),vals[5].VM());
                else if (vals.Count == 5)
                    cnt = CoinsForEvenFactors.CalculateTotalCoinsForEachComboAndReturnCount(ref newNumberOfCoins,externalValue,vals[0].VM(), vals[1].VM(), vals[2].VM(), vals[3].VM(), vals[4].VM());
                else if (vals.Count == 4)
                    cnt = CoinsForEvenFactors.CalculateTotalCoinsForEachComboAndReturnCount(ref newNumberOfCoins,externalValue,vals[0].VM(), vals[1].VM(), vals[2].VM(), vals[3].VM());
                else if (vals.Count == 3)
                    cnt = CoinsForEvenFactors.CalculateTotalCoinsForEachComboAndReturnCount(ref newNumberOfCoins,externalValue,vals[0].VM(), vals[1].VM(), vals[2].VM());
                else if (vals.Count == 2)
                    cnt = CoinsForEvenFactors.CalculateTotalCoinsForEachComboAndReturnCount(ref newNumberOfCoins,externalValue,vals[0].VM(), vals[1].VM());
                else if (vals.Count == 1)
                    cnt = CoinsForEvenFactors.CalculateTotalCoinsForEachComboAndReturnCount(ref newNumberOfCoins,externalValue,vals[0].VM());
                else
                    throw new Exception($"Unexpected number of values initialized - {vals.Count} > 7");
            }

            return cnt;
        }
    }
}
