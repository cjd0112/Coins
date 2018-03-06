using System;
using System.Collections.Generic;
using System.Linq;

namespace CoinsLib.CombinationCalculator.Underlying
{
    /// <summary>
    /// Helper class for testing  
    /// </summary>
    public class CoinsAndMultiples
    {
        /// <summary>
        /// Translates between a Coin structure and a value 
        /// and provides a list of the (value/multiple) pairs that the 
        /// optimized ComboCalculator requires for testing 
        /// </summary>
        /// <param name="c">Coin</param>
        /// <param name="value">Value to calculate - needs to be multiple of lowest unit or will throw exception</param>
        /// <param name="startPoint">used internally </param>
        /// <returns></returns>
        /// <exception cref="Exception">throws if provided value is not a multiple of lowest unit </exception>
        public static IEnumerable<(int value, int multiple)> GenerateCoinsAndMultiples(Coin c, Int32 value,int startPoint = 0)
        {
            if (c.RequiresUnevenFactorCalculator())
                throw new Exception("invalid option - requires BruteForceCalculator");

            if (value % c.GenerateMyUnits().Last() != 0)
                throw new Exception($"Generate coins and multiples requires factor of lowest value - {c.GenerateMyUnits().Last()}");
            
            if (startPoint == 0)
                startPoint = c.GenerateMyUnits().Sum();

            var differenceBetweenValAndStartPoint = value - startPoint;

            int coins = (differenceBetweenValAndStartPoint / c.Units) + 1;

            yield return (coins, c.Units);
            
            if (c.Next != null)
                foreach (var y in GenerateCoinsAndMultiples(c.Next, value, startPoint))
                    yield return y;

        }

    }
}