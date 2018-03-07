using System;
using System.Collections.Generic;
using System.Linq;
using CoinsLib.Coins;
using CoinsLib.CombinationCalculator.Underlying;

namespace Test3
{
    public static class GenerateEven
    {
        public static IEnumerable<Int64> Generate(CoinsLib.CombinationCalculator.Coin foo, int value,int max = 10000)
        {
            var arr = new Int64[value];
            
            var g = ValuesAndMultiplesForEvenFactors.GenerateValuesAndMultiplesRecursive(foo, value).ToArray();
            
            if (g.Length == 7)           
                CoinsForEvenFactors.CalculateTotalCoinsForEachComboAndReturnCount(arr,max,value, g[0], g[1], g[2], g[3], g[4], g[5],g[6]);
            if (g.Length == 6)           
                CoinsForEvenFactors.CalculateTotalCoinsForEachComboAndReturnCount(arr,max,value,g[0], g[1], g[2], g[3], g[4], g[5]);
            if (g.Length == 5)           
                CoinsForEvenFactors.CalculateTotalCoinsForEachComboAndReturnCount(arr, max,value, g[0], g[1], g[2], g[3], g[4]);
            if (g.Length == 4)           
                CoinsForEvenFactors.CalculateTotalCoinsForEachComboAndReturnCount(arr,max, value,g[0], g[1], g[2], g[3]);
            if (g.Length == 3)           
                CoinsForEvenFactors.CalculateTotalCoinsForEachComboAndReturnCount(arr, max,value, g[0], g[1], g[2]);
            if (g.Length == 2)           
                CoinsForEvenFactors.CalculateTotalCoinsForEachComboAndReturnCount(arr,max,value, g[0],g[1]);
            if (g.Length == 1)           
                CoinsForEvenFactors.CalculateTotalCoinsForEachComboAndReturnCount(arr,max,value, g[0]);

            return arr;


        }
    }
}