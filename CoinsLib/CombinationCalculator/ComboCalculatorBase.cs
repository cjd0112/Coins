using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoinsLib.CombinationCalculator.Underlying;

namespace CoinsLib.CombinationCalculator
{
    /// <summary>
    /// Base class for functionality common to organizing the 
    /// different combinations of Coins into the right format to process their output. 
    /// i.e., keeps track of the 'externalNumber' which is 
    /// the value that is incremented by the calling class. 
    /// 
    /// keeps a 'NumberCombinationTicker' which indicates to 
    /// each calculator when it needs to re-calculate.  
    /// 
    /// Relies on caller calling 'increment' to increase
    /// value on each run ... 
    /// </summary>
    public abstract class ComboCalculatorBase : IComboCalculator
    {
        /// <summary>
        /// Each Coin has its own 'ticker' which keeps track of its 
        /// current value on each increment. 
        /// </summary>
        protected List<NumberCombinationsTicker> vals = new List<NumberCombinationsTicker>();

        /// <summary>
        /// E.g., if combo is '6,3,2' startingNumber will be 11 (our first possible 'hit')
        /// i.e., we ignore any values below our starting number
        /// </summary>
        protected int startingNumber;

        protected int externalValue;

        /// <summary>
        /// Ensure the caller uses 'Increment' rather than setting
        /// a value explicitly - which breaks our optimised model. 
        /// 'ExternalValue' means we have our own copy of the actual
        /// value that is currently being calculated. 
        /// </summary>
        protected void Increment()
        {
            externalValue++;
        }

        protected ComboCalculatorBase(Coin c)
        {
            Initialize(c);
        }


        public abstract Int64 Increment(Int64[] coinCountHolder);

        protected Coin coin;

        /// <summary>
        /// establish our NumberCombinationsTickers for each Unit in the set of Coins. 
        /// </summary>
        /// <param name="c"></param>
        /// <returns>this - potential for Fluent interface ... </returns>
        /// <exception cref="ArgumentException">Various exceptions if assumptions on assumptions are not met</exception>
        public IComboCalculator Initialize(Coin c)
        {
            coin = c;
            var j = c.NodeCount();
            if (j > 7)
                throw new ArgumentException("Currently only '7' nodes are supported - it is 2 minute job to extend this class to support more ... ");

            var units = c.GenerateMyUnits().ToArray();

            if (!units.Any())
                throw new ArgumentException("No units found in coin - something gone wrong ... ");

            if (units.Count() > 7)
                throw new ArgumentException("Currently only '7' nodes are supported - it is 2 minute job to extend this class to support more ... ");

            startingNumber = units.Sum();
            foreach (var unit in units)
            {
                vals.Add(new NumberCombinationsTicker(unit, startingNumber));
            }

            return this;
        }
    }
}
