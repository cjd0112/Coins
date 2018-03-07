using System;
using System.Collections.Generic;
using System.Text;

namespace CoinsLib.CombinationCalculator
{
    /// <summary>
    /// inter
    /// </summary>
    public interface IComboCalculator
    {
        Int64 Calculate(int value,int maximumCoins, Int64[] coinCountHolder);
    }
}
