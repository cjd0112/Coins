using System;
using System.Collections.Generic;

namespace CoinsLib.CombinationCalculator.Underlying
{
    public class CoinsByRecursion
    {
        /// <summary>
        /// Recursive function to return all number of coins 
        /// for each combination of a value 
        /// useful for testing purposes - prohibitively slow
        /// for large numbers
        /// </summary>
        /// <param name="c">Input Coin</param>
        /// <param name="value">the value to breakdown</param>
        /// <param name="totalCoins">accumulator for coins</param>
        /// <returns>Array of number of coins for each combination</returns>
        public static IEnumerable<Int64> TotalCoinsForEachCombinationForValue(Coin c, int value, int totalCoins=0)
        {
            if (c.Next == null )
            {
                if (value % c.Units == 0  && value > 0)  
                    yield return value / c.Units + totalCoins;
            }
            else
            {
                for (int i = 1; i <=value/c.Units;i++)
                {
                    totalCoins++;
                    foreach (var x in CoinsByRecursion(c.Next, value - i*c.Units, totalCoins))
                    {
                        yield return x;
                    }
                }
            }
        }

    }
}