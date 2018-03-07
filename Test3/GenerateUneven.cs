using System;
using System.Collections.Generic;
using System.Linq;
using CoinsLib.CombinationCalculator.Underlying;

namespace Test3
{
    public static class GenerateUneven
    {
        public static IEnumerable<Int64> Generate(List<Int32> foo, int value,int max = 5000)
        {
            var arr = new Int64[value];
            
            if (foo.Count() == 7)        
                CoinsForUnevenFactors.CalculateTotalCoinsForEachComboAndReturnCount(arr,max,value, foo[0],foo[1],foo[2],foo[3],foo[4],foo[5],foo[6]);
            if (foo.Count() == 6)        
                CoinsForUnevenFactors.CalculateTotalCoinsForEachComboAndReturnCount(arr,max,value, foo[0],foo[1],foo[2],foo[3],foo[4],foo[5]);
            if (foo.Count() == 5)        
                CoinsForUnevenFactors.CalculateTotalCoinsForEachComboAndReturnCount(arr,max,value, foo[0],foo[1],foo[2],foo[3],foo[4]);
            if (foo.Count() == 4)        
                CoinsForUnevenFactors.CalculateTotalCoinsForEachComboAndReturnCount(arr,max,value, foo[0],foo[1],foo[2],foo[3]);
            if (foo.Count() == 3)        
                CoinsForUnevenFactors.CalculateTotalCoinsForEachComboAndReturnCount(arr,max,value, foo[0],foo[1],foo[2]);
            if (foo.Count() == 2)        
                CoinsForUnevenFactors.CalculateTotalCoinsForEachComboAndReturnCount(arr,max,value, foo[0],foo[1]);
            if (foo.Count() == 1)        
                CoinsForUnevenFactors.CalculateTotalCoinsForEachComboAndReturnCount(arr,max,value, foo[0]);

            return arr;


        }
    }
}