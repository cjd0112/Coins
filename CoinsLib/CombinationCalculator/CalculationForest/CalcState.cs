using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace CoinsLib.CombinationCalculator.CalculationForest
{
    public class CalcState 
    {
        public CalcState(int max, int min, int step)
        {
            MaxParentCoins = max;
            MinParentCoins = min;
            Step = step;
        }
        public int MaxParentCoins;
        public int MinParentCoins;
        public int Step;

        public int NumberCombinations()
        {
            return (MaxParentCoins - MinParentCoins) / Step;
        }


    }
}
