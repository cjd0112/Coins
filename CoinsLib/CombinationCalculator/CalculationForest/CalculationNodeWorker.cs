using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using CoinsLib.Coins;

namespace CoinsLib.CombinationCalculator.CalculationForest
{
    public class CalculationNodeWorker : CalculationNode
    {
#if DEBUG
        private String validationKey = "";
#endif
        public CalculationNodeWorker(Coin c):base(c)
        {
#if DEBUG
            validationKey = CalculationValidator.Key(this);
#endif
        }

        private int differenceInParentComboNumberForEachCoin = -1;
        private int noCoinsToSubtractForEachUnit = -1;
       

        /// <summary>
        /// Given an array from our parent indicating how many Total coins the parent
        /// has produced - we have a relatively simple way of calculating how many 
        /// total coins we will produce. 
        /// 
        /// We need three items : 
        /// 
        /// A) the parent array of total coins produced for each combo
        /// B) the number of coins to subtract for every combination 
        /// C) the new number of combinations every time we add one of our coins
        /// 
        /// B) and C) are the same for every input value. 
        /// 
        /// To derive A -> Every time we ADD our unit to the previous (parent) combination
        /// we are adding one coin = +1 but subtracting "UNIT coins/bottom UNIT".
        /// so, e.g., if we are 'head' in the combination (4,1) then we subtract 3 coins          
        /// from each entry in the input array to reflect the fact we are reducing by 3 overall. 
        /// if  we are 'head' in the combination (8,4) then we subtract 1 coin.
        /// 
        /// so UNIT/HEAD_UNIT - 1 is the number of coins to remove from the input array 
        /// when we add one of our UNITS (B)
        /// 
        /// Number of combinations we need to generate is derived by how many combinations
        /// does our parent produce for one of our Units.   
        /// 
        /// E.g., say we add ONE of our units to the set of parent combinations
        /// we will be adding just one combination to the total, but, as the PARENT 
        /// combinations are already factored into the output - we need to make sure we do not
        /// duplicate them - so so we subtract however many there are.  
        /// 
        /// e.g., if we are '4' in 1,2,4 ... then when we are triggered first time 
        /// when value is '7' we will be triggered with 3 # combinations from 1,2
        /// - (5,1)=6,(3,2)=5,(1,3)=4 ... we are adding a new combination 
        /// (1,1,1)=3 so the total set will contain 4 combinations - but we 
        /// have already output 3 - so, in this example, the difference in combo numbers for adding a new Unit (C)
        /// will be 3-1 = 2.
        /// 
        /// We calculate the 'C' the first time we are triggered based on the 
        /// number of combos from our parent - easy. 
        /// 
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

            if (differenceInParentComboNumberForEachCoin == -1)
            {
                differenceInParentComboNumberForEachCoin = parentState.NumberCombinations();
            }

            if (noCoinsToSubtractForEachUnit == -1)
            {
                noCoinsToSubtractForEachUnit = (Head - GetRootUnit()) - 1;
            }

#if DEBUG
            CalculationValidator.Validate(validationKey,differenceInParentComboNumberForEachCoin,noCoinsToSubtractForEachUnit);
#endif

            // counts backwards from MaxCoins to Min-Coins via step 
            foreach (var c in parentState)
            {

            }

            return 1;
        }
    }
}