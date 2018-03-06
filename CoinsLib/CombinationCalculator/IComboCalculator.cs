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
        IComboCalculator Initialize(Coin c);
        Int64 Increment(Int64[] coinCountHolder);
    }
}
