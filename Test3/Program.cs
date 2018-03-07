using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.Cryptography.X509Certificates;
using CoinsLib.CombinationCalculator;
using CoinsLib.CombinationCalculator.Underlying;
using CoinsLib.Util;

namespace Test3
{
    class Program
    {
      
       
        static void Main(string[] args)
        {

            var res2 = new Int64[30];

            var coins = MagicPurse.GenerateTestCoin(new int[] {4, 2, 1}.ToList());
            
            var g = ValuesAndMultiplesForEvenFactors.GenerateValuesAndMultiplesRecursive(coins,30 ).ToArray();



            Printer.CalculateTotalCoinsForEachComboAndReturnCount(res2, 30, g[0], g[1], g[2]);

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