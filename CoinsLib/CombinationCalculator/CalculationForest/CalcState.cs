using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace CoinsLib.CombinationCalculator.CalculationForest
{
    public class CalcState : IEnumerable<int>, IEnumerator<int>
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


        public IEnumerator<int> GetEnumerator()
        {
            return this;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public bool MoveNext()
        {
            if (Current > MinParentCoins)
            {
                Current -= Step;
                return true;

            }

            return false;
        }

        public void Reset()
        {
            Current = MaxParentCoins;
        }

        public int Current { get; private set; }

        object IEnumerator.Current => Current;

        public void Dispose()
        {
        }
    }
}
