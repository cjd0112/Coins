using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace CoinsLib.CombinationCalculator.CalculationForest
{
    public class CalcState 
    {
        public CalcState(int max, int min, int count)
        {
            MaxParentCoins = max;
            MinParentCoins = min;
            Count = count;

            if (Count <= 0)
                throw new Exception("Invalid count");

            if (MaxParentCoins < MinParentCoins)
                throw new Exception("Invalid Max/Min coin count");
        }

        public int MaxParentCoins;
        public int MinParentCoins;
        public int Count;

        public int NumberCombinations => Count;

        public int Step
        {
            get
            {
                if (Count == 1)
                    return 0;
                return MaxParentCoins - MinParentCoins / (Count - 1);
            }
        }


    }
}
