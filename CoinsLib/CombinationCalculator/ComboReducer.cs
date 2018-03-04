using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoinsLib.Util;

namespace CoinsLib.CombinationCalculator
{
    /*
     * Keeps-track of number of combinations for a value - on a per-value basis
     *
     * max-no co
     */
    public class ComboReducer
    {
        private List<List<Int32>> reducerGrid;

        private int value;
        public ComboReducer(Int32 value)
        {
            reducerGrid = new List<List<Int32>>();
            this.value = value;
            int i = 0;
            foreach (var c in Enumerable.Range(0, value))
            {
                reducerGrid.Add(new List<Int32>());
                reducerGrid.Last().AddRange(Enumerable.Range(0,value).Select(x=>0));
            }
        }

        public void AggregateNoCoinsForValue(Int32 val, Int32 noCoins)
        {
            if (val >= this.value || val < 0)
                throw new ArgumentException($"Invalid argument - {val} - expected max - {this.value}");

            if (noCoins >= this.value || noCoins < 0)
                throw new ArgumentException($"Invalid argument - {noCoins} - expected max - {this.value}");

            reducerGrid[val][noCoins]++;

        }

        public int SharedCombinationOfTwoValues((Int32 val1, Int32 val2) i)
        {
            if (i.val1 >= this.value || i.val1 < 0)
                throw new ArgumentException($"Invalid argument - {i.val1} - expected max - {this.value}");

            if (i.val2 >= this.value || i.val2 < 0)
                throw new ArgumentException($"Invalid argument - {i.val2} - expected max - {this.value}");



            var g = reducerGrid[i.val1].Zip(reducerGrid[i.val2], (x, y) => (x, y)).Sum(x => Math.Min(x.Item1, x.Item2));

            Console.WriteLine($"Output for ({i.val1},{i.val2}) is {g}");

            return g;
        }
    }
}
