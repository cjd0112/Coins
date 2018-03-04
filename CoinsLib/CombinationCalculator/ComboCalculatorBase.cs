using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoinsLib.CombinationCalculator
{
    /*
     * ComboCalculatorBase - what is in common to Brute-Force
     * and Quick - is that they need to know when to trigger an update
     * to avoid unnecessary calculations
     *
     * as that functionality is in common makes sense to put in a base class
     *
     */
    public abstract class ComboCalculatorBase : IComboCalculator
    {
        protected List<NumberCombinationsTicker> vals = new List<NumberCombinationsTicker>();

        protected int startingNumber;

        protected int externalValue;

        protected void Increment()
        {
            externalValue++;
        }

        protected ComboCalculatorBase(Coin c)
        {
            Initialize(c);
        }


        public abstract void Increment(Action<Int32> newNumberOfCoins);

        protected Coin coin;

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
