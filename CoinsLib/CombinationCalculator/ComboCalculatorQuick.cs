using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using CoinsLib.Util;

namespace CoinsLib.CombinationCalculator
{
    /*
     *  quick combo calculator relies on fact that each
     *  Coin in the chain is a multiple of previous
     *  it keeps track of every coin when it 'hits' and each time
     *  the lowest one 'ticks' it uses simple sums to
     *  generate the number of combinations.
     *
     *  it doesn't work where any in chain are not multiples, i.e., Florin - see ComboCalculatorBruteForce
     *
     *  It relies on the caller to increment its state - and that is reflected in
     * the interface
     */
    public class ComboCalculatorQuick : ComboCalculatorBase
    {
        public ComboCalculatorQuick(Coin c) : base(c)
        {

        }

        public override void Increment(Action<Int32> newNumberOfCoins)
        {
            base.Increment();
            // increment all except our first (lowest) item
            for (int i = 1; i < vals.Count; i++)
            {
                vals[i].Increment();
            }

            // increment our first item - we know for ComboQuick - that whenever lowest triggers
            // we need to send a new set of combinations
            if (vals.First().Increment())
            {
                IEnumerable<Int32> res = null;
                if (vals.Count == 7)
                    res = QuickCoinsCalculator.Calculate(vals[0].VM(), vals[1].VM(), vals[2].VM(), vals[3].VM(), vals[4].VM(),vals[5].VM(), vals[6].VM());
                else if (vals.Count == 6)
                    res = QuickCoinsCalculator.Calculate(vals[0].VM(), vals[1].VM(), vals[2].VM(), vals[3].VM(), vals[4].VM(),vals[5].VM());
                else if (vals.Count == 5)
                    res = QuickCoinsCalculator.Calculate(vals[0].VM(), vals[1].VM(), vals[2].VM(), vals[3].VM(), vals[4].VM());
                else if (vals.Count == 4)
                    res = QuickCoinsCalculator.Calculate(vals[0].VM(), vals[1].VM(), vals[2].VM(), vals[3].VM());
                else if (vals.Count == 3)
                    res = QuickCoinsCalculator.Calculate(vals[0].VM(), vals[1].VM(), vals[2].VM());
                else if (vals.Count == 2)
                    res = QuickCoinsCalculator.Calculate(vals[0].VM(), vals[1].VM());
                else if (vals.Count == 1)
                    res = QuickCoinsCalculator.Calculate(vals[0].VM());

                res.ForEach(newNumberOfCoins);
            }


        }
    }
}
