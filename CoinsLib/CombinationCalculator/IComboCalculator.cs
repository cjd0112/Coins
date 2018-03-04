using System;
using System.Collections.Generic;
using System.Text;

namespace CoinsLib.CombinationCalculator
{
    public interface IComboCalculator
    {
        IComboCalculator Initialize(Coin c);
        void Increment(Action<Int32> newNumberOfCombinations);
    }
}
