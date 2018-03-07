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
    /// the value that is incremented by the calling class. 
    /// </summary>
    /// 
    public abstract class ComboCalculatorBase : IComboCalculator
    {
        protected ComboCalculatorBase(Coin c)
        {
            coin = c;
        }

        protected Coin coin;

        public abstract long Calculate(int value, int maxCoins, long[] coinCountHolder);
    }
}
