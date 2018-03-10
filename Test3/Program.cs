using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.Cryptography.X509Certificates;
using CoinsLib.CombinationCalculator;
using CoinsLib.CombinationCalculator.Underlying;
using CoinsLib.Util;

using System.Windows;
namespace Test3
{
    class Program
    {
      
       
        static void Main(string[] args)
        {
            int val = 30;
            var res2 = new Int64[val];
            var arr2 = new int[] { 2,1}.ToList();
            
            Console.WriteLine($"Value,{val}");

            var coins = MagicPurse.GenerateTestCoin(arr2);
            if (arr2.Count == 1)
            {
                var g = ValuesAndMultiplesForEvenFactors.GenerateValuesAndMultiplesRecursive(coins, val).ToArray();
                Printer.CalculateTotalCoinsForEachComboAndReturnCount(res2, val, g[0]);                                
            }
            if (arr2.Count == 2)
            {
                var g = ValuesAndMultiplesForEvenFactors.GenerateValuesAndMultiplesRecursive(coins, val).ToArray();
                Printer.CalculateTotalCoinsForEachComboAndReturnCount(res2, val, g[0], g[1]);                
            }
            else if (arr2.Count == 3)
            {
                var g = ValuesAndMultiplesForEvenFactors.GenerateValuesAndMultiplesRecursive(coins, val).ToArray();
                Printer.CalculateTotalCoinsForEachComboAndReturnCount(res2, val, g[0], g[1], g[2]);
            }
            else if (arr2.Count == 4)
            {
                var g = ValuesAndMultiplesForEvenFactors.GenerateValuesAndMultiplesRecursive(coins, val).ToArray();
                Printer.CalculateTotalCoinsForEachComboAndReturnCount(res2, val, g[0], g[1], g[2],g[3]);
            }
            else if (arr2.Count == 5)
            {
                var g = ValuesAndMultiplesForEvenFactors.GenerateValuesAndMultiplesRecursive(coins, val).ToArray();
                Printer.CalculateTotalCoinsForEachComboAndReturnCount(res2, val, g[0], g[1], g[2],g[3],g[4]);
            }


            return;
            
            
            bool printResults = false;
            
            var arr = new[] {64,32,16,8,4,  2, 1};
            
            Printer.Print(arr.Select(x=>Convert.ToInt64(x)),"Initial array");
            
            var res = CompareThree.GetResults(arr,234);
            
            
            

            Console.WriteLine("checking equals - Recursive vs. Uneven");

            Validator.CheckEquals(res.recursive,res.uneven);

            Console.WriteLine("checking equals - Uneven versus Even");

            Validator.CheckEquals(res.uneven,res.even);

            Console.WriteLine("checking equals - Recursive versus Even");

            Validator.CheckEquals(res.recursive,res.even);
           
            Console.WriteLine("finished");

            if (printResults)
            {
                Printer.Print(res.recursive,"Recursive");
                Printer.Print(res.uneven,"Uneven");
                Printer.Print(res.even,"Even");


            }

        }
    }
}