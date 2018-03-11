using System;
using System.Collections.Generic;
using System.Text;

namespace CoinsLib.CombinationCalculator.CalculationForest
{
    /// <summary>
    /// simple calculation node vector for 
    /// when we receive a scalar figure from our parent (root-node)
    /// we produce a simple vector of combinations
    /// </summary>
    public class CalculationNodeVector :CalculationNode
    {
        private int noCoinsToSubtractForEachUnit = -1;
        public CalculationNodeVector(Coin c,CalculationGrid g) : base(c,g)
        {
        }

        public override long CalculateTotalCoins(int valueToCalculate, long[] arr, CalcState parentState = null,
            int depth = 1)
        {
#if DEBUG
            if (depth != 2)
                throw new Exception("CalculationNodeVector should only be called when depth == 2");

            if (parentState.NumberCombinations != 1)
                throw new Exception("Expecting scalar from root node");

            if (noCoinsToSubtractForEachUnit == -1)
            {
                noCoinsToSubtractForEachUnit = (Head / GetRootUnit()) - 1;

                if (noCoinsToSubtractForEachUnit < 1)
                    throw new ArgumentException("noCoinsToSubtractForEachUnit expected to be >=1");
            }

            Grid.StartDebug(this,valueToCalculate);

#endif


            long cnt = 0;
            if (valueToCalculate - SumOfUnits >= 0)
            {

                var max = -1;
                var min = -1;
                var startValue = parentState.MaxParentCoins;
                for (int i = 0; i < ((valueToCalculate - SumOfUnits) / Head)+1; i++)
                {
                    startValue -= noCoinsToSubtractForEachUnit;
                    if (max == -1)
                        max = startValue;
                    min = startValue;
                    arr[startValue]++;

#if DEBUG
                    Grid.Debug(this, startValue, 1);
#endif
                    cnt++;
                }

#if DEBUG
                Grid.EndDebug(this);
#endif

                var ps = new CalcState(max,min,(int)cnt);
                foreach (var c in GetChildren())
                {
                    cnt += c.CalculateTotalCoins(valueToCalculate, arr, ps, depth + 1);
                }
            }


            return cnt;
        }



    }
}
