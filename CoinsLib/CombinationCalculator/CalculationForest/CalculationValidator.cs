using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace CoinsLib.CombinationCalculator.CalculationForest
{
    /// <summary>
    /// Validates key aspects of calculation - given an input key for ease of reference
    /// Can pre-calculate key-values and make sure they are as expected
    /// good for debugging internals outside of unit tests
    /// </summary>
    public static class CalculationValidator
    {
        public static String Key(CalculationNode c)
        {
            if (c.GetCoin().GenerateMyUnits().SequenceEqual(new[] {4, 2, 1}))
                return "4_2_1";
            return "";
        }

        public static void Validate(String key, int differenceInParentComboNumber,int noCoinsToSubtractForEachUnit)
        {
            if (key == "4_2_1")
            {
                Debug.Assert(differenceInParentComboNumber == 2 );
                Debug.Assert(noCoinsToSubtractForEachUnit == );
            }

        }
    }
}
