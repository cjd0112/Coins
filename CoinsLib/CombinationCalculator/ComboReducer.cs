using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using CoinsLib.Util;

namespace CoinsLib.CombinationCalculator
{
    /// <summary>
    /// A class to keep track of the totalCoins for each combination that 
    /// the ComboCalculatorBase derived engine will produce. 
    /// 
    /// This uses a Int64[Value][Value] grid where each column represents 
    /// a Number (for which you figure out the totalCoins) and each 
    /// row represents the count of totalCoins for that Number. 
    /// 
    /// For efficiency, the calc-engine requires a direct 'buffer' to store 
    /// its results ... e.g., if there was less requirement for speed
    /// then you would provide a Function to ComboCalculatorBase, rather than 
    /// an Array to support better encapsulation/separation of concern etc., 
    /// 
    /// 
    /// You know that 'Value' is big enough for the columns (of course)
    /// In terms of rows The maximum number of coins for each number is 
    /// given by the Value/Lowest Unit (.5d) - which is represented by '1'.
    /// So Value will also be fine to hold the totalCoins for each combination. 
    /// </summary>
    public class ComboReducer
    {
        private Int64[][] reducerGrid;

        private int value;
        public ComboReducer(Int32 value)
        {
            this.value = value;
            reducerGrid = new Int64[value][];
            foreach (var c in Enumerable.Range(0, value))
            {
                reducerGrid[c] = new Int64[value];
            }
        }

        /// <summary>
        /// Given value return the right storage array to hold totalCoin tally 
        /// for combination calculations
        /// </summary>
        /// <param name="value">value</param>
        /// <returns>Buffer for results</returns>
        /// <exception cref="ArgumentException">throws if value out of range</exception>
        public Int64[] GetStorageArrayForValue(Int32 value)
        {
            if (value >= this.value || value  < 0)
                throw new ArgumentException($"Invalid argument - {value} - expected max - {this.value}");            
            return reducerGrid[value];
        }

        public Int64[][] GetStorageArray()
        {
            return reducerGrid;
        }


        /// <summary>
        /// Given two values - it will find the two corresponding
        /// rows of totalCoin amounts for those values and do a 
        /// simple calculation to determine how many # of totalcoins are in common.
        /// 
        /// Zips up the equivalent length sequences and takes Sum of the 
        /// minimum ... 
        /// </summary>
        /// <param name="i">tuple indicating the two values to get</param>
        /// <returns>total number of cases where coins can be shared evenly</returns>
        /// <exception cref="ArgumentException"></exception>
        public Int64 SharedCombinationOfTwoValues((Int32 val1, Int32 val2) i)
        {
            if (i.val1 >= this.value || i.val1 < 0)
                throw new ArgumentException($"Invalid argument - {i.val1} - expected max - {this.value}");

            if (i.val2 >= this.value || i.val2 < 0)
                throw new ArgumentException($"Invalid argument - {i.val2} - expected max - {this.value}");

            var g = reducerGrid[i.val1].Zip(reducerGrid[i.val2], (x, y) => (x, y)).Sum(x =>x.Item1 * x.Item2);
            
 
            return g;
        }
    }
}
