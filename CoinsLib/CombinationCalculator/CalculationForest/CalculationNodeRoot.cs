﻿using System;
using System.Linq;
using System.Runtime.CompilerServices;
using CoinsLib.Util;

namespace CoinsLib.CombinationCalculator.CalculationForest
{
    public class CalculationNodeRoot : CalculationNode
    {
        public CalculationNodeRoot(Coin c) : base(c)
        {
            
        }

        public override long CalculateTotalCoins(int valueToCalculate,long[] arr, CalcState parentState=null,  int depth = 1)
        {
            #if DEBUG
            if (valueToCalculate <= 0)
                throw new Exception($"Unexpected valueToCalculate - {valueToCalculate}");
            
            if (coin.Next != null)
                throw new Exception("Root node is supposed to be singleton");
            
            if (arr.Length < valueToCalculate)
                throw new Exception("passed in array is supposed to be >= max valueToCalculate");
            
            if (parentState != null)
                throw new Exception("Expected parentState to be null on calling root node");
            
            #endif 

            // we are singleton - e.g., '9'  if we get hit by '18' then 
            // there are 2 coins we can make ... 
            if (valueToCalculate % Head == 0)
            {
                int myCoins = valueToCalculate / Head;
                
                // keep tally in global record;
                arr[myCoins]++;

                return 1+ GetChildren().Sum(x =>
                    x.CalculateTotalCoins(valueToCalculate, arr, new CalcState(myCoins, myCoins-1, 1),depth+1));
            }
            return 0;

        }
    }
}