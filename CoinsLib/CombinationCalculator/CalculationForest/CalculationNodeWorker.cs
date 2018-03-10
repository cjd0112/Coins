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
        private int decrementInTotalCoinsForEachCoin = -1;
       

        /// <summary>
        /// Given an array from our parent indicating how many Total coins the parent
        /// has produced - we have a relatively simple way of calculating how many 
        /// total coins we will produce. 
        /// 
        /// We need three items : 
        /// 
        /// A) the parent array of total coins produced for each combo
        /// B) the number of coins to subtract for every combination 
        /// C) the number of combinations to subtract every time we add one of our coins
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
        /// Our parent provides us with #of combinations, the max-number of coins and the 'step' between each coin
        /// 
        /// We will be transforming that into an array where each row provides our corresponding number of combinations.
        /// 
        /// Because each new coin -> 1 to (Value/Units) -> reduces the combination set and reduces the maximum number
        /// we get a triangular output, where each row is number of coins 
        /// 
        /// E.g., if provided with following array indicating (45-26)=19 combinations and top one has 45 coins. 
        ///       
        /// 45,
        /// 44
        /// 43,
        /// 42,
        /// 41
        /// ...
        /// 26
        /// 
        /// we can convert this to following downward sloping triangle										
        /// 45										
        /// 44										
        /// 43	43									
        /// 42	42									
        /// 41	41									
        /// 40	40	40								
        /// 39	39	39								
        /// 38	38	38								
        /// 37	37	37	37							
        /// 36	36	36	36							
        /// 35	35	35	35							
        /// 34	34	34	34	34						
        /// 33	33	33	33	33						
        /// 32	32	32	32	32						
        /// 31	31	31	31	31	31					
        /// 30	30	30	30	30	30					
        /// 29	29	29	29	29	29					
        /// 28	28	28	28	28	28	28				
        /// 27	27	27	27	27	27	27				
        /// 26	26	26	26	26	26	26				
        /// 25	25	25	25	25	25	25	25			
        /// 	24	24	24	24	24	24	24			
        /// 		23	23	23	23	23	23			
        /// 			22	22	22	22	22	22		
        /// 				21	21	21	21	21		
        /// 					20	20	20	20		
        /// 						19	19	19	19	
        /// 							18	18	18	
        /// 								17	17	
        /// 									16	16
        /// 										15
        /// 
        /// where each new column is a new combination, 
        /// number of new columns is given by Value/Units,
        /// the top downward slope is given by (B) - no coins to subtract for each combination
        /// and length of each successive column is (C) minus previous length
        /// 
        /// and then we can convert this to e.g,. 
        /// 
        /// 45 = 1
        /// 44 = 1
        /// 43 = 2
        /// 42 = 2
        /// etc., 
        /// 
        /// And add that to our state - indicating that, e.g., there are 2 combinations where total coins is '43' etc., 
        /// 
        /// We could iterate over each column to do this - but this would be inefficient. 
        /// 
        /// so instead, we do it directly - by calculating the height of the whole triangle. 
        /// incrementing the no_of_combos until we reach the mid-point (25) in example above
        /// and then decrementing the no_of_combos after that until we reach the end point (15). 
        /// 
        /// It is grim - but efficient ... 
        /// 
        /// 
        /// </summary>
        /// <param name="valueToCalculate"></param>
        /// <param name="arr"></param>
        /// <param name="parentState"></param>
        /// <param name="depth"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public override long CalculateTotalCoins(int valueToCalculate,long[] arr, CalcState parentState=null,int depth = 1)
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
                noCoinsToSubtractForEachUnit = (Head / GetRootUnit()) - 1;

                if (noCoinsToSubtractForEachUnit < 0)
                    throw new ArgumentException("noCoinsToSubtractForEachUnit must be >=1");
            }

            // every time we have a new coin we reduce our number of coins by 
            // noCoinsToSubtractForEachUnit - (moving our triangle coin columns down by X)
            // but we also have less combinations to add ... this represents our net shift). 
            if (decrementInTotalCoinsForEachCoin == -1)
            {
                decrementInTotalCoinsForEachCoin = noCoinsToSubtractForEachUnit - parentState.NumberCombinations();
            }


#if DEBUG
            CalculationValidator.Validate(validationKey,differenceInParentComboNumberForEachCoin,noCoinsToSubtractForEachUnit);
#endif
            if (valueToCalculate % Head == 0 && valueToCalculate / Head > 0)
            {
                int cnt = 0;
                int max = -1;
                int min = int.MaxValue;

                foreach (var (triangleRow, number) in new CoinTriangle(parentState.MaxParentCoins,
                    parentState.NumberCombinations() - noCoinsToSubtractForEachUnit, valueToCalculate / Head,
                    noCoinsToSubtractForEachUnit, decrementInTotalCoinsForEachCoin))
                {
                    if (max == -1)
                        max = triangleRow;
                    min = triangleRow;
                    arr[triangleRow] += number;
                    cnt++;
                }

                var ourState = new CalcState(max, min, parentState.Step);
                return cnt + GetChildren().Sum(x => x.CalculateTotalCoins(valueToCalculate, arr, ourState, depth+1));
            }

            return 0;
        }
    }
}