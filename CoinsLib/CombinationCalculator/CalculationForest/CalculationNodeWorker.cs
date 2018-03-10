using System;
using System.Collections.Generic;
using System.Linq;
using CoinsLib.Coins;

namespace CoinsLib.CombinationCalculator.CalculationForest
{
    public class CalculationNodeWorker : CalculationNode
    {
        public CalculationNodeWorker(Coin c):base(c)
        {
            
        }

        private int possCombos = 0;
        /// <summary>
        /// possible combinations for each coin is our #units 
        /// </summary>
        /// <returns></returns>
        public override int PossibleCombinationsForOneCoin()
        {
            if (possCombos == 0)
                possCombos = parent.PossibleCombinationsForOneCoin() * Head;
            return possCombos;
        }

        /// <summary>
        /// Given an array from our parent indicating how many Total coins the parent
        /// has produced - we have a relatively simple way of calculating how many 
        /// total coins we will produce. 
        /// 
        /// Insight is that every time we ADD our unit to the previous (parent) combination
        /// we are adding one coin = +1 but subtracting UNIT coins/bottom UNIT.
        /// so, e.g., if we are 'head' in the combination (4,1) then we subtract 3 coins          
        /// from each entry in the input array to reflect the fact we are reducing by 3 overall. 
        /// if  we are 'head' in the combination (8,4) then we subtract 1 coin.
        /// 
        /// If we are UNEVEN - then we need a different technique!
        /// 
        /// That gives us how many less coins we will generate each time our UNIT is triggered (value /unit)
        /// 
        /// In terms of how many combinations we need to generate which is the other factor
        /// it is the same number as is given to us by parent MINUS the number of combinations 
        /// that the parent will produce per coin.  Because in adding our coin we are reducing the 
        /// number of combinations 
        /// 
        /// Figure out  how many fewer coins we will produce from the parent set. 
        /// 
        /// </summary>
        /// <param name="valueToCalculate"></param>
        /// <param name="arr"></param>
        /// <param name="parentState"></param>
        /// <param name="depth"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public override long CalculateTotalCoins(int valueToCalculate,long[] arr, CalcState parentState,int depth = 0)
        {
            #if DEBUG
            if (depth != this.Depth)
                throw new Exception("Calc tree out of synch somehow ");
            
            if (valueToCalculate <= 0)
                throw new Exception($"Unexpected valueToCalculate - {valueToCalculate}");

            #endif


            return 1;
        }
    }
}