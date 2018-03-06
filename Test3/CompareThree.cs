using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using CoinsLib.CombinationCalculator;
using CoinsLib.CombinationCalculator.Underlying;

namespace Test3
{
    public class CompareThree
    {
        public static (IEnumerable<Int64> recursive, IEnumerable<Int64> uneven, IEnumerable<Int64> even) GetResults(
            IEnumerable<Int32> arr, int numberToAdd)
        {            
            var targetValue = arr.Sum() + numberToAdd;
            
            Console.WriteLine("target value is " + targetValue);

            var coin = MagicPurse.GenerateTestCoin(arr.ToList());

            var s = new Stopwatch();

            s.Start();

            var coinBuffer0 = new Int64[targetValue];

            CoinsByRecursion.TotalCoinsForEachCombinationForValue(coinBuffer0, coin, targetValue);            
           
            s.Stop();
            
            Console.WriteLine($"Elapsed ms - {s.ElapsedMilliseconds}");
            
            s = new Stopwatch();

            s.Start();

            var coinBuffer1 = GenerateUneven.Generate(arr.ToList(), targetValue);            
               
            s.Stop();
            
            Console.WriteLine(s.ElapsedMilliseconds);

            
            s = new Stopwatch();

            s.Start();

            var coinBuffer2 = GenerateEven.Generate(coin, targetValue);
           
            s.Stop();
            
            Console.WriteLine(s.ElapsedMilliseconds);

            return (coinBuffer0, coinBuffer1, coinBuffer2);


        }
    }
}