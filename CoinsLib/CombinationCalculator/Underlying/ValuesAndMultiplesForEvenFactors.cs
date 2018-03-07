using System;
using System.Collections.Generic;
using System.Linq;

namespace CoinsLib.CombinationCalculator.Underlying
{
    /// <summary>
    /// Generates required representation of a number for EvenFactors
    /// </summary>
    public class ValuesAndMultiplesForEvenFactors
    {
        /// <summary>
        /// Recursive version that translates between a Coin structure and a value 
        /// and provides a list of the (value/multiple) pairs that the 
        /// optimized EvenFactor ComboCalculator requires 
        /// </summary>
        /// <param name="c">Coin</param>
        /// <param name="value">Value to calculate - needs to be multiple of lowest unit or will throw exception</param>
        /// <param name="startPoint">used internally </param>
        /// <returns></returns>
        /// <exception cref="Exception">throws if provided value is not a multiple of lowest unit </exception>
        public static IEnumerable<(int value, int multiple)> GenerateValuesAndMultiplesRecursive(Coin c, Int32 value,int startPoint = 0)
        {
            #if DEBUG
                if (c.RequiresUnevenFactorCalculator())
                    throw new Exception("invalid option - requires BruteForceCalculator");
    
                if (value % c.GenerateMyUnits().Last() != 0)
                    throw new Exception($"Generate coins and multiples requires factor of lowest value - {c.GenerateMyUnits().Last()}");            
            #endif
            
            if (startPoint == 0)
                startPoint = c.GenerateMyUnits().Sum();

            var differenceBetweenValAndStartPoint = value - startPoint;

            int coins = (differenceBetweenValAndStartPoint / c.Units) + 1;

            yield return (coins, c.Units);
            
            if (c.Next != null)
                foreach (var y in GenerateValuesAndMultiplesRecursive(c.Next, value, startPoint))
                    yield return y;

        }
        
        /// <summary>
        /// Translates between a Coin structure and a value 
        /// and provides a list of the (value/multiple) pairs that the 
        /// optimized EvenFactor ComboCalculator requires 
        /// </summary>
        /// <param name="c">Coin</param>
        /// <param name="value">Value to calculate - needs to be multiple of lowest unit or will throw exception</param>
        /// <param name="startPoint">used internally </param>
        /// <returns></returns>
        /// <exception cref="Exception">throws if provided value is not a multiple of lowest unit </exception>
        public static IEnumerable<(int value, int multiple)> GenerateValuesAndMultiples(Coin c, Int32 value,int startPoint = 0)
        {
#if DEBUG
            if (c.RequiresUnevenFactorCalculator())
                throw new Exception("invalid option - requires BruteForceCalculator");
    
            if (value % c.GenerateMyUnits().Last() != 0)
                throw new Exception($"Generate coins and multiples requires factor of lowest value - {c.GenerateMyUnits().Last()}");            
#endif

            do
            {
                if (startPoint == 0)
                    startPoint = c.GenerateMyUnits().Sum();

                var differenceBetweenValAndStartPoint = value - startPoint;

                int coins = (differenceBetweenValAndStartPoint / c.Units) + 1;

                yield return (coins, c.Units);
                c = c.Next;
            } while (c!= null);

        }


    }
}